using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Services;
using System.Security.Claims;

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
        var accessJwt = await tokenService.GenerateAccessToken(userId,resolvedTenantId,formatedTenantRole,role,userName,email,minutes,days);

        var refreshTokenStr = tokenService.GenerateRefreshToken();

        // 3. COOKIE 1: Save the short-lived Access JWT (Expires in 15 minutes)
        context.Response.Cookies.Append ($".App.AccessToken.{resolvedTenantId.ToString ()}",
        accessJwt.ToString () ?? "",
        new CookieOptions
        {
            HttpOnly = true,   // Protects against XSS attacks stealing your JWT
            Secure = true,     // Mandates HTTPS through Nginx
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes (minutes),
            Path = "/"         // Accessible by all pages in your app
        });

        // 4. COOKIE 2: Save the long-lived Refresh Token (Expires in 7 days)
        context.Response.Cookies.Append ($".App.RefreshToken.{resolvedTenantId.ToString ()}",refreshTokenStr,new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays (days),
            // FIX: Changed path from "/account/refresh-token" to match your working endpoint route exactly
            Path = "/refresh-token"
        });

    }

    public static void AddUserClaims
    (HttpContext context,string userId,Guid resolvedTenantId,
    string formatedTenantRole,string userName,string email,string userRole)
    {
        List<Claim> listUserClaims =
        [
            new Claim (ClaimTypes.NameIdentifier,userId.ToString()),
            new Claim (ClaimTypes.Role,"User"),
            new Claim ("TenantId",resolvedTenantId.ToString()),
            new Claim("TenantRole",formatedTenantRole),
            new Claim("UserRole",userRole),
            new Claim ("UserName",userName),
            new Claim ("Email",email)
        ];

        // FIX: Pass an authentication type string ("JwtCookie") to the constructor
        ClaimsIdentity claimsIdentity = new(listUserClaims, "JwtCookie");

        context.User.AddIdentity (claimsIdentity);

    }
}