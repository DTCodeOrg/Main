using Main.Common;
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
        TenantTheme? theme = await _themeRepository.GetThemeByTenantAsync (tenantId);

        if ( theme == null )
        {
            return null;
        }

        TenantThemeModel themeDataModel = new ()
        {
            Id = theme.Id,
            PrimaryColor = theme.PrimaryColor,
            SecondaryColor = theme.SecondaryColor,
            BackgroundColor = theme.BackgroundColor,
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
            Id = themeEntity.Id,
            PrimaryColor = themeEntity.PrimaryColor,
            SecondaryColor = themeEntity.SecondaryColor,
            BackgroundColor = themeEntity.BackgroundColor,
            FontStack = themeEntity.FontStack,
            LogoFilePath = themeEntity.LogoFilePath,
            TenantId = themeEntity.TenantId
        };

        return themeDataModel;
    }

    public async Task UpdateTenantThemeAsync (TenantThemeModel theme)
    {
        TenantTheme existingTheme =
        await _themeRepository.GetThemeByTenantAsync(_tenantSetter.ResolvedTenantId);

        existingTheme.PrimaryColor = theme.PrimaryColor;
        existingTheme.SecondaryColor = theme.SecondaryColor;
        existingTheme.BackgroundColor = theme.BackgroundColor;
        existingTheme.FontStack = theme.FontStack;
        existingTheme.LogoFilePath = theme.LogoFilePath;
        existingTheme.TenantId = _tenantSetter.ResolvedTenantId;

        await _themeRepository.UpdateTenantThemeAsync (existingTheme);
    }
}
