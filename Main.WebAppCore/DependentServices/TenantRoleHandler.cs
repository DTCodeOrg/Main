using Main.Infrastructure;
using Main.WebAppCore.DependentServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Main.WebAppCore.DepententServices;

public class TenantRoleHandler: AuthorizationHandler<TenantRoleRequirement>
{
    private readonly ITenantSetter _tenantSetter;

    public TenantRoleHandler (ITenantSetter tenantSetter)
    {
        _tenantSetter = tenantSetter;
    }

    protected override Task HandleRequirementAsync (AuthorizationHandlerContext context,TenantRoleRequirement requirement)
    {
        var user = context.User;

        // 1. Check for Global Admin override immediately to bypass tenant validation restrictions
        if ( user.IsInRole ("GlobalAdmin") )
        {
            context.Succeed (requirement);
            return Task.CompletedTask;
        }

        // 2. FIX: Align string key casing exactly with your "TenantId" JWT configuration payload
        var tokenTenantId = user.FindFirst("TenantId")?.Value;
        var resolvedTenantId = _tenantSetter.CurrentTenantId.ToString();
        var loggedUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // 3. Early exit if the incoming request tenant context doesn't match the token payload
        if ( string.IsNullOrEmpty (tokenTenantId) ||
            !tokenTenantId.Equals (resolvedTenantId,StringComparison.OrdinalIgnoreCase) ||
            string.IsNullOrEmpty (loggedUserId) )
        {
            return Task.CompletedTask; // Fails safely
        }

        // 4. Construct the expected composite evaluation string
        var expectedClaimValue = $"{loggedUserId}:{resolvedTenantId}:{requirement.AllowedRole}";

        // 5. FIX: Use a clean LINQ .Any() lookup to evaluate the claim collection accurately
        bool hasValidTenantRole = user.HasClaim(c => c.Type == "TenantRole" && c.Value == expectedClaimValue);

        if ( hasValidTenantRole && user.IsInRole ("User") )
        {
            context.Succeed (requirement);
        }

        return Task.CompletedTask;
    }
}
