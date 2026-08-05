using DataTransferModel;
using Main.Common;
using Main.Infrastructure;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middlewares;

public class TenantResolverHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverHandlingMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (
        HttpContext context,
        ITenantSetter tenantSetter,
        ITenancyService tenancyService,
        IMemoryCache memoryCache,
        ILogger<TenantResolverHandlingMiddleware> logger)
    {
        TenantDisplayDataModel? resolvedTenant
        = await TenantResolutionExtensions.TryResolveTenantAsync ( context, tenancyService, memoryCache);

        if ( resolvedTenant != null )
        {
            tenantSetter.CurrentTenantId = resolvedTenant?.MyTenantId ?? Guid.Empty;
            tenantSetter.TenantName = resolvedTenant?.Name ?? string.Empty;
            tenantSetter.TenantStore = resolvedTenant?.StoreType ?? StoreType.FineArts;
            context.Items["TenantId"] = resolvedTenant?.MyTenantId ?? Guid.Empty;
        }
        else
        {
            tenantSetter.CurrentTenantId = Guid.Empty;
            context.Items["TenantId"] = resolvedTenant?.MyTenantId ?? Guid.Empty;
        }

        await _next (context);
    }
}

