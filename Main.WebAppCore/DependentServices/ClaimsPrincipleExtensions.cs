using System.Security.Claims;

namespace Main.WebAppCore.DependentServices;

public static class ClaimsPrincipalExtensions
{
    // Custom overload: checks your formatted tenant claim string instantly
    public static bool IsInTenantRole (this ClaimsPrincipal user,string currentTenantId,string expectedRole)
    {
        if ( user?.Identity?.IsAuthenticated != true )
        {
            return false;
        }

        // Allow GlobalAdmin to bypass everything
        if ( user.IsInRole ("GlobalAdmin") )
        {
            return true;
        }

        var loggedUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if ( string.IsNullOrEmpty (loggedUserId) )
        {
            return false;
        }

        // Reconstruct the exact string format your token uses
        var expectedClaimValue = $"{loggedUserId}:{currentTenantId}:{expectedRole}";

        // Instantly scan the claims array for a match
        return user.HasClaim (c => c.Type == "TenantRole" && c.Value == expectedClaimValue);
    }

    public static string? GetUserId (this ClaimsPrincipal user)
    {
        return user.FindFirst (ClaimTypes.NameIdentifier)?.Value;
    }
}
