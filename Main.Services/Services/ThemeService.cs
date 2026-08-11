using Domain.Model;
using Main.Common;
using Main.IRepository;

namespace Main.Services;

public class ThemeService: IThemeService
{
    private readonly IThemeRepository _themeRepository;

    public ThemeService (IThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }

    public async Task<TenantThemeModel> GetTenantThemeAsync (Guid themeId)
    {
        TenantTheme theme = await _themeRepository.GetTenantThemeAsync (themeId);

        TenantThemeModel themeDataModel = new()
        {
            Id = theme.Id,
            PrimaryColor = theme.PrimaryColor,
            SecondaryColor = theme.SecondaryColor,
            BackgroundColor = theme.BackgroundColor,
            FontStack = theme.FontStack,
            LogoFileName = theme.LogoFileName
        };

        return themeDataModel;
    }

    public async Task UpdateTenantThemeAsync (TenantThemeModel theme)
    {
        TenantTheme existingTheme = await _themeRepository.GetTenantThemeAsync(theme.Id);

        existingTheme.PrimaryColor = theme.PrimaryColor;
        existingTheme.SecondaryColor = theme.SecondaryColor;
        existingTheme.BackgroundColor = theme.BackgroundColor;
        existingTheme.FontStack = theme.FontStack;
        existingTheme.LogoFileName = theme.LogoFileName;

        await _themeRepository.UpdateTenantThemeAsync (existingTheme);
    }
}
