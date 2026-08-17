using DataTransferModel;
using Main.Common.Models;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppCore.Helper;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore;

[Area ("CompanyContent")]
[Authorize (Policy = "TenantAdmin")]
public class ManageProductController: BaseController
{

    private readonly IProductService _productService;
    private readonly ILogger<ManageProductController> _logger;
    private readonly ITenantSetter _tenantSetter;

    private readonly ITenantCacheService _tenantCacheService;

    public ManageProductController (IProductService productService,
        ILogger<ManageProductController> logger,
        ITenantSetter tenantSetter,
        ITenantCacheService tenantCacheService)
    {
        _productService = productService;
        _logger = logger;
        _tenantSetter = tenantSetter;
        _tenantCacheService = tenantCacheService;
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

        List<ImageFile> listSessionImageFiles = GetAllSessionImages(_tenantCacheService);

        listSessionImageFiles.ForEach (imgFile =>
        {
            productImageFileDataModel = new ProductFileDataModel ()
            {
                ImageFileContent = imgFile.FileContent,
                ProductID = imgFile.PostID ?? 0,
                ProductImageFileID = 0
            };

            listProductImageFileDataModels.Add (productImageFileDataModel);
        });

        productDataModel.ImageFiles = listProductImageFileDataModels;

        ClearImageFileListSession (_tenantCacheService);
    }



    public IActionResult NewProduct ()
    {
        try
        {
            ClearImageFileListSession (_tenantCacheService);

            ProductViewModel objProductViewModel = new ()
            {
                PageName = "New Product"
            };

            objProductViewModel.AV_Category = DropDownListItems.GetCategoryList ();
            objProductViewModel.AV_SubCategory = DropDownListItems.GetSubCategoryList ();

            return View (objProductViewModel);
        }
        catch
        {
            return View (new ProductViewModel ());
        }
    }

    [HttpPost]
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
    public JsonResult UploadImage (IFormFile file)
    {
        if ( file != null && file.Length > 0 && file.Length > AppSettings.Current.PostImageSize )
        {
            return Json (new
            {
                success = false
            });
        }

        ImageFile imageFile = ReadImage ( file ?? file! );

        if ( imageFile.IsNew && imageFile.FileContent.Length > 0 )
        {
            SetSessionImageFile (imageFile,_tenantCacheService);
        }

        return Json (new
        {
            success = true
        });
    }


    private ImageFile ReadImage (IFormFile file)
    {
        if ( !string.IsNullOrEmpty (file.ContentType) && file.FileName != null )
        {
            string extension = Path.GetExtension(file.FileName).ToLower();

            if ( extension.Equals (".jpg") || extension.Equals (".jpeg")

                || extension.Equals (".png") || extension.Equals (".gif") )
            {
                var imgByte = new Byte[file.Length];

                var stream = file.OpenReadStream();

                _ = stream.Read (imgByte);

                ImageFile objFile = new()
                {
                    FileContent = imgByte ,
                    IsNew = true ,
                    PostID = 0
                };

                return objFile;
            }
        }

        return new ImageFile ();
    }

    [HttpGet]
    [Authorize (Roles = "Company,Admin")]
    public PartialViewResult LoadImage ()
    {
        try
        {
            var objImageList = GetAllSessionImages(_tenantCacheService);

            var objImage = objImageList.Last();

            return PartialView ("~/Areas/CompanyContent/Views/ManageProduct/_Image.cshtml",objImage);
        }
        catch
        {
            return PartialView ("~/Areas/CompanyContent/Views/ManageProduct/_Image.cshtml",new ImageFile ());
        }
    }


    [HttpDelete]
    public async Task<JsonResult> ImageRemove (int id,int postId)
    {
        try
        {
            bool result;
            if ( postId != 0 )
            {
                result = await _productService.DeleteProductImage (id,postId);
            }

            result = RemoveSessionImageFile (id,_tenantCacheService);

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
            var listSubCategories = DropDownListItems.GetSubCategories( id );

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