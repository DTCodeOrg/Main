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

        // 1. Skip running rotation logic on auth management endpoints completely!
        if ( path.StartsWithSegments ("/Account/Logout") ||
            path.StartsWithSegments ("/Auth/Logout") ||
            path.StartsWithSegments ("/Account/Login") ||
            path.StartsWithSegments ("/Auth/Login") )
        {
            await _next (context);
            return;
        }

        var accessCookieName = $".App.AccessToken.{tenantSetter.ResolvedTenantId}";
        bool hasAccessToken = context.Request.Cookies.TryGetValue(accessCookieName, out var accessToken);

        // 2. FIX: Check if the access token exists AND is cryptographically valid (not expired)
        if ( hasAccessToken && !string.IsNullOrEmpty (accessToken) )
        {
            // Use your service to check the cryptographic signature and expiration status
            var principal = tokenService.ValidateAndDecryptToken(accessToken, out var validatedToken);

            // If the token is valid, safe, and active, continue the request pipeline normally
            if ( principal != null )
            {
                await _next (context);
                return;
            }

            // If principal is null, it means ValidateAndDecryptToken threw an exception (e.g., token expired).
            // The code will naturally bypass this IF statement and fall through to rotate the token!
        }

        var refreshCookieName = $".App.RefreshToken.{tenantSetter.ResolvedTenantId}";

        if ( context.Request.Cookies.TryGetValue (refreshCookieName,out var refreshToken) && !string.IsNullOrEmpty (refreshToken) )
        {
            try
            {
                var rotationResult = await tokenService.RotateRefreshTokenAsync(refreshToken.ToString(), tenantSetter.ResolvedTenantId, 15, 7);

                if ( rotationResult != null )
                {
                    // This appends the fresh new access/refresh tokens to the browser cookie storage response container
                    await AuthorizationExtensions.AddTenantRefreshHeaderToken (context,tenantSetter.ResolvedTenantId,rotationResult,15,7);

                    // CRUCIAL FOR CURRENT REQUEST: Feed the brand-new access token back into the HttpContext 
                    // so the subsequent .AddJwtBearer middleware reads the NEW token instead of the old expired one!
                    context.Items["JwtBearer:Token"] = rotationResult.AccessToken;
                }
            }
            catch
            {
                // Handle logging or clean up invalid refresh tokens here if needed
            }
        }

        await _next (context);
    }
}