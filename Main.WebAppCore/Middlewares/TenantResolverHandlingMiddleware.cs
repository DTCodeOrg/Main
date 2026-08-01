using DataTransferModel;
using Main.Common;
using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
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
    ITenantContext tenantContext,
    ITenantSetter tenantSetter,
    ITenancyService tenancyService,
    IMemoryCache memoryCache,
    ITokenService tokenService,
    ILogger<ExceptionLoggingService> logger)
    {



        var tenantDisplayDataModel = await TenantResolutionExtensions.TryResolveTenantAsync (context,tenantContext,tenantSetter,tenancyService,memoryCache,rootDomain,logger);


        // 3. CRITICAL: Store it in HttpContext.Items so it lives for the entire lifecycle of this single request
        context.Items["ResolvedTenantId"] = tenantDisplayDataModel?.MyTenantId;

        // 4. Log the output via your infrastructure tracing tool right away to verify it worked
        // This resolves the exact line mismatch you saw earlier!
        Serilog.Log.Warning ("TenantResolutionMiddleware resolved host '{Host}' to Tenant ID: {TenantId}",context.Request.Host.Host,tenantDisplayDataModel?.MyTenantId);

        SetTenantSetter (tenantSetter,tenantDisplayDataModel);

        context.Request.Headers[TenantHeaderKey] = tenantDisplayDataModel?.MyTenantId.ToString () ?? string.Empty;

        await _next (context);
    }

    private void SetTenantSetter (ITenantSetter tenantSetter,TenantDisplayDataModel? tenantDisplayDataModel)
    {
        tenantSetter.CurrentTenantId = tenantDisplayDataModel?.MyTenantId ?? Guid.Empty;
        tenantSetter.TenantStore = tenantDisplayDataModel?.StoreType ?? StoreType.FineArts;
        tenantSetter.TenantName = tenantDisplayDataModel?.Name ?? string.Empty;
    }
}

