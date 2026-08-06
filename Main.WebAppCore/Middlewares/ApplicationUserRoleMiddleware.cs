using Main.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Main.WebAppCore.Middlewares;

public class ApplicationUserRoleMiddleware: AuthorizationHandler<TenantRoleRequirementMiddleware>
{
    private readonly ITenantSetter _tenantSetter;

    // You can safely inject your scoped tenant resolver here
    public ApplicationUserRoleMiddleware (ITenantSetter tenantSetter)
    {
        _tenantSetter = tenantSetter;
    }

    protected override Task HandleRequirementAsync (AuthorizationHandlerContext context,TenantRoleRequirementMiddleware requirement)
    {
        // 1. READ FROM JWT: Extract tenant ownership from the validated JWT claims
        var tokenTenantId = context.User.FindFirst("TenantId")?.Value;

        // 2. READ FROM JWT: Extract role from your custom UserRole claim key
        var tokenUserRole = context.User.FindFirst("UserRole")?.Value;

        // 3. READ FROM REQUEST CONTEXT: Get the current active tenant requested by the URL
        var currentTenantId = _tenantSetter.CurrentTenantId.ToString();

        // 4. CROSS-CHECK EVERYTHING: Secure validation logic
        if ( !string.IsNullOrEmpty (tokenTenantId) &&
            tokenTenantId.Equals (currentTenantId,StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrEmpty (tokenUserRole) &&
            tokenUserRole.Equals (requirement.AllowedRole,StringComparison.OrdinalIgnoreCase) )
        {
            context.Succeed (requirement); // Access granted!
        }

        return Task.CompletedTask;
    }
}