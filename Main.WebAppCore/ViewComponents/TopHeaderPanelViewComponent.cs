using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore.ViewComponents;

public class TopHeaderPanelViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
