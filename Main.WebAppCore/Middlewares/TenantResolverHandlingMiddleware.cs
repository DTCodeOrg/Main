using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middleware;

public class TenantResolverHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private const string rootDomain = "localhost";


    public TenantResolverHandlingMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (
    HttpContext context,
    ITenantContext tenantContext,
    ITenantSetter tenantSetter,
    ITenancyService tenancyService,
    IMemoryCache memoryCache,
    ITokenService tokenService,
    ILogger<ExceptionLoggingService> logger)
    {



        Guid resolvedTenantId = await TenantResolutionExtensions.TryResolveTenantAsync(context,tenantContext,tenantSetter,tenancyService,memoryCache,rootDomain,logger);


        // 3. CRITICAL: Store it in HttpContext.Items so it lives for the entire lifecycle of this single request
        context.Items["ResolvedTenantId"] = resolvedTenantId;

        // 4. Log the output via your infrastructure tracing tool right away to verify it worked
        // This resolves the exact line mismatch you saw earlier!
        Serilog.Log.Warning ("TenantResolutionMiddleware resolved host '{Host}' to Tenant ID: {TenantId}",context.Request.Host.Host,resolvedTenantId);

        await _next (context);
    }
}

