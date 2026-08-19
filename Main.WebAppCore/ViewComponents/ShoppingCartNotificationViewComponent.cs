using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewComponents;

public class ShoppingCartNotificationViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
