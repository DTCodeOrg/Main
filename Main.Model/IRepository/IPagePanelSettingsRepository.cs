using Main.Model.Tenant;

namespace Main.IRepository;

public interface IPagePanelSettingsRepository
{
    Task<List<Product>> GetAllProducts ();

    Task<Product> GetProductByProductID (int? postId);

    Task<List<Product>> GetSelectProducts ();
}
