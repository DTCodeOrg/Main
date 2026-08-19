using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewComponents;

public class LanguageMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
