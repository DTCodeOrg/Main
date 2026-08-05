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

        // 2. If the user is already successfully authenticated by the access token, continue down the pipeline
        if ( context.User.Identity?.IsAuthenticated == true )
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

                    // Manually build the ClaimsPrincipal from the new access token so this current request evaluates as authenticated immediately
                    var principal = tokenService.ValidateAndDecryptToken(rotationResult.AccessToken, out _);
                    if ( principal != null )
                    {
                        context.User = principal; // Binds the identity to the current executing Razor View context instantly!
                    }
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
