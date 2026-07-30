using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace Main.WebAppCore.Controllers;


public class RefreshController: BaseController
{
    private readonly ITenantSetter _tenantSetter;
    private readonly ITokenService _tokenService;
    private readonly ITenantContext _tenantContext;

    public RefreshController (ITenantSetter tenantSetter,ITokenService tokenService,ITenantContext tenantContext)
    {
        _tenantSetter = tenantSetter;
        _tokenService = tokenService;
        _tenantContext = tenantContext;
    }

    // FIX: Added a leading slash to route explicitly to domain root "/refresh-token"
    [HttpPost ("refresh-token")]
    [ValidateAntiForgeryToken] // Ensure your global or local anti-forgery filters run securely here
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
                Secure = true, // Mandated via Nginx HTTPS terminations
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
                Path = "/refresh-token"
            });

            // Return payload matching client expectation
            return Ok (new
            {
                token = tokenResult.AccessToken
            });
        }
        catch ( SecurityException ex )
        {
            // FIX: Cookie deletion criteria must match the exact security settings (Secure, SameSite) 
            // used when appended, otherwise browsers block the deletion payload.
            Response.Cookies.Delete (cookieName,new CookieOptions
            {
                Path = "/refresh-token",
                Secure = true,
                SameSite = SameSiteMode.Strict,
                HttpOnly = true
            });

            return Unauthorized (ex.Message);
        }
    }
}
