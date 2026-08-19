using Main.Infrastructure;

namespace Main.WebAppCore.Middlewares;

public interface ITenantAssetResolver
{
    string GetLogoUrl ();
}

public class TenantAssetResolver: ITenantAssetResolver
{
    private readonly ITenantSetter _tenantSetter;

    public TenantAssetResolver (ITenantSetter tenantSetter)
    {
        _tenantSetter = tenantSetter;
    }

    public string GetLogoUrl ()
    {
        // Fallback to a default logo if tenant or theme logo isn't set yet
        if ( _tenantSetter.CurrentTenant.TenantThemeModel?.LogoRelativeFilePath == null )
        {
            return "~/favicon.ico";
        }

        // Return the clean, resolved path
        return $"~/uploads/{_tenantSetter.ResolvedTenantId.ToString ()}/logos/{_tenantSetter.CurrentTenant.TenantThemeModel?.LogoRelativeFilePath!}";
    }
}
