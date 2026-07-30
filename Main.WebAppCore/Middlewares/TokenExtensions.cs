namespace Main.WebAppCore.Middlewares;

public static class TenantSafetyCheckExtensions
{
    public static bool CheckContamination (string resolvedTenantId,HttpContext context)
    {
        if ( context.User?.FindFirst ("TenantId") == null )
        {
            return true;
        }

        var tenantId = context.User?.FindFirst ("TenantId")?.Value ?? "";

        if ( resolvedTenantId == tenantId )
        {
            return true;
        }

        return false;
    }
}
