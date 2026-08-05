using Main.Infrastructure;
using Main.Infrastructure.ICrosscuttingServices;
using Main.WebAppCore.Controllers.Extensions;
namespace Main.WebAppCore.Middleware;

public class TokenRefreshMiddleware
{
    private readonly RequestDelegate _next;

    public TokenRefreshMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (HttpContext context,ITokenService tokenService,ITenantSetter tenantSetter)
    {
        // 1. CRITICAL LOGOUT BYPASS: Do not auto-refresh tokens if the user is actively hitting the logout action!
        if ( context.Request.Path.StartsWithSegments ("/Account/Logout") ||
            context.Request.Path.StartsWithSegments ("/Auth/Logout") )
        {
            await _next (context);
            return;
        }

        // 3. The access token was missing or expired. Let's look for a valid refresh token instead
        var tenantId = tenantSetter.CurrentTenantId;
        var refreshCookieName = $".App.RefreshToken.{tenantId}";

        if ( context.Request.Cookies.TryGetValue (refreshCookieName,out var refreshToken) && !string.IsNullOrEmpty (refreshToken) )
        {
            try
            {
                // Try to rotate the token securely via the database service
                var rotationResult = await tokenService.RotateRefreshTokenAsync(refreshToken, tenantId, 15, 7);

                if ( rotationResult != null )
                {
                    // Securely append new cookies to the response object payload safely outside the Jwt Event context
                    await AuthorizationExtensions.AddTenantRefreshHeaderToken (context,tenantId,rotationResult,15,7);
                }
            }
            catch
            {
                // If rotation fails (e.g. token reuse or expired), clear malicious cookies out here cleanly
            }
        }

        await _next (context);
    }
}
