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

    public static async Task AddTenantIsolatedHeaderToken
    (HttpContext context,ITokenService tokenService,
     string userId,Guid resolvedTenantId,
     string role,string formatedTenantRole,string userName,string email,int minutes,int days)
    {
        // 2. Create your tokens after successful sign-in
        var accessJwt = await tokenService.GenerateAccessToken(userId,resolvedTenantId,formatedTenantRole,role,userName,email,minutes);

        var refreshTokenStr = tokenService.GenerateRefreshToken();

        // 1. Fetch token record from DB/Redis by its raw token string and matching tenant context

        // 3. Save the Refresh Token string securely to your database or cache
        _ = await tokenService.SaveRefreshToken (userId,resolvedTenantId,refreshTokenStr);

        // 3. COOKIE 1: Save the short-lived Access JWT (Expires in 15 minutes)
        context.Response.Cookies.Append ($".App.AccessToken.{resolvedTenantId.ToString ()}",
        accessJwt.ToString () ?? "",
        new CookieOptions
        {
            HttpOnly = true,   // Protects against XSS attacks stealing your JWT
            Secure = true,     // Mandates HTTPS through Nginx
            Domain = context!.Request.Host.Host,
            Path = "/",
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes (minutes)
        });

        // 4. COOKIE 2: Save the long-lived Refresh Token (Expires in 7 days)
        context.Response.Cookies.Append ($".App.RefreshToken.{resolvedTenantId.ToString ()}",refreshTokenStr,new CookieOptions
        {
            HttpOnly = true,                 // Protects against XSS attacks stealing your refresh token
            Secure = true,                   // Mandates HTTPS through Nginx
            SameSite = SameSiteMode.Lax,     // <-- CHANGED: Allows cookie handling during cross-domain redirects
            Domain = context!.Request.Host.Host, // <-- ADDED: Binds the cookie dynamically to the current tenant domain
            Expires = DateTimeOffset.UtcNow.AddDays (days),
            Path = "/"
        });

    }
}