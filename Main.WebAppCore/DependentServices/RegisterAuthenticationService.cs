using Main.Infrastructure;
using Main.Infrastructure.ICrosscuttingServices;
using Main.WebAppCore.Controllers.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Main.WebAppCore.DependentServices;

public static class RegisterAuthenticationService
{
    public static IServiceCollection AddAuthentication (this IServiceCollection services,IConfiguration configuration)
    {

        _ = services.AddAuthentication (options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer ("Bearer",options =>
        {
            options.Authority = null;  // No external authority; self-validating
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey (
                    Encoding.UTF8.GetBytes (configuration["Jwt:Key"]!)
                ),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                // Ensure these matching property labels completely reflect what is emitted inside your token generator service payload claims
                RoleClaimType = "UserRole",
                NameClaimType = "UserName"
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = async context =>
                {
                    var tenantSetter = context.HttpContext.RequestServices.GetRequiredService<ITenantSetter>();
                    var tenantId = tenantSetter.CurrentTenantId;

                    var accessCookieName = $".App.AccessToken.{tenantId}";
                    var refreshCookieName = $".App.RefreshToken.{tenantId}";

                    // 1. Check if a valid Access Token exists
                    if ( context.Request.Cookies.TryGetValue (accessCookieName,out var accessToken) && !string.IsNullOrEmpty (accessToken) )
                    {
                        var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();

                        // Validate the token cryptographically and check its lifetime bounds
                        var principal = tokenService.ValidateAndDecryptToken(accessToken, out var validatedToken);
                        if ( principal != null && validatedToken != null && validatedToken.ValidTo > DateTime.UtcNow )
                        {
                            context.Token = accessToken;
                            return; // Access token is alive and well, break out early
                        }
                    }

                    // 2. Access Token failed/expired. Fallback immediately to secure Refresh Token rotation
                    if ( context.Request.Cookies.TryGetValue (refreshCookieName,out var refreshToken) && !string.IsNullOrEmpty (refreshToken) )
                    {
                        var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                        var rotationResult = await tokenService.RotateRefreshTokenAsync(refreshToken, tenantId, 15, 7);

                        if ( rotationResult != null )
                        {
                            // Write the fresh, rotated cookies straight to the response payload
                            await AuthorizationExtensions.AddTenantIsolatedHeaderToken (
                                context.HttpContext,tokenService,
                                context.HttpContext.User.FindFirst (ClaimTypes.NameIdentifier)?.Value ?? "",
                                tenantId,
                                context.HttpContext.User.FindFirst ("UserRole")?.Value ?? "",
                                context.HttpContext.User.FindFirst ("TenantRole")?.Value ?? "",
                                context.HttpContext.User.FindFirst ("UserName")?.Value ?? "",
                                context.HttpContext.User.FindFirst ("Email")?.Value ?? "",
                                15,7
                            );

                            context.Token = rotationResult.AccessToken;
                        }
                    }
                }
            };



        });


        return services;
    }

}
