using DataTransferModel;
using Main.Common;
using Main.Model.Tenant;

namespace Main.Services.Extensions;

public static class ProductServiceMapping
{
    public static List<ProductDisplayModel> GetProductDisplayModels (List<Product> listProducts)
    {
        List<ProductDisplayModel> objListPostDisplayModel = [];

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
            ProductName = productDataModel.ProductName,
            CategoryID = productDataModel.CategoryID,
            Price = productDataModel.UnitPrice,
            Discount = productDataModel.Discount,
            SaleCommission = productDataModel.SaleCommission,
            Description = productDataModel.Description,
            NameTag = productDataModel.SearchTag,
            PostType = productDataModel.PostType
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
        ProductFileDataModel fileDataModel;
        if ( productEntity.ListImageFiles != null &&
            productEntity.ListImageFiles.Count > 0 )
        {
            productEntity.ListImageFiles.ToList ().ForEach (fileEntity =>
            {
                fileDataModel = new ProductFileDataModel ()
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
                ProductCommentDataModel objCommentDataModel = new ()
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
            SearchTag = productEntity.NameTag,
            PostType =   productEntity.PostType ,
            Description = productEntity.Description,
            CategoryID = productEntity.CategoryID,
            UnitPrice = productEntity.Price,
            ListComments = listCommentsDataModel,
            ImageFiles = listProductFilesDataModel
        };

        return productDataModel;
    }

    public static Product MapProductUpdateEntity (Product productEntity,ProductDataModel productDataModel)
    {
        List<ProductImageFile> listProductImageFileEntity
            = [];

        listProductImageFileEntity.AddRange (productEntity.ListImageFiles);

        ProductImageFile fileEntity;

        productDataModel.ImageFiles.ForEach (fileDataModel =>
        {
            fileEntity = new ProductImageFile ()
            {
                ProductID = productEntity.ProductID,
                FiePath = fileDataModel.FilePath
            };

            listProductImageFileEntity.Add (fileEntity);

        });

        List<ProductComment> listProductCommentsEntity
            = [];

        listProductCommentsEntity.AddRange (productEntity.ListComments);


        productDataModel.ListComments.ForEach (commentDataModel =>
        {
            var commentEntity = new ProductComment ()
            {
                ProductID = productEntity.ProductID
            };

            commentEntity.Comment = commentEntity.Comment;

            listProductCommentsEntity.Add (commentEntity);
        });

        productEntity.ProductName = productDataModel.ProductName!;
        productEntity.Discount = productDataModel.Discount;
        productEntity.SaleCommission = productDataModel.SaleCommission;
        productEntity.NameTag = productDataModel.SearchTag;
        productEntity.PostType = EnumPostType.Product;
        productEntity.Description = productDataModel.Description;
        productEntity.CategoryID = productDataModel.CategoryID;
        productEntity.Price = productDataModel.UnitPrice;

        productEntity.ListComments = listProductCommentsEntity;
        productEntity.ListImageFiles = listProductImageFileEntity;

        return productEntity;
    }
}
