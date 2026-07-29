using DataTransferModel;
using Main.Common;
using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
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

    // Registration Flow 3: User requested to check email
    public IActionResult VerifyEmailSent ()
    {
        ViewData["Title"] = "Email Sent";
        return View ();
    }

    // Registration Flow 4: User clicks Verification link
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

    // Registration Flow 5: Tenant Account is confirmed.(Email verified)
    public IActionResult VerifyComplete ()
    {
        ViewData["Title"] = "Verification Complete";
        return View ();
    }



    // Login Flow 1: login page
    public IActionResult Login ()
    {
        LoginViewModel loginDisplayViewModel = new("Login");
        return View (loginDisplayViewModel);
    }

    // Login Flow: login form submit (1. authentication, 2. authorization (jwt token)
    [HttpPost]
    public async Task<IActionResult> Login (LoginViewModel loginDisplayViewModel)
    {
        // 1. Guard Clause against completely empty payloads
        if ( loginDisplayViewModel == null )
        {
            ModelState.AddModelError (string.Empty,"Invalid login attempt form payload.");
            return View (new LoginViewModel ());
        }

        string email = loginDisplayViewModel?.Email ?? string.Empty;

        // 1.2. FIX: Validate form structural rules first (Required fields, Email format, etc.)
        if ( !ModelState.IsValid )
        {
            // If they left fields blank, return the view with automatic validation span messages
            return View ("Login",loginDisplayViewModel);
        }

        // 2 Application User needed for User Id
        ApplicationUserDataModel? applicationUser = await _userAccountService.GetApplicationUser (email);


        _logger.LogWarning ("Appli User Email:" + applicationUser?.Email!);

        _logger.LogWarning ("Appli User Id:" + applicationUser?.Id!);

        _logger.LogWarning ("Appli Tenant Id:" + applicationUser?.MyTenantId!);

        // 3. Validation: User existence and email confirmation rules
        bool result = await IsEmailConfirmed (email);
        _logger.LogWarning ("Email Confirmed: " + result + "...");

        if ( !result )
        {

            await SendVerifyEmail (email,HttpContext);

            return View (new LoginViewModel ());
        }



        // 4. User password submission check
        bool signinresult = await _userAccountService.PasswordSignInAsync (applicationUser?.Email!,loginDisplayViewModel?.Password!,isPersistent: false, lockoutOnFailure: false);

        // 3. Validation: User existence and email confirmation rules

        _logger.LogWarning ("Signin Result (true/false): " + signinresult + "...");

        // 5. Login successful workflow execution
        if ( signinresult )
        {

            _logger.LogWarning ("Signin Success: (Tenannt Id) " + _tenantSetter.CurrentTenantId.ToString () + "...");

            // 2. Get tenant specific role (find for user)
            string tenantRole = await AuthorizationExtensions.GetTenantUserRole(_userAccountService, email, _tenantSetter.CurrentTenantId);

            string formatedTenantRole = $"{applicationUser?.Id ?? ""}:{_tenantSetter.CurrentTenantId}:{tenantRole}";

            _logger.LogWarning ("Tenant Role: " + tenantRole + "...");

            // 4. Append safe Isolated JWT Identity Header
            _ = AuthorizationExtensions.AddTenantIsolatedHeaderToken
                (HttpContext,
                _tokenService,
                applicationUser?.Id
                ?? "",
                _tenantSetter.CurrentTenantId,
                tenantRole.ToString (),
                formatedTenantRole,
                applicationUser?.UserName ?? "",
                applicationUser?.Email ?? "",
                15,7);

            _logger.LogWarning ("Signin Success (formatted tenant role): " + formatedTenantRole + "...");

            //// Commit claims tracking properties directly to HttpContext
            //AuthorizationExtensions.AddUserClaims (HttpContext,
            //            applicationUser?.Id ?? "",
            //            _tenantSetter.CurrentTenantId,
            //            formatedTenantRole,
            //            applicationUser?.UserName ?? "",
            //            applicationUser?.Email ?? "",
            //            tenantRole);


            _logger.LogWarning ("Claims Success  (User Name): " + applicationUser?.UserName + "...");


            // Route directly to your newly fixed root index endpoint
            return RedirectToAction ("Index","Home");
        }

        return View ("Login",loginDisplayViewModel);
    }

    public async Task<bool> IsEmailConfirmed (string? email)
    {
        bool result = await _userAccountService.IsEmailConfirmedAsync (email ?? "");

        return result;
    }

    [HttpPost] // Highly recommended to use POST for logout to prevent pre-fetching browser logs
    public async Task<IActionResult> Logout ()
    {
        await _userAccountService.SignOutAsync ();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var tenantId = _tenantSetter.CurrentTenantId;

        // 1. Invalidate long-lived token on the backend server database
        if ( !string.IsNullOrEmpty (userId) )
        {
            _ = await _tokenService.RevokeUserRefreshTokensAsync (userId,tenantId);
        }

        // 2. Erase both token cookies from the browser
        Response.Cookies.Delete ($".App.AccessToken.{tenantId}",new CookieOptions { Path = "/" });

        // FIX: Aligned path value to match your actual "/refresh-token" route layout
        Response.Cookies.Delete ($".App.RefreshToken.{tenantId}",new CookieOptions { Path = "/refresh-token" });

        // 3. Clear your custom tenant session state
        HttpContext.Session.Clear ();

        // 4. CLIENT-SIDE: Signal modern browsers to wipe all local origins data
        Response.Headers.Append ("Clear-Site-Data","\"cache\", \"storage\"");

        // 5. CLIENT-SIDE: Instruct proxy (Nginx) and browser to never cache this response
        Response.Headers.Append ("Cache-Control","no-cache, no-store, must-revalidate");
        Response.Headers.Append ("Pragma","no-cache");
        Response.Headers.Append ("Expires","0");


        // 6. CLIENT-SIDE: Explicitly wipe your real multi-tenant antiforgery cookie via correct naming convention
        // FIX: Changed name prefix from ".AspNetCore.Antiforgery" to match your active "TenantAntiforgeryFilter" cookie
        var tenantXsrfCookieName = $".AspNetCore.Antiforgery.{tenantId}";
        Response.Cookies.Delete (tenantXsrfCookieName,new CookieOptions
        {
            Path = "/",
            Secure = true,
            HttpOnly = false // Must match the original creation flags from your filter
        });

        // 7. Deletes standard ASP.NET Identity and Session cookies if they exist
        Response.Cookies.Delete (".AspNetCore.Identity.Application",new CookieOptions { Path = "/" });
        Response.Cookies.Delete (".AspNetCore.Session",new CookieOptions { Path = "/" });

        // 8. Redirect to login
        // FIX: Changed target controller from "Account" to your actual working "Auth" controller
        return RedirectToAction ("Login","Auth");
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
