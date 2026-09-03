using DataTransferModel;
using Main.Common;
using Main.Common.Models;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.Models;
using Main.WebAppCore.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.WebAppCore.Controllers;

[Area ("PageContent")]
[Authorize (Policy = "TenantAdmin")]
public class PagesController: BaseController
{
    private readonly IPageService _pageService;
    private readonly ILogger<PagesController> _logger;
    private readonly ITenantSetter _tenantSetter;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public PagesController (
      IPageService pageDataService,
      ITenantSetter tenantSetter,
      ILogger<PagesController> logger,
      IStringLocalizer<SharedResource> localizer
    )
    {
        _pageService = pageDataService;
        _logger = logger;
        _tenantSetter = tenantSetter;
        _localizer = localizer;
    }


    [Authorize (Policy = "TenantAdmin")]
    public async Task<IActionResult> Index ()
    {
        try
        {
            List<PageDisplayDataModel> listPageDataModel
            = await _pageService.GetAllPages(_tenantSetter.CurrentTenant.TenantName);

            List<PageDisplayViewModel> listPageViewModel
            = PageMapping.PageDisplayMapping ( listPageDataModel, _tenantSetter.CurrentTenant.TenantName );

            return View (listPageViewModel);
        }
        catch ( Exception ex )
        {
            {
                return BadRequest (ex.Message);
            }
        }
    }


    [Authorize (Policy = "TenantAdmin")]
    public async Task<IActionResult> NewProductPanel (int id)
    {
        PanelViewModel pagePanelViewModel = new ();

        List<PostDataModel> listSelectProductsDataModel =
        await _pageService.GetSelectProducts ();

        pagePanelViewModel.ListSelectPosts = PageMapping.MapSelectPostViewModel (listSelectProductsDataModel,AppSettings.Current.EnumCurrency,_localizer,_tenantSetter.CurrentTenant.StoreType);

        pagePanelViewModel.PageID = id;
        pagePanelViewModel.PanelTitle = "";

        return View (pagePanelViewModel);
    }


    [HttpPost]
    [IgnoreAntiforgeryToken]
    [Authorize (Policy = "TenantAdmin")]
    public async Task<IActionResult> SaveNewProductPanel ([FromBody] LocalModel model)
    {
        if ( model == null )
        {
            return Json (new
            {
                success = false,
                message = "model is null"
            });
        }

        try
        {
            PanelDataModel pagePanelDataModel =
                new ( ( EnumPanelTemplate ) model.TemplateTypeID,
                model.PageID, model.PanelTitle );

            List<PostDataModel> listReferencePosts
                = await _pageService.GetSelectProducts ( );

            List<PostDataModel> listUserSelectedPosts = [];

            listUserSelectedPosts = listReferencePosts.Where (obj =>
            {
                return model.Numbers.Contains (obj.PanelPostID);
            }).ToList ();

            listUserSelectedPosts.ForEach (selectedPost =>
            {
                pagePanelDataModel.CreatePost (selectedPost);
            });

            bool result = await _pageService.CreateNewPanel ( pagePanelDataModel );

            return Json (new
            {
                success = result,
                receivedUrl = Url.Action ("Index","Pages",new
                {
                    Area = "PageContent"
                })
            });
        }
        catch ( Exception ex )
        {
            return Json (new
            {
                success = false,
                message = ex.Message
            });
        }
    }


    public async Task<IActionResult> PreviewPageContent (int id)
    {
        PageDataModel pagePanelDataModel = await _pageService.GetPageDataModel(id);

        PageViewModel pageViewModel = PageMapping.MapPageViewModel ( pageDataModel: pagePanelDataModel );

        return View (pageViewModel.ListPagePanels.ToList ());
    }


    public async Task<IActionResult> EditPageContent (int id)
    {
        PageDataModel pagePanelDataModel = await _pageService.GetPageDataModel(id);

        PageViewModel pageViewModel = PageMapping.MapPageViewModel ( pagePanelDataModel );

        return View (pageViewModel.ListPagePanels.ToList ());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> UpdatePositions ([FromBody] List<PanelPositionDataModel> listPanelPositionDataModel)
    {
        if ( listPanelPositionDataModel.Count == 0 )
        {
            return BadRequest (new
            {
                success = false,
                error = "Payload is empty or invalid!"
            });
        }

        int pageId = listPanelPositionDataModel.Last().PageID;

        try
        {
            foreach ( var _ in listPanelPositionDataModel.Where (item => item == null).Select (item => new { }) )
            {
                return Json (new
                {
                    success = false,
                    error = "Validation failed!"
                });
            }

            BaseDataModel baseDataModel = _tenantSetter.CreateMetaData;

            bool result = await _pageService.UpdatePanelsOrderAsync ( listPanelPositionDataModel, baseDataModel, pageId );

            return Json (new
            {
                success = result
            });
        }
        catch ( Exception ex )
        {
            _logger.LogWarning (ex,"Error updating panel positions");
            return Json (new
            {
                success = false,
                error = ex.Message
            });
        }
    }



    [HttpDelete]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> DeletePanel (int panelId,int pageId)
    {
        try
        {
            bool result = await _pageService.DeletePanelAsync ( panelId );

            return Json (new
            {
                success = result,
                receivedUrl = Url.Action ("EditPageContent","Pages",new
                {
                    Area = "PageContent",
                    id = pageId
                })
            });
        }
        catch ( Exception ex )
        {
            return Json (new
            {
                success = false,
                error = ex.Message
            });
        }
    }
}