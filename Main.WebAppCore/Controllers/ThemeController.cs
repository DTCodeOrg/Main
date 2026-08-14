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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ThemeController (IStorageService storageService,
        ITenantSetter tenantSetter,IThemeService themeService,
        IWebHostEnvironment webHostEnvironment)
    {
        _storageService = storageService;
        _tenantSetter = tenantSetter;
        _themeService = themeService;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
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
    public async Task<IActionResult> UpdateLogo (UpdateLogoViewModel model)
    {
        if ( !ModelState.IsValid )
        {
            return View (model);
        }

        string? fileName = await _storageService.SaveTenantAssetAsync ( _webHostEnvironment, _tenantSetter.ResolvedTenantId, model.LogoFile,"uploads" );

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
