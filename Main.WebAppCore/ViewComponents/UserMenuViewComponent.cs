using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewComponents;

public class UserMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
