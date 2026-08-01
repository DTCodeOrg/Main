using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Main.WebAppCore.DependentServices;

public static class RegisterAuthenticationService
{
    public static IServiceCollection AddAuthentication (this IServiceCollection services,IConfiguration configuration)
    {
        var secretKey = configuration["Jwt:Key"];

        if ( string.IsNullOrEmpty (secretKey) )
        {
            throw new InvalidOperationException ("JWT Signing Key ('Jwt:Key') is missing from the configuration system.");
        }

        var key = Encoding.UTF8.GetBytes(secretKey);

        _ = services.AddAuthentication (options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer (options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey (key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RoleClaimType = "UserRole",
                NameClaimType = "UserName",
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = async context =>
                {
                    try
                    {
                        var tenantSetter = context.HttpContext.RequestServices.GetRequiredService<ITenantSetter>();
                        var tenantId = tenantSetter.CurrentTenantId;

                        var accessCookieName = $".App.AccessToken.{tenantId}";
                        var refreshCookieName = $".App.RefreshToken.{tenantId}";

                        // 1. Try to authenticate with the existing Access Token
                        if ( context.Request.Cookies.TryGetValue (accessCookieName,out var accessToken) && !string.IsNullOrEmpty (accessToken) )
                        {
                            context.Token = accessToken;
                        }
                        else
                        {
                            // 2. Access token is missing/expired. Try the Refresh Token
                            if ( context.Request.Cookies.TryGetValue (refreshCookieName,out var refreshToken) && !string.IsNullOrEmpty (refreshToken) )
                            {
                                var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                                var rotationResult = await tokenService.RotateRefreshTokenAsync(refreshToken, tenantId, 15, 7);

                                if ( rotationResult != null )
                                {
                                    // 3. Drop the fresh Access Token Cookie
                                    context.HttpContext.Response.Cookies.Append (accessCookieName,rotationResult.AccessToken,new CookieOptions
                                    {
                                        HttpOnly = true,
                                        Secure = true,
                                        SameSite = SameSiteMode.Strict,
                                        Expires = DateTimeOffset.UtcNow.AddMinutes (15),
                                        Path = "/"
                                    });

                                    // CRITICAL FIX: Changed Path from "/refresh-token" to "/"
                                    // This ensures the browser sends the refresh token on ANY page load when access token is dead.
                                    context.HttpContext.Response.Cookies.Append (refreshCookieName,rotationResult.RefreshToken,new CookieOptions
                                    {
                                        HttpOnly = true,
                                        Secure = true,
                                        SameSite = SameSiteMode.Strict,
                                        Expires = DateTimeOffset.UtcNow.AddDays (7),
                                        Path = "/"
                                    });

                                    // 4. Authenticate this current request instantly
                                    context.Token = rotationResult.AccessToken;
                                }
                            }
                        }
                    }
                    catch ( Exception ex )
                    {
                        // Fail-safe: Log error and let request drop through as Anonymous instead of throwing a 500 error
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogError (ex,"Error executing silent token rotation middleware.");
                    }
                }
            };
        });

        return services;
    }

}
