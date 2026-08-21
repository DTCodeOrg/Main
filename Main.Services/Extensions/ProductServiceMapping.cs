using DataTransferModel;
using Main.Common;
using Main.Model.Tenant;

namespace Main.Services.Extensions;

public static class ProductServiceMapping
{
    public static List<ProductDisplayModel> GetProductDisplayModels (List<Product> listProducts)
    {
        List<ProductDisplayModel> objListPostDisplayModel
            = new();

        ProductDisplayModel objProductDisplayModel;

        foreach ( Product item in listProducts.ToList () )
        {
            objProductDisplayModel = new ProductDisplayModel ();

            MapProductDisplayModel (item,objProductDisplayModel);

            objListPostDisplayModel.Add (objProductDisplayModel);
        }

        return objListPostDisplayModel;
    }

    private static void MapProductDisplayModel (
        Product productEntity,ProductDisplayModel productDisplayModel)
    {
        productDisplayModel.ProductID = productEntity.ProductID;
        productDisplayModel.CategoryID = productEntity.CategoryID;
        productDisplayModel.SubCategoryID = productEntity.SubCategoryID;
        productDisplayModel.ProductName = productEntity.ProductName;
        productDisplayModel.UnitPrice = productEntity.Price;
        productDisplayModel.Discount = productEntity.Discount;
    }

    public static Product MapSaveProductEntity (ProductDataModel productDataModel)
    {
        Product productEntity = CreateProductEntity(productDataModel);

        List <ProductImageFile> objListFileEntity
            = MapProductFileEntity(productDataModel);

        if ( productDataModel != null )
        {
            productEntity.ListImageFiles = objListFileEntity;
            productEntity.ListComments = new List<ProductComment> ();
        }

        return productEntity;
    }

    private static Product CreateProductEntity (ProductDataModel productDataModel)
    {
        return new Product ()
        {
            ProductName = productDataModel.ProductName!.ToString (),
            CategoryID = productDataModel.CategoryID,
            SubCategoryID = productDataModel.SubCategoryID,
            Price = productDataModel.UnitPrice,
            Discount = productDataModel.Discount
        };
    }

    private static List<ProductImageFile> MapProductFileEntity
        (ProductDataModel productDataModel)
    {
        List<ProductImageFile> listProductFileEntity = [];

        ProductImageFile productImageFile;

        productDataModel.ImageFiles.ForEach (fileDataModel =>
        {
            productImageFile = new ProductImageFile ()
            {
                ProductID = fileDataModel.ProductID,
                FiePath = fileDataModel.FilePath,
                FileContent = fileDataModel.FileContent
            };

            listProductFileEntity.Add (productImageFile);
        });

        return listProductFileEntity;
    }

    public static ProductDataModel MapSingleProductDataModel (Product productEntity)
    {
        if ( productEntity == null )
        {
            return new ProductDataModel ();
        }

        List<ProductFileDataModel> listProductFilesDataModel = [];

        if ( productEntity.ListImageFiles != null &&
            productEntity.ListImageFiles.Count > 0 )
        {
            productEntity.ListImageFiles.ToList ().ForEach (fileEntity =>
            {
                ProductFileDataModel fileDataModel = new()
                {
                    ProductImageFileID = fileEntity.ProductImageFileID,
                    FileContent = fileEntity.FileContent!,
                    FilePath = fileEntity.FiePath,
                    ProductID = fileEntity.ProductID
                };

                listProductFilesDataModel.Add (fileDataModel);

            });
        }

        List<ProductCommentDataModel> listCommentsDataModel = [];

        if ( productEntity.ListComments != null
             && productEntity.ListComments.Count > 0 )
        {

            productEntity.ListComments.ToList ().ForEach (commentEntity =>
            {
                ProductCommentDataModel objCommentDataModel = new()
                {
                    ProductCommentID = commentEntity.ProductCommentID,
                    Comment = commentEntity.Comment,
                    ProductID = commentEntity.ProductID
                };

                listCommentsDataModel.Add (objCommentDataModel);

            });
        }

        ProductDataModel productDataModel = new()
        {
            ProductID = productEntity.ProductID,
            ProductName = productEntity.ProductName,
            Discount = productEntity.Discount,
            SaleCommission = productEntity.SaleCommission,
            SearchTag = productEntity.SearchTag,
            PostType =   productEntity.PostType ,
            Description = productEntity.Description,
            CategoryID = productEntity.CategoryID,
            SubCategoryID = productEntity.SubCategoryID,
            UnitPrice = productEntity.Price,
            ListComments = listCommentsDataModel,
            ImageFiles = listProductFilesDataModel
        };

        return productDataModel;
    }

    public static Product MapProductUpdateEntity (Product productEntity,ProductDataModel productDataModel)
    {
        List<ProductImageFile> listProductImageFileEntity
            = new();

        listProductImageFileEntity.AddRange (productEntity.ListImageFiles);

        ProductImageFile fileEntity;

        productDataModel.ImageFiles.ForEach (fileDataModel =>
        {
            fileEntity = new ProductImageFile (fileDataModel.FileContent);

            fileEntity.CreateParameters (fileDataModel.BaseDataModel);

            fileEntity.ProductID = productEntity.ProductID;

            listProductImageFileEntity.Add (fileEntity);

        });

        List<ProductComment> listProductCommentsEntity
            = new();

        listProductCommentsEntity.AddRange (productEntity.ListComments);


        productDataModel.ListComments.ForEach (commentDataModel =>
        {
            var commentEntity = new ProductComment
            {
                ProductID = productEntity.ProductID
            };
            commentEntity.Comment = commentEntity.Comment;

            listProductCommentsEntity.Add (commentEntity);
        });

        productEntity.ProductName = productDataModel.ProductName!;
        productEntity.Discount = productDataModel.Discount;
        productEntity.SaleCommission = productDataModel.SaleCommission;
        productEntity.SearchTag = productDataModel.SearchTag;
        productEntity.PostType = EnumPostType.Product;
        productEntity.Description = productDataModel.Description;
        productEntity.CategoryID = productDataModel.CategoryID;
        productEntity.SubCategoryID = productDataModel.SubCategoryID;
        productEntity.Price = productDataModel.UnitPrice;

        productEntity.ListComments = listProductCommentsEntity;
        productEntity.ListImageFiles = listProductImageFileEntity;

        productEntity.ModifyParameters (productDataModel.BaseDataModel);

        return productEntity;
    }
}
