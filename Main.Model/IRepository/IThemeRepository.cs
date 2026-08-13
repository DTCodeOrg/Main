using Main.Model.Identity;

namespace Main.IRepository;

public interface IThemeRepository
{
    Task<TenantTheme?> GetThemeByTenantAsync (Guid tenantId);

    Task<TenantTheme> GetTenantThemeAsync (Guid tenantId);

    Task UpdateTenantThemeAsync (TenantTheme theme);
}