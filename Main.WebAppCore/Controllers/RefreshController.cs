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
            var currentTenantId = _tenantSetter.CurrentTenantId;
            var cookieName = $".App.RefreshToken.{currentTenantId}";

            // 1. Extract raw refresh token from the cookie securely
            if ( !Request.Cookies.TryGetValue (cookieName,out var currentRefreshToken) || string.IsNullOrEmpty (currentRefreshToken) )
            {
                return Unauthorized ("Missing token.");
            }

            try
            {
                // 2. Clear application contextual metadata; pass only token and tenant to the service
                var tokenResult = await _tokenService.RotateRefreshTokenAsync(currentRefreshToken, currentTenantId, 15, 7);

                if ( tokenResult == null )
                {
                    return Unauthorized ("Invalid or expired token.");
                }

                // 3. Drop Cookie 1: Fresh Short-Lived Access JWT
                Response.Cookies.Append ($".App.AccessToken.{currentTenantId}",tokenResult.AccessToken,new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes (15),
                    Path = "/"
                });

                // 4. Drop Cookie 2: Rolled Long-Lived Refresh Token String
                Response.Cookies.Append ($".App.RefreshToken.{currentTenantId}",tokenResult.RefreshToken,new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays (7),
                    Path = "/refresh-token" // Path validation matches routing correctly
                });

                return Ok (new
                {
                    token = tokenResult.AccessToken
                });
            }
            catch ( SecurityException ex )
            {
                // Clear old/compromised cookie on reuse anomalies or theft detections
                Response.Cookies.Delete (cookieName,new CookieOptions { Path = "/refresh-token" });
                return Unauthorized (ex.Message);
            }
        }

    }
}
