using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore.ViewComponents;

public class AdvancedSearchViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
