using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Main.Model.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class PagePanelSettingsRepository: IPagePanelSettingsRepository
{
    private readonly TenantDbContext _tenantContext;

    public PagePanelSettingsRepository (TenantDbContext context)
    {
        _tenantContext = context;
    }

    public async Task<List<Product>> GetAllProducts ()
    {
        return await _tenantContext.Products.ToListAsync ();
    }

    public async Task<Product> GetProductByProductID (int? postId)
    {
        Product? product = await _tenantContext.Products
            .FirstOrDefaultAsync<Product> (a => a.ProductID == postId);

        if ( product != null )
        {
            return product;
        }

        return new Product ();
    }

    public async Task<List<Product>> GetSelectProducts ()
    {
        return await _tenantContext.Products.ToListAsync ();
    }
}

