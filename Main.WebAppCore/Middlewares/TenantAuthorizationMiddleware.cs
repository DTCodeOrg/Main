
using Microsoft.AspNetCore.Authorization;

namespace Main.WebAppCore.Middlewares;

public static class TenantAuthorizationMiddleware
{
    public static IServiceCollection AddAuthorizations (this IServiceCollection services,IConfiguration configuration)
    {
        _ = services.AddScoped<IAuthorizationHandler,ApplicationUserRoleMiddleware> ();

        _ = services.AddAuthorization (options =>
        {
            options.AddPolicy ("TenantAdmin",policy => policy.Requirements.Add (new TenantRoleRequirementMiddleware ("Admin")));

            options.AddPolicy ("TenantContentManager",policy => policy.Requirements.Add (new TenantRoleRequirementMiddleware ("ContentManager")));

            options.AddPolicy ("TenantMember",policy => policy.Requirements.Add (new TenantRoleRequirementMiddleware ("Member")));
        });

        return services;
    }
}
