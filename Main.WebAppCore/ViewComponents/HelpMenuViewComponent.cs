using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewComponents;

public class HelpMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
