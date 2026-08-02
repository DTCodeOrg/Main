using Main.Infrastructure;
using Main.Infrastructure.ICrosscuttingServices;
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
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
.AddJwtBearer (options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey (key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,

        // Ensure these matching property labels completely reflect what is emitted inside your token generator service payload claims
        RoleClaimType = "UserRole",
        NameClaimType = "UserName"
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = async context =>
        {
            // 1. Resolve your active scoped tenant setting container instance on this execution thread
            var tenantSetter = context.HttpContext.RequestServices.GetRequiredService<ITenantSetter>();
            var tenantId = tenantSetter.CurrentTenantId;

            // 2. Build the exact dynamic string names matching your login endpoint configurations
            var accessCookieName = $".App.AccessToken.{tenantId}";
            var refreshCookieName = $".App.RefreshToken.{tenantId}";

            // 3. Attempt to authenticate the incoming thread with the Access Token cookie first
            if ( context.Request.Cookies.TryGetValue (accessCookieName,out var accessToken) && !string.IsNullOrEmpty (accessToken) )
            {
                context.Token = accessToken;
            }
            else
            {
                // 4. Access Token is dead or missing! Process your secure database refresh token chain instead
                if ( context.Request.Cookies.TryGetValue (refreshCookieName,out var refreshToken) && !string.IsNullOrEmpty (refreshToken) )
                {
                    var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                    var rotationResult = await tokenService.RotateRefreshTokenAsync(refreshToken, tenantId, 15, 7);

                    if ( rotationResult != null )
                    {
                        // Drop the brand-new rotated cookie tokens into the response headers
                        context.HttpContext.Response.Cookies.Append (accessCookieName,rotationResult.AccessToken,new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Lax,
                            Expires = DateTimeOffset.UtcNow.AddMinutes (15),
                            Domain = context.HttpContext.Request.Host.Host,
                            Path = "/"
                        });

                        context.HttpContext.Response.Cookies.Append (refreshCookieName,rotationResult.RefreshToken,new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Lax,
                            Expires = DateTimeOffset.UtcNow.AddDays (7),
                            Domain = context.HttpContext.Request.Host.Host,
                            Path = "/"
                        });

                        // Instantly authorize the current request with the newly issued access key
                        context.Token = rotationResult.AccessToken;
                    }
                }
            }
        }
    };
});


        return services;
    }

}
