using Main.Infrastructure;
using Main.Infrastructure.ICrosscuttingServices;
using Main.WebAppCore.Controllers.Extensions;
namespace Main.WebAppCore.Middlewares;

public class TokenRefreshMiddleware
{
    private readonly RequestDelegate _next;

    public TokenRefreshMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (HttpContext context,ITokenService tokenService,ITenantSetter tenantSetter)
    {
        var path = context.Request.Path;

        // 1. CRITICAL: Skip running rotation logic on auth management endpoints completely!
        if ( path.StartsWithSegments ("/Account/Logout") ||
            path.StartsWithSegments ("/Auth/Logout") ||
            path.StartsWithSegments ("/Account/Login") ||
            path.StartsWithSegments ("/Auth/Login") )
        {
            await _next (context);
            return;
        }

        var accessCookieName = $".App.AccessToken.{tenantSetter.ResolvedTenantId}";

        // 2. If already authenticated by the JwtBearer middleware, skip rotation
        if ( context.Request.Cookies.TryGetValue (accessCookieName,out var accessToken) && !string.IsNullOrEmpty (accessToken) )
        {
            await _next (context);
            return;
        }

        var refreshCookieName = $".App.RefreshToken.{tenantSetter.ResolvedTenantId}";

        if ( context.Request.Cookies.TryGetValue (refreshCookieName,out var refreshToken) && !string.IsNullOrEmpty (refreshToken) )
        {
            try
            {
                var rotationResult = await tokenService.RotateRefreshTokenAsync(refreshToken.ToString(), tenantSetter.ResolvedTenantId, 15, 7);

                if ( rotationResult != null )
                {
                    await AuthorizationExtensions.AddTenantRefreshHeaderToken (context,tenantSetter.ResolvedTenantId,rotationResult,15,7);
                }
            }
            catch
            {

            }
        }

        await _next (context);
    }
}