using Main.WebAppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewComponents;

public class CategoryMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync (MenuObjectModel model)
    {
        return View (model);
    }
}
