using Main.Common;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middlewares;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (HttpContext context,ITenantSetter tenantSetter,ITenancyService tenancyService,IMemoryCache memoryCache,ILogger<TenantResolverMiddleware> logger)
    {
        TenantDataModel? resolvedTenant
        = await TenantResolutionExtensions.TryResolveTenantAsync ( context, tenancyService, memoryCache);

        if ( resolvedTenant != null )
        {
            tenantSetter.ResolvedTenantId = resolvedTenant.ResolvedTenantId;
            tenantSetter.CurrentTenant = resolvedTenant;
            tenantSetter.CurrentTenant.ResolvedTenantId = resolvedTenant.ResolvedTenantId;
            context.Items["TenantId"] = resolvedTenant.ResolvedTenantId;

            tenantSetter.CurrentTenant.TenantThemeModel =
                new TenantThemeModel ()
                {
                    Default = true,
                    PrimaryColor = resolvedTenant.TenantThemeModel.PrimaryColor,
                    SecondaryColor = resolvedTenant.TenantThemeModel.SecondaryColor,
                    BackgroundColor = resolvedTenant.TenantThemeModel.BackgroundColor,
                    FontStack = resolvedTenant.TenantThemeModel.FontStack,
                    LogoFileName = resolvedTenant.TenantThemeModel.LogoFileName
                };
        }

        await _next (context);
    }
}

