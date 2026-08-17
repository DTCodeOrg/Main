using Main.Common.Models;
using Main.Infrastructure;
using Main.IRepository;
using Main.Model.Identity;

namespace Main.Services;

public class ThemeService: IThemeService
{
    private readonly IThemeRepository _themeRepository;
    private readonly ITenantSetter _tenantSetter;

    public ThemeService (IThemeRepository themeRepository,ITenantSetter tenantSetter)
    {
        _themeRepository = themeRepository;
        _tenantSetter = tenantSetter;
    }

    public async Task<TenantThemeModel?> GetThemeByTenantAsync (Guid tenantId)
    {
        TenantTheme theme = await _themeRepository.GetThemeByTenantAsync (tenantId);

        TenantThemeModel themeDataModel = new ()
        {
            Id = theme.Id,
            FontStack = theme.FontStack,
            LogoFilePath = theme.LogoFilePath,
            TenantId = theme.TenantId
        };

        return themeDataModel;
    }

    public async Task<TenantThemeModel> GetTenantThemeAsync (Guid tenantId)
    {
        TenantTheme themeEntity = await _themeRepository.GetTenantThemeAsync (tenantId);

        TenantThemeModel themeDataModel = new()
        {
            TenantId = themeEntity.TenantId ,
            BodyBackgroundColor = themeEntity.BodyBackgroundColor,
            BodyColor = themeEntity.BodyColor,
            HeaderColor = themeEntity.HeaderColor,
            LogoColor = themeEntity?.LogoColor,
            MenuBackgroundColor  =themeEntity.MenuBackgroundColor,
            MenuItemHoverBGColor = themeEntity.MenuItemHoverBGColor,
            MenuItemHoverColor = themeEntity.MenuItemHoverColor,
            FontStack = themeEntity.FontStack,
            LogoFilePath = themeEntity.LogoFilePath
        };

        return themeDataModel;
    }

    public async Task UpdateTenantThemeAsync (TenantThemeModel theme)
    {
        TenantTheme existingTheme =
        await _themeRepository.GetThemeByTenantAsync(_tenantSetter.ResolvedTenantId);

        existingTheme.FontStack = theme.FontStack;
        existingTheme.LogoFilePath = theme.LogoFilePath;
        existingTheme.TenantId = _tenantSetter.ResolvedTenantId;

        await _themeRepository.UpdateTenantThemeAsync (existingTheme);
    }
}
