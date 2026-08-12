using Main.Model.Identity;

namespace Main.IRepository;

public interface IThemeRepository
{
    Task<TenantTheme> GetTenantThemeAsync (Guid themeId);

    Task UpdateTenantThemeAsync (TenantTheme theme);
}