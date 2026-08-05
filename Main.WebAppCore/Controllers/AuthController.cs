using DataTransferModel;
using Main.Common;
using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Infrastructure.ICrosscuttingServices;
using Main.Services;
using Main.WebAppCore.Controllers.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore.Controllers;

public class AuthController: BaseController
{
    private readonly ITenantSetter _tenantSetter;
    private readonly ITenantContext _userContext;
    private readonly IAccountService _userAccountService;
    private readonly IEmailSenderService _emailService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<ExceptionLoggingService>  _logger;

    public AuthController (
        IAccountService userAccountService,
        ITenantContext userContext,
        IEmailSenderService emailService,
        ITenantSetter tenantSetter,
        ITokenService tokenService,
        ILogger<ExceptionLoggingService> logger
       )
    {
        _userAccountService = userAccountService;
        _userContext = userContext;
        _emailService = emailService;
        _tenantSetter = tenantSetter;
        _tokenService = tokenService;
        _logger = logger;
    }

    // Registration Flow 1: User accesses the registration page
    public IActionResult Registration ()
    {
        var objModel = new RegistrationViewModel();

        return View (objModel);
    }

    // Registration Flow 2: User submits the registration form.
    [HttpPost]
    public async Task<IActionResult> Registration (RegistrationViewModel registrationViewModel)
    {
        if ( ModelState.IsValid )
        {
            return View (registrationViewModel);
        }

        try
        {
            var that = this!;

            UserAccountDataModel userAccountDataModel
            = AuthExtensions.MapToDataModel(registrationViewModel != null ?
            registrationViewModel : new RegistrationViewModel());

            // Create the tenant user account (ApplicationUser)
            IdentityResult result =
            await _userAccountService.CreateApplicationUserAccount
            ( userAccountDataModel );

            string email =  registrationViewModel?.Email ?? string.Empty;

            if ( result.Succeeded )
            {
                await SendVerifyEmail (email,HttpContext);

                return RedirectToAction ("VerifyEmailSent");
            }

            return View (registrationViewModel);
        }
        catch
        {
            throw;
        }
    }

    public async Task SendVerifyEmail
    (string? email,HttpContext context)
    {
        string localEmail = email ??  string.Empty ;
        string emailVerifyToken = await _userAccountService.GetEmailVerifyToken (localEmail);

        string? verifyLink = Url.Action(
            action: "VerifyLink",
            controller: "Auth",
            values: new
            {
                Email = email, Token = emailVerifyToken
            },
            protocol: Request.Scheme
        );

        var verifyEmailDataModel = new VerifyDataModel ()
        {
            Email = localEmail , VerifyLink = verifyLink!
        };

        await _emailService.SendEmailVerificationAsync (verifyEmailDataModel);
    }


    public IActionResult VerifyEmailSent ()
    {
        ViewData["Title"] = "Email Sent";
        return View ();
    }


    public async Task<IActionResult> VerifyLink (string email,string token)
    {
        if ( string.IsNullOrEmpty (email) || string.IsNullOrEmpty (token) )
        {
            return BadRequest ("Invalid verification request parameters.");
        }

        _ = _userContext.GetCreateBaseDataModel ();
        _ = await _userAccountService.CompleteEmailVerification (email,token);

        return RedirectToAction ("VerifyComplete");
    }


    public IActionResult VerifyComplete ()
    {
        ViewData["Title"] = "Verification Complete";
        return View ();
    }


    public IActionResult Login ()
    {
        LoginViewModel loginDisplayViewModel = new("Login");
        return View (loginDisplayViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Login (LoginViewModel loginDisplayViewModel)
    {
        if ( loginDisplayViewModel == null )
        {
            ModelState.AddModelError (string.Empty,"Invalid login attempt form payload.");
            return View (new LoginViewModel ());
        }

        string email = loginDisplayViewModel?.Email ?? string.Empty;


        if ( !ModelState.IsValid )
        {
            return View ("Login",loginDisplayViewModel);
        }

        ApplicationUserDataModel? applicationUser = await _userAccountService.GetApplicationUser (email);

        bool result = await IsEmailConfirmed (email);

        if ( !result )
        {

            await SendVerifyEmail (email,HttpContext);

            return View (new LoginViewModel ());
        }

        bool signinresult = await _userAccountService.PasswordSignInAsync (applicationUser?.Email!,loginDisplayViewModel?.Password!);

        if ( signinresult )
        {
            string tenantRole = await _userAccountService.GetTenantUserRoleClaim
            (email, _tenantSetter.CurrentTenantId);

            string formatedTenantRole = $"{applicationUser?.Id ?? ""}:{_tenantSetter.CurrentTenantId}:{tenantRole}";


            _ = AuthorizationExtensions.AddTenantIsolatedHeaderToken
                (HttpContext,_tokenService,applicationUser?.Id
                ?? "",_tenantSetter.CurrentTenantId,tenantRole.ToString (),
                formatedTenantRole,applicationUser?.UserName ?? "",
                applicationUser?.Email ?? "",15,7);

            return RedirectToAction ("Index","Home",new
            {
                area = ""
            });

        }

        return View ("Login",loginDisplayViewModel);
    }

    public async Task<bool> IsEmailConfirmed (string? email)
    {
        bool result = await _userAccountService.IsEmailConfirmedAsync (email ?? "");

        return result;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout ()
    {
        var tenantId = _tenantSetter.CurrentTenantId;
        var accessTokenName = $".App.AccessToken.{tenantId.ToString()}";
        string refreshTokenName = $".App.RefreshToken.{tenantId.ToString()}";

        // 1. Revoke the token in the database first
        if ( Request.Cookies.TryGetValue (refreshTokenName,out var refreshToken) )
        {
            _ = await _tokenService.RevokeUserRefreshTokensAsync (refreshToken,_tenantSetter.CurrentTenantId);
        }

        // 2. Define CookieOptions that MATCH your creation settings perfectly
        CookieOptions cookieOptions = new ()
        {
            HttpOnly = true,
            Secure = true,          // Must match your login creation setup
            SameSite = SameSiteMode.Lax, // Must match your login creation setup
            Path = "/" ,             // Must match your login creation setup
            Domain = Request.Host.Host // Must match your login creation setup
        };

        // 3. Pass the options object into the Delete method
        HttpContext.Response.Cookies.Delete (accessTokenName,cookieOptions);
        HttpContext.Response.Cookies.Delete (refreshTokenName,cookieOptions);

        // 4. Signal Nginx to bypass caches
        Response.Headers.Append ("Cache-Control","no-cache, no-store, must-revalidate");
        Response.Headers.Append ("X-Clear-Cache","true");

        return RedirectToAction ("Index","Home");
    }



    // Password Reset Flow (1): User initiates password reset by providing email address 
    [HttpGet]
    public IActionResult ResetEmail ()
    {
        ViewData["Title"] = "Password Reset";

        return View (new ForgotPasswordViewModel ());
    }


    // Password Reset Flow (2): User submits email address to receive password reset link.
    [HttpPost]
    public async Task<IActionResult> ResetEmail (ForgotPasswordViewModel forgotPasswordViewModel)
    {
        if ( !ModelState.IsValid )
        {
            return View (forgotPasswordViewModel);
        }

        var that = this!;
        var user = await _userAccountService.FindByEmailAsync(forgotPasswordViewModel.Email);

        if ( user != null &&
        await EmailExtensions.IsEmailConfirmed (_userAccountService,user?.Email!) )
        {
            await EmailExtensions.SendVerifyEmail
            (( IUrlHelper ) that,_userAccountService,
            _emailService,forgotPasswordViewModel.Email,HttpContext);

            return RedirectToAction ("SendVerifyEmail");
        }

        var result = await EmailExtensions.SendResetEmail
        (( IUrlHelper ) that, _userAccountService, _emailService, forgotPasswordViewModel.Email, HttpContext);

        return RedirectToAction (nameof (ResetEmailSent));
    }



    // Password Reset Flow (3): User is informed that reset email is sent.
    [HttpGet]
    public IActionResult ResetEmailSent ()
    {
        ViewData["Title"] = "Reset Email Sent";
        return View ();
    }


    // Password Reset Flow (4): User clicks the password reset link
    public async Task<IActionResult> ResetLink (string email,string token)
    {
        if ( string.IsNullOrEmpty (email) || string.IsNullOrEmpty (token) )
        {
            return BadRequest ("Invalid link request.");
        }

        var user = await _userAccountService.FindByEmailAsync(email);

        if ( user == null )
        {
            return BadRequest ("Invalid link request.");
        }

        var resetPasswordViewModel = new ResetPasswordViewModel()
        {
            Email = email,
            Token = token
        };

        return View ("ResetPassword",resetPasswordViewModel);
    }


    // Password Reset Flow - (5): User submits the new password
    [HttpPost]
    public async Task<IActionResult> ResetPassword (ResetPasswordViewModel resetPasswordViewModel)
    {
        if ( !ModelState.IsValid )
        {
            return View (resetPasswordViewModel);
        }

        ApplicationUserDataModel? applicationUserDataModel = await _userAccountService.FindByEmailAsync(resetPasswordViewModel.Email);


        // Reset with new password and invalidate the token and timestamp to prevent reuse 
        var email = applicationUserDataModel?.Email;

        bool result = await _userAccountService.ResetPasswordAsync(email!, resetPasswordViewModel.Token, resetPasswordViewModel.ConfirmPassword);

        if ( result )
        {
            return RedirectToAction (nameof (ResetComplete));
        }

        return View (resetPasswordViewModel);
    }


    // Password Reset Flow - (6): User is shown a confirmation page
    [HttpGet]
    public IActionResult ResetComplete ()
    {
        ViewData["Title"] = "Password Updated";
        return View ();
    }

    // Change Password Flow - (1): Authenticated user accesses the change password form
    [HttpGet]
    public async Task<IActionResult> ChangePassword ()
    {
        if ( User == null )
        {
            return RedirectToAction ("Login","Auth");
        }

        var changePasswordViewModel = new ChangePasswordViewModel
        {
            Email = User.Claims
            .FirstOrDefault (c => c.Type == ClaimTypes.Email)?.Value ?? ""
        };

        return View (changePasswordViewModel);
    }



    // Change Password Flow - (2): User submits the change password form 
    [HttpPost]

    public async Task<IActionResult> ChangePassword (ChangePasswordViewModel changePasswordViewModel)
    {

        if ( !ModelState.IsValid )
        {
            return View (changePasswordViewModel);
        }

        ApplicationUserDataModel? userIdentity = await _userAccountService.FindByEmailAsync(changePasswordViewModel.Email);

        if ( userIdentity == null )
        {
            return View (changePasswordViewModel);
        }

        var result =
        await _userAccountService.ChangePasswordAsync(userIdentity?.Email!, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

        if ( result )
        {
            return RedirectToAction (nameof (ResetComplete));
        }

        return View (changePasswordViewModel);
    }
}
