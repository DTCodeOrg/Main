
using DataTransferModel;
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
        TenantDisplayDataModel? resolvedTenant
        = await TenantResolutionExtensions.TryResolveTenantAsync ( context, tenancyService, memoryCache);

        if ( resolvedTenant != null )
        {
            tenantSetter.ResolvedTenantId = resolvedTenant.MyTenantId;
            tenantSetter.CurrentTenant.TenantName = resolvedTenant.TenantName;
            context.Items["TenantId"] = resolvedTenant.MyTenantId;

            tenantSetter.CurrentTenant.ThemeModel =
                new TenantThemeModel ()
                {
                    Default = true,
                    PrimaryColor = resolvedTenant.ThemeModel.PrimaryColor,
                    SecondaryColor = resolvedTenant.ThemeModel.SecondaryColor,
                    BackgroundColor = resolvedTenant.ThemeModel.BackgroundColor,
                    FontStack = resolvedTenant.ThemeModel.FontStack,
                    LogoFileName = resolvedTenant.ThemeModel.LogoFileName
                };
        }

        await _next (context);
    }
}

