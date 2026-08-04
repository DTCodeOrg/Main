using DataTransferModel;
using Main.Common;
using Main.Infrastructure;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middleware;

public class TenantResolverHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private const string rootDomain = "localhost";
    private const string TenantHeaderKey = "X-Tenant-ID";

    public TenantResolverHandlingMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (
    HttpContext context,
    ITenantSetter tenantSetter, // This object reference must NEVER be overwritten via "="
    ITenancyService tenancyService,
    IMemoryCache memoryCache,
    ILogger<TenantResolverHandlingMiddleware> logger)
    {
        // 1. Resolve tenant context data into a separate variable payload reference
        TenantDisplayDataModel? resolvedTenant = await TenantResolutionExtensions.TryResolveTenantAsync (
        context, tenancyService, memoryCache, logger
    );

        if ( resolvedTenant != null )
        {
            // 2. CRITICAL: Mutate the properties of the container-managed instance directly
            tenantSetter.CurrentTenantId = resolvedTenant?.MyTenantId ?? Guid.Empty;
            tenantSetter.TenantName = resolvedTenant?.Name ?? string.Empty;
            tenantSetter.TenantStore = resolvedTenant?.StoreType ?? StoreType.FineArts;
        }
        else
        {
            // Handle unresolvable host domains gracefully
            tenantSetter.CurrentTenantId = Guid.Empty;
        }

        await _next (context);
    }
}

