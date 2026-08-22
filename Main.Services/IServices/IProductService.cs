using DataTransferModel;

namespace Main.Services;

public interface IProductService
{
    Task<bool> SaveNewProduct (ProductDataModel productDataModel);

    Task<bool> UpdateProduct (ProductDataModel productDataModel);

    Task<string> DeleteProductImage (int id,int postId);

    Task<bool> DeleteProduct (int productId);

    Task<ProductDataModel> GetProductForEditProductID (int? productID);

    Task<List<ProductDisplayModel>> GetAllProducts ();
}

