using Main.Infrastructure.ICrosscuttingServices;
using Main.Services;

namespace Main.WebAppCore.Controllers.Extensions;

public static class AuthorizationExtensions
{
    public static async Task<string> GetTenantUserRole
    (IAccountService accountService,string email,Guid resolvedTenantId)
    {
        string tenantRole = await accountService.GetTenantUserRoleClaim
        (email, resolvedTenantId);

        return tenantRole;
    }

    public static async Task AddTenantIsolatedHeaderToken (
        HttpContext context,ITokenService tokenService,
        string userId,Guid resolvedTenantId,
        string role,string formatedTenantRole,string userName,string email,int minutes,int days)
    {
        var accessJwt = await tokenService.GenerateAccessToken(userId, resolvedTenantId, formatedTenantRole, role, userName, email, minutes);

        var refreshTokenStr = tokenService.GenerateRefreshToken();

        _ = await tokenService.SaveRefreshToken (userId,resolvedTenantId,refreshTokenStr);

        var tenantIdStr = resolvedTenantId.ToString();

        // Standard Cookie Options Base Configuration
        var baseCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // Cooperates with CookieSecurePolicy.Always requirement through Nginx
            SameSite = SameSiteMode.Lax,
            Path = "/"  ,
            Domain = context.Request.Host.Host
            // Domain configuration can be omitted if you are running multi-tenant structures across 
            // distinct subdomains, as the browser automatically scopes it to the active Host origin.
        };

        // Append Access Token Cookie
        context.Response.Cookies.Append ($".App.AccessToken.{tenantIdStr}",accessJwt,
            new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddMinutes (minutes) });

        // Append Refresh Token Cookie
        context.Response.Cookies.Append ($".App.RefreshToken.{tenantIdStr}",refreshTokenStr,
            new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddDays (days) });
    }

}