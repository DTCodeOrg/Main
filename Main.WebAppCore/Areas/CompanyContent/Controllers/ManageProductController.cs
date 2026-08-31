using DataTransferModel;
using Main.Common.Models;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.Helpers;
using Main.WebAppCore.Models;
using Main.WebAppCore.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.WebAppCore.Controllers;

[Area ("CompanyContent")]
[Authorize (Policy = "TenantAdmin")]
public class ManageProductController: BaseController
{
    private readonly IStorageService _storageService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IProductService _productService;
    private readonly ILogger<ManageProductController> _logger;
    private readonly ITenantSetter _tenantSetter;
    private readonly ITenantCacheService _tenantCacheService;
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUrlHelperFactory _urlHelperFactory;
    private readonly IActionContextAccessor _actionContextAccessor;

    public ManageProductController
    (IProductService productService,
        ILogger<ManageProductController> logger,
        ITenantSetter tenantSetter,
        ITenantCacheService tenantCacheService,
        IWebHostEnvironment webHostEnvironment,
        IStorageService storageService,
        IStringLocalizer<SharedResource> localizer,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor)
    {
        _productService = productService;
        _logger = logger;
        _tenantSetter = tenantSetter;
        _tenantCacheService = tenantCacheService;
        _webHostEnvironment = webHostEnvironment;
        _storageService = storageService;
        _localizer = localizer;
        _urlHelperFactory = urlHelperFactory;
        _actionContextAccessor = actionContextAccessor;
    }


    public async Task<IActionResult> Index ()
    {
        try
        {
            List<ProductDisplayModel> productDataModels = await _productService.GetAllProducts();

            List<ProductDisplayViewModel> displayProductViewModels =
            ProductMapping.MapDisplayProductViewModel  ( productDataModels, _localizer, _tenantSetter );

            return View (displayProductViewModels);
        }
        catch
        {
            return View (new List<ProductDisplayViewModel> ());
        }
    }


    private void SetImageInDataModel (ProductDataModel productDataModel)
    {
        List<ProductFileDataModel> listProductImageFileDataModels = [];

        ProductFileDataModel productImageFileDataModel;

        List<ImageFile>? listSessionImageFiles  = GetAllSessionImages(_tenantCacheService);

        listSessionImageFiles?.ForEach (async sessionImageFile =>
        {
            var fileRelativePath = await _storageService.MoveFileToDestinationFolderAsync
                (sessionImageFile.FileName!, true);

            productImageFileDataModel = new ProductFileDataModel ()
            {
                ProductID = sessionImageFile.PostID ?? 0,
                ProductImageFileID = 0,
                FilePath = sessionImageFile.RelativeFilePath!
            };

            listProductImageFileDataModels.Add (productImageFileDataModel);
        });

        productDataModel.ImageFiles = listProductImageFileDataModels;
    }

    public IActionResult NewProduct ()
    {

        ClearImageFileListSession (_tenantCacheService);

        ProductViewModel objProductViewModel = new()
        {
            AVSubCategory = DropDownListItems.GetSubCategoryList
            (_localizer, _tenantSetter.CurrentTenant.StoreType),

            AVCategory = DropDownListItems.GetCategoryList
            (_localizer, _tenantSetter.CurrentTenant.StoreType),

            PageName = "Product Page"
        };

        return View (objProductViewModel);
    }

    [HttpPost]
    [Authorize (Policy = "TenantAdmin")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> SaveProduct (ProductViewModel collection)
    {
        if ( !ModelState.IsValid )
        {
            Dictionary<string, string[]> validationErrors
            = ModelState.Where
                (ms => ms.Value!.Errors.Count > 0).ToDictionary
                (kvp => kvp.Key,kvp => kvp.Value!.Errors.Select
                (e => e.ErrorMessage).ToArray());

            ViewBag.ValidationSummary = validationErrors;

            return BadRequest (
            new
            {
                success = false,
                message = "Validation failed",
                validationErrors
            });
        }

        try
        {
            ProductDataModel productDataModel = ProductMapping.NewProductDataModel ( collection );

            SetImageInDataModel (productDataModel);

            var result = await _productService.SaveNewProduct( productDataModel );

            var actionContext = _actionContextAccessor.ActionContext;

            if ( actionContext == null )
            {
                return BadRequest (
                new
                {
                    success = false,
                    message = "Validation failed"
                });
            }

            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(actionContext);

            var urlRedirectIndex = urlHelper.Action ("Index", "ManageProduct", new
            {
                Area = "CompanyContent"
            });

            ClearImageFileListSession (_tenantCacheService);

            return Ok (new
            {
                success = result,
                urlGo = urlRedirectIndex
            });
        }
        catch ( Exception ex )
        {
            return BadRequest (new
            {
                success = false,message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult> Edit (int id)
    {
        ClearImageFileListSession (_tenantCacheService);

        ProductDataModel productDataModel = await _productService.GetProductForEditProductID (id);

        ProductViewModel objProductViewModel = ProductMapping.MapProductViewModel
        ( productDataModel, _localizer, _tenantSetter.CurrentTenant.StoreType );

        objProductViewModel.PageName = "Edit Product";

        objProductViewModel.AVCategory = DropDownListItems.GetCategoryList
        (_localizer,_tenantSetter.CurrentTenant.StoreType);

        return View (objProductViewModel);
    }


    [HttpPost]
    [Authorize (Policy = "TenantAdmin")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Edit (ProductViewModel collection)
    {
        if ( !ModelState.IsValid )
        {
            return BadRequest (ModelState);
        }

        try
        {
            ProductDataModel productDataModel = ProductMapping.MapProductDataModel ( collection );

            SetImageInDataModel (productDataModel);

            var result = await _productService.UpdateProduct( productDataModel );

            return Ok (new
            {
                success = true,
                urlGo = Url.Action ("Index","ManageProduct",new
                {
                    Area = "CompanyContent"
                })
            });
        }
        catch ( Exception ex )
        {
            return BadRequest (new
            {
                success = false,
                message = ex.Message
            });
        }
    }


    public async Task<IActionResult> Details (int id)
    {
        try
        {
            ProductDataModel productDataModel = await _productService.GetProductForEditProductID(id);

            ProductViewModel productViewModel = ProductMapping.MapProductViewModel
            ( productDataModel, _localizer, _tenantSetter.CurrentTenant.StoreType );

            productViewModel.PageName = "Product Details";

            return View (productViewModel);
        }
        catch
        {
            return View (new ProductViewModel ());
        }
    }


    [HttpPost]
    public async Task<IActionResult> UploadImage (IFormFile fileInput)
    {
        if ( !ReadImage (fileInput) )
        {
            return PartialView ("_Image",new ImageFile ());
        }

        ImageFile imageFile = await _storageService.SaveSessionFileAsync
        ( _tenantSetter.ResolvedTenantId, _tenantSetter.HttpContextUserId, fileInput, true );

        SetSessionImageFile (imageFile,_tenantCacheService);

        return PartialView ("_NewImage",imageFile);
    }

    private bool ReadImage (IFormFile fileInput)
    {
        if ( fileInput != null && fileInput.FileName != null )
        {
            string extension = Path.GetExtension(fileInput.FileName).ToLower();

            if ( extension.Equals (".jpg") || extension.Equals (".jpeg") ||
                extension.Equals (".png") || extension.Equals (".gif") )
            {
                return true;
            }
        }

        return false;
    }


    [HttpPost]
    [Authorize (Policy = "TenantAdmin")]
    [IgnoreAntiforgeryToken]
    public async Task<JsonResult> ImageRemove (int id = 0,int postId = 0,string? fileName = null)
    {
        try
        {
            string fileNameDeleted = await _productService.DeleteProductImage(id, postId);

            if ( string.IsNullOrEmpty (fileName) )
            {
                fileName = fileNameDeleted;
            }

            bool result = DeleteSessionImage(fileName, _tenantCacheService);

            return Json (new
            {
                success = result
            });
        }
        catch ( Exception ex )
        {
            return Json (new
            {
                success = false,message = ex.Message
            });
        }
    }



    [HttpGet]
    public async Task<IActionResult> Delete (int id)
    {
        try
        {
            ProductViewModel productViewModel = new ();
            productViewModel.ProductID = id;

            return View (productViewModel);
        }
        catch
        {
            return BadRequest (new
            {
                success = false
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct (int id,int fakeId)
    {
        try
        {
            bool result = await _productService.DeleteProduct(id);

            if ( result )
            {
                return RedirectToAction ("Index");
            }

            return RedirectToAction ("Delete",new
            {
                id = id
            });
        }
        catch
        {
            return BadRequest (new
            {
                success = false
            });
        }
    }
}