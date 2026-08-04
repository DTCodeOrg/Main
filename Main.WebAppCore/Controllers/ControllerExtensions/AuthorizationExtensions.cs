using Main.Common;
using Main.Infrastructure.ICrosscuttingServices;

namespace Main.WebAppCore.Controllers.Extensions;

public static class AuthorizationExtensions
{

    public static async Task AddTenantIsolatedHeaderToken (
        HttpContext context,ITokenService tokenService,
        string userId,Guid resolvedTenantId,
        string role,string formatedTenantRole,string userName,string email,int minutes,int days)
    {
        var accessJwt = await tokenService.GenerateAccessToken(userId, resolvedTenantId, formatedTenantRole, role, userName, email, minutes);

        var refreshTokenStr = tokenService.GenerateRefreshToken();

        _ = await tokenService.SaveRefreshToken (userId,resolvedTenantId,refreshTokenStr);

        var tenantIdStr = resolvedTenantId.ToString();

        var baseCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/"  ,
            Domain = context.Request.Host.Host
        };

        // Append Access Token Cookie
        context.Response.Cookies.Append ($".App.AccessToken.{tenantIdStr}",accessJwt,
            new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddMinutes (minutes) });

        // Append Refresh Token Cookie
        context.Response.Cookies.Append ($".App.RefreshToken.{tenantIdStr}",refreshTokenStr,
            new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddDays (days) });
    }

    public static async Task AddTenantRefreshHeaderToken (HttpContext context,Guid resolvedTenantId,TokenResult rotationResult,int minutes,int days)
    {
        var tenantIdStr = resolvedTenantId.ToString();

        var baseCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/"  ,
            Domain = context.Request.Host.Host
        };

        // Append Access Token Cookie
        context.Response.Cookies.Append ($".App.AccessToken.{tenantIdStr}",rotationResult.AccessToken,
            new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddMinutes (minutes) });

        // Append Refresh Token Cookie
        context.Response.Cookies.Append ($".App.RefreshToken.{tenantIdStr}",rotationResult.RefreshToken,
            new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddDays (days) });
    }
}