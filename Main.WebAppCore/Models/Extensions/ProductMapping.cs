using DataTransferModel;
using Main.Common;
using Main.Common.Models;

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
            CategoryID = int.Parse (productViewModel.CategoryID!),
            SubCategoryID = int.Parse (productViewModel.SubCategoryID!),
            Description = productViewModel.Description,
            PostType = EnumPostType.Product,
            ProductID = 0
        };
    }

    public static ProductViewModel MapProductViewModel (ProductDataModel productDataModel)
    {
        ProductViewModel productViewModel = new ()
        {
            ProductID
                = productDataModel.ProductID.HasValue ? productDataModel.ProductID.Value : 0,

            CategoryID = productDataModel.CategoryID.HasValue ? productDataModel.CategoryID.Value.ToString() : "",

            SubCategoryID = productDataModel.SubCategoryID.HasValue ? productDataModel.SubCategoryID.Value.ToString() : "",

            ProductName = productDataModel.ProductName!,

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
                RelativeFilePath = file.FilePath,
                FileContent = file.FileContent!,
                PostID = file.ProductID,
                PostType = EnumPostType.Product
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

            CategoryID = int.Parse (productViewModel.CategoryID!),

            SubCategoryID = int.Parse (productViewModel.SubCategoryID!),

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
                ProductID = model.ProductID.HasValue ? model.ProductID.Value : 0,

                DisplayCategory = "",

                ProductName = model.ProductName!,

                DisplaySubCategory = "",

                UnitPrice = model.UnitPrice
            };

            dispayProductViewModels.Add (productDisplayViewModel);

        });

        return dispayProductViewModels.ToList ();
    }
}
