using Main.Infrastructure;
using Main.WebAppCore.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Main.WebAppCore.Middlewares;

public static class AuthenticationMiddleware
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
            options.Authority = null;
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
                RoleClaimType = "UserRole",
                NameClaimType = "UserName"
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var tenantSetter = context.HttpContext.RequestServices.GetRequiredService<ITenantSetter>();

                    var accessCookieName = $".App.AccessToken.{tenantSetter.ResolvedTenantId.ToString()}";

                    if ( context.Request.Cookies.TryGetValue (accessCookieName,out var accessToken) )
                    {
                        // Just hand the token to the native engine. Let it handle validation!
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };

        });


        return services;
    }

}
