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
        if ( context.Request.Path.StartsWithSegments ("/Account/Logout") ||
            context.Request.Path.StartsWithSegments ("/Auth/Logout") )
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
