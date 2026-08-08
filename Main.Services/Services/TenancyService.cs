using DataTransferModel;
using Domain.Model;
using Main.Common;
using Main.IRepository;

namespace Main.Services.Services;

public class TenancyService: ITenancyService
{
    public readonly ITenantRepository _tenantRepository;

    public TenancyService (ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<TenantDisplayDataModel> FindHostAsync (string hostName)
    {
        var tenant = await _tenantRepository.FindHostAsync (hostName);

        TenantDisplayDataModel tenantDataModel
        =  new (tenant.TenantId, tenant.TenantName, tenant.Host, tenant.SecretKey );

        if ( tenant.TenantTheme != null )
        {
            TenantTheme tenantTheme = tenant.TenantTheme;

            tenantDataModel.ThemeModel = new TenantThemeModel ()
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
            tenantDataModel.ThemeModel = new TenantThemeModel ()
            {
                // Deep, mysterious evergreen/pine color for headers and primary elements
                PrimaryColor = "#1B3B2B",

                // Soft, glowing moss or misty sage green for buttons and accents
                SecondaryColor = "#728C69",

                // Very soft, off-white tinted with a hint of morning mist/gray to prevent harsh contrast
                BackgroundColor = "#F4F6F4",

                // Clean, natural-feeling sans-serif font stack with a classic fallback
                FontStack = "system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif",

                LogoFileName = ""
            };
        }

        return tenantDataModel;
    }
}
