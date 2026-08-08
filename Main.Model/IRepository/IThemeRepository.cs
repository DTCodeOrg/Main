using Domain.Model;

namespace Main.IRepository;

public interface IThemeRepository
{
    Task<TenantTheme> GetTenantThemeAsync (Guid themeId);

    Task UpdateTenantThemeAsync (TenantTheme theme);
}