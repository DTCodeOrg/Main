using Microsoft.AspNetCore.Authorization;

namespace Main.WebAppCore.Middlewares;

public class TenantRoleRequirementMiddleware: IAuthorizationRequirement
{
    public string AllowedRole
    {
        get;
    }
    public TenantRoleRequirementMiddleware (string allowedRole) => AllowedRole = allowedRole;
}