using DataTransferModel;

using Main.Common;

using WebAppCore.Helper;

namespace WebAppCore.ViewModel.Extensions;

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
        ProductViewModel productViewModel = new();

        productViewModel.ProductID = productDataModel.ProductID;
        productViewModel.CategoryID = productDataModel.CategoryID.ToString ();
        productViewModel.SubCategoryID = productDataModel.SubCategoryID.ToString ();
        productViewModel.ProductName = productDataModel.ProductName;
        productViewModel.UnitPrice = productDataModel.UnitPrice;
        productViewModel.Discount = productDataModel.Discount;
        productViewModel.SaleCommission = productDataModel.SaleCommission;
        productViewModel.Description = productDataModel.Description;
        productViewModel.SearchTag = productDataModel.SearchTag;


        List <ImageFile> imageFiles = new();
        ImageFile imageFile;

        productDataModel.ImageFiles.ForEach (file =>
        {
            imageFile = new ImageFile (file.ImageFileContent,file.ProductID,file.ProductImageFileID);
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

        ProductDataModel productDataModel = new();

        productDataModel.ProductID = productViewModel.ProductID;
        productDataModel.CategoryID = int.Parse (productViewModel.CategoryID);
        productDataModel.SubCategoryID = int.Parse (productViewModel.SubCategoryID);
        productDataModel.ProductName = productViewModel.ProductName;
        productDataModel.UnitPrice = productViewModel.UnitPrice;
        productDataModel.Discount = productViewModel.Discount;
        productDataModel.SaleCommission = productViewModel.SaleCommission;
        productDataModel.Description = productViewModel.Description;
        productDataModel.PostType = EnumPostType.Product;

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
