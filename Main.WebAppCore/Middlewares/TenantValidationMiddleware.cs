using Main.Infrastructure;

namespace Main.WebAppCore.Middleware;

public class TenantValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TenantValidationMiddleware (RequestDelegate next) => _next = next;

    public async Task InvokeAsync (HttpContext context)
    {
        if ( context.User.Identity?.IsAuthenticated == true )
        {
            // Verify token tenant claims match the active routing tenant frame
            var tokenTenantId = context.User.FindFirst("TenantId")?.Value;
            var tenantSetter = context.RequestServices.GetRequiredService<ITenantSetter>();

            if ( tokenTenantId != tenantSetter.CurrentTenantId.ToString () )
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync ("Cross-tenant identity access denied.");
                return;
            }
        }

        await _next (context);
    }
}
