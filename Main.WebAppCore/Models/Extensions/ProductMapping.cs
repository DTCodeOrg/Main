using DataTransferModel;
using Main.Common;
using Main.Common.Models;
using Main.Infrastructure;
using Main.WebAppCore.Helpers;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.WebAppCore.Models.Extensions;

public static class ProductMapping
{
    public static ProductDataModel NewProductDataModel (ProductViewModel productViewModel)
    {
        return new ProductDataModel ()
        {
            ProductName = productViewModel.ProductName,
            ProductOwner = productViewModel.ProductOwner,
            SearchTag = productViewModel.SearchTag,
            UnitPrice = productViewModel.UnitPrice,
            Discount = productViewModel.Discount,
            SaleCommission = productViewModel.SaleCommission,
            CategoryID = productViewModel.CategoryID,
            SubCategoryID = productViewModel.SubCategoryID,
            Description = productViewModel.Description,
            PostType = EnumPostType.Product,
            ProductID = 0
        };
    }

    public static ProductViewModel MapProductViewModel (ProductDataModel productDataModel,IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        ProductViewModel productViewModel = new ()
        {
            ProductID = productDataModel.ProductID,

            CategoryID = productDataModel.CategoryID,

            SubCategoryID = productDataModel.SubCategoryID,

            CategoryText = DropDownListItems.GetTextForCategoryId ( productDataModel.CategoryID, localizer, storeType ),

            SubCategoryText = DropDownListItems.GetTextForCategoryId ( productDataModel.SubCategoryID, localizer, storeType ),

            ProductName = productDataModel.ProductName,

            ProductOwner = productDataModel.ProductOwner,

            UnitPrice = productDataModel.UnitPrice,

            Discount = productDataModel.Discount,

            SaleCommission = productDataModel.SaleCommission,

            Description = productDataModel.Description,

            SearchTag = productDataModel.SearchTag
        };


        List <ImageFile> imageFiles = [];
        ImageFile imageFile;

        productDataModel.ImageFiles.ForEach (file =>
        {
            imageFile = new ImageFile ()
            {
                SessionFilePath = file.FilePath,
                RelativeFilePath = file.FilePath,
                FileContent = file.FileContent!,
                PostID = file.ProductID,
                PostType = EnumPostType.Product,
                FileID = file.ProductImageFileID
            };

            imageFiles.Add (imageFile);

        });

        productViewModel.ImageFiles = imageFiles;

        return productViewModel;

    }

    public static ProductDataModel MapProductDataModel (ProductViewModel productViewModel)
    {
        if ( productViewModel == null )
        {
            return new ProductDataModel ();
        }

        ProductDataModel productDataModel = new()
        {
            ProductID = productViewModel.ProductID,

            CategoryID = productViewModel.CategoryID,

            SubCategoryID = productViewModel.SubCategoryID,

            ProductName = productViewModel.ProductName,

            ProductOwner = productViewModel.ProductOwner,

            UnitPrice = productViewModel.UnitPrice,

            Discount = productViewModel.Discount,

            SaleCommission = productViewModel.SaleCommission,

            Description = productViewModel.Description,

            SearchTag = productViewModel.SearchTag,

            PostType = EnumPostType.Product
        };

        return productDataModel;
    }

    public static List<ProductDisplayViewModel> MapDisplayProductViewModel
    (List<ProductDisplayModel> productDataModels,IStringLocalizer<SharedResource> localizer,ITenantSetter tenantSetter)
    {
        List<ProductDisplayViewModel> dispayProductViewModels = [];

        ProductDisplayViewModel productDisplayViewModel;

        productDataModels.ForEach (model =>
        {
            productDisplayViewModel = new ProductDisplayViewModel ()
            {
                ProductID = model.ProductID,

                DisplayCategory =
                TenantStoreHelper.GetTextForCategoryId (model.CategoryID,localizer,tenantSetter.CurrentTenant.StoreType),

                DisplaySubCategory =
                TenantStoreHelper.GetTextForCategoryId (model.SubCategoryID,localizer,tenantSetter.CurrentTenant.StoreType),

                ProductName = model.ProductName,

                ProductOwner = model.ProductOwner,

                UnitPrice = model.UnitPrice,

                TenantName = tenantSetter.CurrentTenant.TenantName
            };

            dispayProductViewModels.Add (productDisplayViewModel);

        });

        return dispayProductViewModels.ToList ();
    }
}
