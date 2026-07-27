using DataTransferModel;
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

    public AuthController (
        IAccountService userAccountService,
        ITenantContext userContext,
        IEmailSenderService emailService,
        ITenantSetter tenantSetter,
        ITokenService tokenService
       )
    {
        _userAccountService = userAccountService;
        _userContext = userContext;
        _emailService = emailService;
        _tenantSetter = tenantSetter;
        _tokenService = tokenService;
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
                await EmailExtensions.SendVerifyEmail
                (( IUrlHelper ) that,_userAccountService,
                _emailService,email,HttpContext);

                return RedirectToAction ("VerifyEmailSent");
            }

            return View (registrationViewModel);
        }
        catch
        {
            throw;
        }
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

        var that = this!;
        string email = loginDisplayViewModel?.Email ?? string.Empty;

        // 2. FIX: Validate form structural rules first (Required fields, Email format, etc.)
        if ( !ModelState.IsValid )
        {
            // If they left fields blank, return the view with automatic validation span messages
            return View ("Login",loginDisplayViewModel);
        }

        // 3. (1. Authentication Setup)
        Guid resolvedTenantId = _tenantSetter.CurrentTenantId;

        var applicationIdentityUserDataModel
        = await _userAccountService.GetApplicationUser(email, resolvedTenantId);

        // 4. Validation: User existence and email confirmation rules
        if ( await AuthentiicationExtensions.InvalidApplicationUser (_userAccountService,applicationIdentityUserDataModel,loginDisplayViewModel!,resolvedTenantId) )
        {
            bool emailConfirm = loginDisplayViewModel?.EmailConfirmed ?? true;
            if ( !emailConfirm )
            {
                await EmailExtensions.SendVerifyEmail (( IUrlHelper ) that,_userAccountService,_emailService,email,HttpContext);
            }

            return View ("Login",loginDisplayViewModel);
        }

        // 5. User password submission check
        bool result = await AuthentiicationExtensions.PasswordSignInAsync (
        _userAccountService,
        applicationIdentityUserDataModel!.UserName!,
        loginDisplayViewModel!.Password,
        isPersistent: false,
        lockoutOnFailure: false
    );

        // 6. Login successful workflow execution
        if ( result )
        {
            // Get tenant specific role
            string tenantRole = await AuthorizationExtensions.GetTenantUserRole(_userAccountService, email, resolvedTenantId);

            // Append safe Isolated JWT Identity Header
            AuthorizationExtensions.AddTenantIsolatedHeaderToken (HttpContext,_tokenService,applicationIdentityUserDataModel.Id,resolvedTenantId,tenantRole.ToString (),15,7);

            string formatedTenantRole = $"{applicationIdentityUserDataModel.Id}:{resolvedTenantId}:{tenantRole}";

            // Commit claims tracking properties directly to HttpContext
            AuthorizationExtensions.AddUserClaims (HttpContext,applicationIdentityUserDataModel.Id,resolvedTenantId,formatedTenantRole,applicationIdentityUserDataModel.UserName!,applicationIdentityUserDataModel.Email!);

            // Route directly to your newly fixed root index endpoint
            return RedirectToAction ("Index","Home");
        }

        // 7. FIX: If password verification failed, assign the bad credential warning here at the end
        loginDisplayViewModel.Message = "Invalid login attempt. Please check your credentials and try again.";
        return View ("Login",loginDisplayViewModel);
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
        var tenantXsrfCookieName = $".TenantAuth.XSRF.{tenantId}";
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
