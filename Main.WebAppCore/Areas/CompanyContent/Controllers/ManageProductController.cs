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
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public ManageProductController (IProductService productService,
        ILogger<ManageProductController> logger,
        ITenantSetter tenantSetter,
        ITenantCacheService tenantCacheService,
        IWebHostEnvironment webHostEnvironment,
        IStorageService storageService,
        IStringLocalizer<SharedResource> localizer)
    {
        _productService = productService;
        _logger = logger;
        _tenantSetter = tenantSetter;
        _tenantCacheService = tenantCacheService;
        _webHostEnvironment = webHostEnvironment;
        _storageService = storageService;
        _localizer = localizer;
    }


    public async Task<IActionResult> Index ()
    {
        try
        {
            List<ProductDisplayModel> productDataModels = await _productService.GetAllProducts();

            List<ProductDisplayViewModel> displayProductViewModels = ProductMapping.MapDisplayProductViewModel
                ( productDataModels );

            return View (displayProductViewModels);
        }
        catch
        {
            return View (new List<ProductDisplayViewModel> ());
        }
    }


    private void SetImageInDataModel (ProductDataModel productDataModel)
    {
        List<ProductFileDataModel> listProductImageFileDataModels
            = new();

        ProductFileDataModel productImageFileDataModel;

        List<ImageFile>? listSessionImageFiles = GetAllSessionImages(_tenantCacheService);

        listSessionImageFiles?.ForEach (async sessionImageFile =>
        {
            var fileRelativePath  =
                    await _storageService.MoveFileToDestinationFolderAsync (
                    sessionImageFile.FileName, true);

            productImageFileDataModel = new ProductFileDataModel ()
            {
                ProductID = sessionImageFile.PostID ?? 0,
                ProductImageFileID = 0,
                FilePath = sessionImageFile.RelativeFilePath
            };

            listProductImageFileDataModels.Add (productImageFileDataModel);
        });

        productDataModel.ImageFiles = listProductImageFileDataModels;

        ClearImageFileListSession (_tenantCacheService);
    }

    public IActionResult NewProduct ()
    {

        ClearImageFileListSession (_tenantCacheService);

        ProductViewModel objProductViewModel = new ()
        {
            AVCategory = DropDownListItems.GetCategoryList (_localizer),
            AVSubCategory = DropDownListItems.GetSubCategoryList (_localizer),
            PageName = "Product Page"
        };

        return View (objProductViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveProduct (ProductViewModel collection)
    {
        if ( !ModelState.IsValid )
        {
            return BadRequest (ModelState);
        }

        try
        {
            ProductDataModel productDataModel = ProductMapping.NewProductDataModel ( collection );

            SetImageInDataModel (productDataModel);

            var result = await _productService.SaveNewProduct(productDataModel);

            string? redirectUrl = Url.Action("Index", "ManageProduct", new
            {
                Area = "CompanyContent"
            });

            return Ok (new
            {
                success = result,urlGo = redirectUrl
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
        try
        {
            ClearImageFileListSession (_tenantCacheService);

            ProductDataModel productDataModel = await _productService.GetProductForEditProductID(id);

            ProductViewModel productViewModel = ProductMapping.MapProductViewModel ( productDataModel );

            productViewModel.PageName = "Edit Product";

            return View (productViewModel);
        }
        catch
        {
            return View (new ProductViewModel ());
        }
    }


    [HttpPost]
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

            var result = await _productService.UpdateProduct(productDataModel);

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
                success = false,message = ex.Message
            });
        }
    }


    public async Task<IActionResult> Details (int id)
    {
        try
        {
            ProductDataModel productDataModel = await _productService.GetProductForEditProductID(id);

            ProductViewModel productViewModel = ProductMapping.MapProductViewModel ( productDataModel );

            productViewModel.SetDisplaytext ();

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
            // Return the image URL to the frontend
            return PartialView ("_Image",new ImageFile ());
        }

        ImageFile imageFile = await _storageService.SaveSessionFileAsync
        (_tenantSetter.ResolvedTenantId, _tenantSetter.HttpContextUserId, fileInput, true);

        SetSessionImageFile (imageFile,_tenantCacheService);

        // Return the image URL to the frontend
        return PartialView ("_Image",imageFile);
    }


    private bool ReadImage (IFormFile fileInput)
    {
        if ( fileInput != null && fileInput.FileName != null )
        {
            string extension = Path.GetExtension(fileInput.FileName).ToLower();

            if ( extension.Equals (".jpg") || extension.Equals (".jpeg")

                || extension.Equals (".png") || extension.Equals (".gif") )
            {
                return true;
            }
        }

        return false;
    }


    //[HttpGet]
    //public IActionResult LoadImage ()
    //{
    //    List<ImageFile>? imageFileList = GetAllSessionImages(_tenantCacheService)!;

    //    ImageFile imageFile = imageFileList?.LastOrDefault<ImageFile >()!;

    //    return PartialView ("_Image",imageFile);

    //}


    [HttpDelete]
    public async Task<JsonResult> ImageRemove (string fileName,int id,int postId)
    {
        try
        {
            bool result;
            if ( postId != 0 )
            {
                result = await _productService.DeleteProductImage (id,postId);
            }

            result = DeleteSessionImage (fileName,_tenantCacheService);

            return Json (new
            {
                success = result
            });
        }
        catch
        {
            return Json (new
            {
                errors = false
            });
        }
    }


    [HttpGet]
    public JsonResult GetSubCategories (int id)
    {
        try
        {
            var listSubCategories = DropDownListItems.GetSubCategoryList(_localizer);

            return Json (listSubCategories);
        }
        catch
        {
            return Json (new List<SelectListItem> ());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete (int id)
    {
        try
        {
            ProductViewModel productViewModel = new();
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