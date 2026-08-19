using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore.ViewComponents;

public class MainHeaderPanelViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}

