using Main.Common.Models;
using Main.Infrastructure.ICrosscuttingServices;

namespace Main.WebAppCore.Controllers.ControllerExtensions;

public static class AuthorizationExtensions
{
    public static async Task AddTenantRefreshHeaderToken (HttpContext context,Guid resolvedTenantId,TokenResult rotationResult,int minutes,int days)
    {
        var tenantIdStr = resolvedTenantId.ToString();

        var baseCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/",
            Domain = context.Request.Host.Host
        };

        // 1. Correct the cookie lifespan matching rules
        // To allow the refresh token to run over 7 days, the access cookie file 
        // must physically stay in the browser. The JWT inside will manage the 15-minute expiry check.
        context.Response.Cookies.Append ($".App.AccessToken.{tenantIdStr}",rotationResult.AccessToken,
            new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddDays (days) }); // Extended browser lifetime

        context.Response.Cookies.Append ($".App.RefreshToken.{tenantIdStr}",rotationResult.RefreshToken,
            new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddDays (days) });

        var tokenService = context.RequestServices.GetRequiredService<ITokenService>();


        // 2. Validate and Decrypt the brand-new token you just built
        var validateResult = tokenService.ValidateAndDecryptToken(rotationResult.AccessToken, out _);

        if ( validateResult != null )
        {
            // FIX: Force the current HTTP request context thread to immediately accept the fresh claims identity.
            // This stops the downstream .AddJwtBearer middleware from checking the old expired cookie.
            context.User = validateResult;
        }
    }
}