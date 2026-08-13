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

    public async Task InvokeAsync
        (HttpContext context,
        ITenantSetter tenantSetter,
        ITenancyService tenancyService,
        IThemeService themeService,
        IMemoryCache memoryCache,
        ILogger<TenantResolverMiddleware> logger)
    {
        TenantDataModel? resolvedTenant
        = await TenantResolutionExtensions.TryResolveTenantAsync
        ( context, tenancyService, memoryCache );

        TenantThemeModel? tenantTheme =
            await memoryCache.GetOrCreateAsync($"tenant_{resolvedTenant?.ResolvedTenantId}", async entry =>
            {
                _ =  entry.SetSize(1);
                _ =  entry.SetSlidingExpiration(TimeSpan.FromHours(1));
                _ =  entry.SetAbsoluteExpiration(TimeSpan.FromHours(1));

                return await themeService.GetThemeByTenantAsync( resolvedTenant?.ResolvedTenantId ?? Guid.Empty );
            });


        if ( resolvedTenant != null )
        {
            tenantSetter.ResolvedTenantId = resolvedTenant.ResolvedTenantId;
            tenantSetter.CurrentTenant = resolvedTenant;
            tenantSetter.CurrentTenant.ResolvedTenantId = resolvedTenant.ResolvedTenantId;
            context.Items["TenantId"] = resolvedTenant.ResolvedTenantId;

            tenantSetter.CurrentTenant.TenantThemeModel
            = new TenantThemeModel ()
            {
                Default = true,
                PrimaryColor = tenantTheme?.PrimaryColor ?? "#1B3B2B",
                SecondaryColor = tenantTheme?.SecondaryColor ?? "#728C69",
                BackgroundColor = tenantTheme?.BackgroundColor ?? "#F4F6F4",
                FontStack = tenantTheme?.FontStack ?? "system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif",
                LogoFilePath = tenantTheme?.LogoFilePath ?? "~/favicon.ico"
            };
        }

        await _next (context);
    }
}

