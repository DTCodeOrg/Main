using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.Controllers;

[Authorize (Policy = "TenantAdmin")]
public class ThemeController: Controller
{
    private readonly IStorageService _storageService;
    private readonly ITenantSetter _tenantSetter;
    private readonly IThemeService _themeService;

    public ThemeController (IStorageService storageService,
        ITenantSetter tenantSetter,IThemeService themeService,
        IWebHostEnvironment webHostEnvironment)
    {
        _storageService = storageService;
        _tenantSetter = tenantSetter;
        _themeService = themeService;
    }

    [HttpGet]
    public async Task<IActionResult> UpdateLogo ()
    {
        var themeDataModel
            = await _themeService.GetTenantThemeAsync(_tenantSetter.ResolvedTenantId);

        var logoViewModel = new UpdateLogoViewModel
        {
            CurrentLogoFileName = themeDataModel?.LogoRelativeFilePath
        };
        ViewData["LogoPath"] = "";
        return View (logoViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateLogo (IFormFile logoFile)
    {
        if ( logoFile == null )
        {
            return View ();
        }

        var logoRelativeFilePath  =
            await _storageService.SaveTenantLogoAsync ( _tenantSetter.ResolvedTenantId,logoFile );

        var themeDataModel =
            await _themeService.GetTenantThemeAsync ( _tenantSetter.ResolvedTenantId );

        if ( themeDataModel != null )
        {
            themeDataModel.LogoRelativeFilePath = logoRelativeFilePath;

            await _themeService.UpdateTenantThemeAsync (themeDataModel);
        }

        ViewData["LogoPath"] = logoRelativeFilePath;

        return View ();

    }
}