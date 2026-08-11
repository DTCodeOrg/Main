using Main.Infrastructure;
using Main.Infrastructure.ICrosscuttingServices;
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
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes (configuration["Jwt:Key"]!)),
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
                    //var tenantSetter = context.HttpContext.RequestServices.GetRequiredService<ITenantSetter>();
                    //var accessCookieName = $".App.AccessToken.{tenantSetter.ResolvedTenantId}";
                    //if ( context.Request.Cookies.TryGetValue (accessCookieName,out var accessToken) )
                    //{
                    //    //  FIX: Hand the raw token string directly to the native middleware engine.
                    //    // This will unpack the signature, map your claims, and set context.Success() automatically.

                    //}

                    var tenantSetter = context.HttpContext.RequestServices.GetRequiredService<ITenantSetter>();
                    var accessCookieName = $".App.AccessToken.{tenantSetter.ResolvedTenantId}";
                    if ( context.Request.Cookies.TryGetValue (accessCookieName,out var accessToken) )
                    {
                        var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();

                        // Validate manually 
                        var validateResult = tokenService.ValidateAndDecryptToken (accessToken,out var validatedToken);

                        if ( validateResult != null )
                        {
                            // This tells the framework: "Stop looking, this user is authenticated!"
                            // context.Token = accessToken;
                            context.Principal = validateResult;
                            context.Success ();
                        }
                    }

                    return Task.CompletedTask;
                }
            };
        });


        return services;
    }

}
