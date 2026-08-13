using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.Controllers;

[Authorize (Roles = "Admin")]
public class ThemeController: Controller
{
    private readonly IStorageService _storageService;
    private readonly ITenantSetter _tenantSetter;
    private readonly IThemeService _themeService;

    public ThemeController (IStorageService storageService,
        ITenantSetter tenantSetter,IThemeService themeService)
    {
        _storageService = storageService;
        _tenantSetter = tenantSetter;
        _themeService = themeService;
    }

    [HttpGet]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> UpdateLogo ()
    {
        var theme = await _themeService.GetTenantThemeAsync(_tenantSetter.ResolvedTenantId);

        var viewModel = new UpdateLogoViewModel
        {
            CurrentLogoFileName = theme?.LogoFilePath
        };

        return View (viewModel);
    }

    [HttpPost]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> UpdateLogo (UpdateLogoViewModel model)
    {
        // Server-side validation check
        if ( !ModelState.IsValid )
        {
            return View (model);
        }


        // 1. Save file to physical/cloud location via service
        string? fileName =
                await _storageService.SaveTenantAssetAsync (
                    _tenantSetter.ResolvedTenantId,
                    model.LogoFile,
                    "logos" );

        // 2. Save filename directly to the tenant theme entity
        var theme = await _themeService.GetTenantThemeAsync(_tenantSetter.ResolvedTenantId);

        if ( theme != null )
        {
            theme.LogoFilePath = fileName;
            await _themeService.UpdateTenantThemeAsync (theme);
        }

        return RedirectToAction ("Index","Home",new
        {
            area = ""
        });
    }
}
