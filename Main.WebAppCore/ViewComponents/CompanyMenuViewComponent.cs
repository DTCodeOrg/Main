using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewComponents;

public class CompanyMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
