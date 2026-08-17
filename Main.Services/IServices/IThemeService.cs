using Main.Common.Models;

namespace Main.Services;

public interface IThemeService
{
    Task<TenantThemeModel?> GetThemeByTenantAsync (Guid tenantId);

    Task<TenantThemeModel> GetTenantThemeAsync (Guid themeId);

    Task UpdateTenantThemeAsync (TenantThemeModel theme);
}