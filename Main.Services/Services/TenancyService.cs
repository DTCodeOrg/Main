using Main.Common;
using Main.IRepository;
using Main.Model.Identity;

namespace Main.Services.Services;

public class TenancyService: ITenancyService
{
    public readonly ITenantRepository _tenantRepository;

    public TenancyService (ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<TenantDataModel> FindHostAsync (string hostName)
    {
        Tenant? tenant = await _tenantRepository.FindHostAsync (hostName);

        if ( tenant == null )
        {
            throw new InvalidOperationException ("Tenant not found");
        }

        TenantDataModel tenantDataModel =
        new (tenant.TenantId, tenant.TenantName, tenant.Host, tenant.SecretKey );

        tenantDataModel.ResolvedTenantId = tenant.TenantId;

        if ( tenant.TenantTheme != null )
        {
            TenantTheme tenantTheme = tenant.TenantTheme;

            tenantDataModel.TenantThemeModel = new TenantThemeModel ()
            {
                PrimaryColor = tenantTheme.PrimaryColor,
                SecondaryColor = tenantTheme.SecondaryColor,
                BackgroundColor = tenantTheme.BackgroundColor,
                FontStack = tenantTheme.FontStack,
                LogoFileName = tenantTheme.LogoFileName
            };
        }
        else
        {
            tenantDataModel.TenantThemeModel = new TenantThemeModel ()
            {
                PrimaryColor = "#1B3B2B",
                SecondaryColor = "#728C69",
                BackgroundColor = "#F4F6F4",
                FontStack = "system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif",
                LogoFileName = ""
            };
        }

        return tenantDataModel;
    }
}
