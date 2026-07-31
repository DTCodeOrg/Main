
using Main.Infrastructure;

namespace Main.WebAppCore.Middleware;

public class TenantSecurityMiddleware
{
    private readonly RequestDelegate _next;

    public TenantSecurityMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (HttpContext context,ITenantSetter tenantSetter)
    {
        // Only run this validation boundary check if the user is successfully logged in
        if ( context.User.Identity?.IsAuthenticated == true )
        {
            // 1. Get the TenantId embedded securely inside the user's identity claims matrix
            var userTenantId = context.User.FindFirst("TenantId")?.Value;

            // 2. Get the TenantId matching the active browser proxy URL mapping safely
            var resolvedTenantId = tenantSetter.CurrentTenantId.ToString();

            // 3. ENFORCE ISOLATION
            if ( string.IsNullOrEmpty (userTenantId) ||
                string.IsNullOrEmpty (resolvedTenantId) ||
                !string.Equals (userTenantId,resolvedTenantId,StringComparison.OrdinalIgnoreCase) )
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync ("Access Denied: You do not belong to this tenant space.");
                return;
            }

        }

        await _next (context);
    }
}
