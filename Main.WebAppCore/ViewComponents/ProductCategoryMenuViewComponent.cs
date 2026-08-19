using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewComponents;

public class ProductCategoryMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
