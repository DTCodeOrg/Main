using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace Main.WebAppCore.Controllers
{
    public class RefreshController: BaseController
    {
        public readonly ITenantSetter _tenantSetter;
        public readonly ITokenService _tokenService;
        public readonly ITenantContext _tenantContext;

        public RefreshController (ITenantSetter tenantSetter,ITokenService tokenService,ITenantContext tenantContext)
        {
            _tenantSetter = tenantSetter;
            _tokenService = tokenService;
            _tenantContext = tenantContext;
        }

        [HttpPost ("refresh-token")]
        public async Task<IActionResult> Refresh ()
        {
            var cookieName = $".App.RefreshToken.{_tenantSetter.CurrentTenantId}";

            // Extract token from the secure cookie
            if ( !Request.Cookies.TryGetValue (cookieName,out var currentRefreshToken) )
            {
                return Unauthorized ("Missing token.");
            }

            try
            {
                // Execute the service logic
                var tokenResult =
                    await _tokenService.RotateRefreshTokenAsync
                        (currentRefreshToken ?? "", _tenantSetter.CurrentTenantId,
                        _tenantContext.ApplicationUserId,
                        _tenantContext.GetCurrentTenantRole() ?? "",
                        _tenantContext.User?.FindFirst("UserRole")?.Value ?? "",
                        _tenantContext.User?.FindFirst("UserName")?.Value ?? "",
                        _tenantContext.User?.FindFirst("Email")?.Value ?? "",15,7);

                if ( tokenResult == null )
                {
                    return Unauthorized ("Invalid or expired token.");
                }

                // 1. COOKIE 1: Save the short-lived Access JWT (Expires in 15 minutes)
                Response.Cookies.Append ($".App.AccessToken.{_tenantSetter.CurrentTenantId}",
                tokenResult.AccessToken.ToString () ?? "",
                new CookieOptions
                {
                    HttpOnly = true,   // Protects against XSS attacks stealing your JWT
                    Secure = true,     // Mandates HTTPS through Nginx
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes (15),
                    Path = "/"         // Accessible by all pages in your app
                });

                // 2. COOKIE 2: Save the long-lived Refresh Token (Expires in 7 days)
                Response.Cookies.Append ($".App.RefreshToken.{_tenantSetter.CurrentTenantId}",tokenResult.RefreshToken,new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays (7),
                    // FIX: Aligned path value to match your actual [HttpPost("refresh-token")] route
                    Path = "/refresh-token"
                });

                // Return the fresh access JWT in the JSON payload
                return Ok (new
                {
                    token = tokenResult.AccessToken
                });
            }
            catch ( SecurityException ex )
            {
                // Clear cookies immediately on breach detection
                Response.Cookies.Delete (cookieName);
                return Unauthorized (ex.Message);
            }
        }

    }
}
