using Main.Common;

namespace Main.Services;

public interface IThemeService
{
    Task<TenantThemeModel> GetTenantThemeAsync (Guid themeId);

    Task UpdateTenantThemeAsync (TenantThemeModel theme);
}