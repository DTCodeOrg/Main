using DataTransferModel;
using Main.Common;
using Main.Common.Models;
using Main.WebAppCore.Helpers;
using Main.WebAppCore.Models.Product;

namespace Main.WebAppCore.Models.Extensions;

public static class ProductMapping
{
    public static ProductDataModel NewProductDataModel (ProductViewModel productViewModel)
    {
        return new ProductDataModel ()
        {
            ProductName = productViewModel.ProductName,
            SearchTag = productViewModel.SearchTag,
            UnitPrice = productViewModel.UnitPrice,
            Discount = productViewModel.Discount,
            SaleCommission = productViewModel.SaleCommission,
            CategoryID = int.Parse (productViewModel.CategoryID),
            SubCategoryID = int.Parse (productViewModel.SubCategoryID),
            Description = productViewModel.Description,
            PostType = EnumPostType.Product,
            ProductID = 0
        };
    }

    public static ProductViewModel MapProductViewModel (ProductDataModel productDataModel)
    {
        ProductViewModel productViewModel = new ()
        {
            ProductID = productDataModel.ProductID,
            CategoryID = productDataModel.CategoryID.ToString (),
            SubCategoryID = productDataModel.SubCategoryID.ToString (),
            ProductName = productDataModel.ProductName,
            UnitPrice = productDataModel.UnitPrice,
            Discount = productDataModel.Discount,
            SaleCommission = productDataModel.SaleCommission,
            Description = productDataModel.Description,
            SearchTag = productDataModel.SearchTag
        };


        List <ImageFile> imageFiles = new();
        ImageFile imageFile;

        productDataModel.ImageFiles.ForEach (file =>
        {
            imageFile = new ImageFile (file.FileContent!,file.ProductID,file.ProductImageFileID)
            {
                RelativeFilePath = file.FilePath
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

        ProductDataModel productDataModel = new ()
        {
            ProductID = productViewModel.ProductID,
            CategoryID = int.Parse (productViewModel.CategoryID),
            SubCategoryID = int.Parse (productViewModel.SubCategoryID),
            ProductName = productViewModel.ProductName,
            UnitPrice = productViewModel.UnitPrice,
            Discount = productViewModel.Discount,
            SaleCommission = productViewModel.SaleCommission,
            Description = productViewModel.Description,
            PostType = EnumPostType.Product
        };

        return productDataModel;
    }

    public static List<ProductDisplayViewModel> MapDisplayProductViewModel (List<ProductDisplayModel> productDataModels)
    {
        List<ProductDisplayViewModel> dispayProductViewModels = new();

        ProductDisplayViewModel productDisplayViewModel;

        productDataModels.ForEach (model =>
        {
            productDisplayViewModel = new ProductDisplayViewModel ()
            {
                ProductID = model.ProductID,
                DisplayCategory = DropDownListItems.GetCategoryText (model.CategoryID.ToString ()),
                ProductName = model.ProductName,
                DisplaySubCategory = DropDownListItems.GetSubCategoryText (model.SubCategoryID.ToString ()),
                UnitPrice = model.UnitPrice
            };

            dispayProductViewModels.Add (productDisplayViewModel);
        });

        return dispayProductViewModels.ToList ();
    }
}
