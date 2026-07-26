
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

            // 2. Get the TenantId matching the active browser Nginx proxy URL mapping
            var resolvedTenantId = tenantSetter.CurrentTenantId.ToString();

            // 3. FIX: ENFORCE ISOLATION (Blocks requests where the claim DOES NOT match the current route domain)
            if ( string.IsNullOrEmpty (userTenantId) || !string.Equals (userTenantId,resolvedTenantId,StringComparison.OrdinalIgnoreCase) )
            {
                // Set status code to 403 Forbidden
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "text/plain";

                await context.Response.WriteAsync ("Access Denied: You do not belong to this tenant space.");
                return; // Short-circuit and stop the request pipeline immediately
            }
        }

        await _next (context);
    }
}
