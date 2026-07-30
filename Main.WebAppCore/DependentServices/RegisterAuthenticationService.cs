using Main.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Main.WebAppCore.DependentServices;

public static class RegisterAuthenticationService
{
    public static IServiceCollection AddAuthentication (this IServiceCollection services,IConfiguration configuration)
    {
        // FIX: Pointed directly to "Jwt:Key" to match your TokenService dependency
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
                IssuerSigningKey = new SymmetricSecurityKey (key), // Securely matches TokenService
                ValidateIssuer = false,
                ValidateAudience = false,

                // CRITICAL FIX: Your TokenService emits "UserRole" and "UserName" claims.
                // These must be mapped here so HttpContext.User.IsInRole() reads them correctly.
                RoleClaimType = "UserRole",
                NameClaimType = "UserName",
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var tenantSetter = context.HttpContext.RequestServices.GetRequiredService<ITenantSetter>();

                    if ( tenantSetter?.CurrentTenantId != null )
                    {
                        var cookieName = $".App.AccessToken.{tenantSetter.CurrentTenantId}";

                        if ( context.Request.Cookies.TryGetValue (cookieName,out var token) )
                        {
                            context.Token = token;
                        }
                    }

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}
