using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewComponents;

public class AdminMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ()
    {
        return View ();
    }
}
