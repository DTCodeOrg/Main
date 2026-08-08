
using Domain.Model;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class ProductRepository: IProductRepository
{

    private readonly TenantDbContext _tenantContext;

    public ProductRepository (TenantDbContext context)
    {
        _tenantContext = context;
    }

    public async Task<bool> SaveChanges ()
    {
        var result = await _tenantContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<Product>> GetAllProducts ()
    {
        return await _tenantContext.Products.ToListAsync ();
    }

    public async Task<bool> DeleteProduct (int productId)
    {
        var product = _tenantContext.Products.FirstOrDefault<Product>(a => a.ProductID == productId);

        if ( product != null )
        {
            _ = _tenantContext.Products.Remove (product);
        }

        var result = await _tenantContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteProductImage (int id,int productId)
    {
        var image = await _tenantContext.ProductImageFiles.FirstOrDefaultAsync <ProductImageFile>
                                   ( a => a.ProductImageFileID == id && a.ProductID == productId );

        if ( image != null )
        {
            _ = _tenantContext.ProductImageFiles.Remove (image);
        }

        var result = await _tenantContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<Product> GetProductByProductID (int postId)
    {
        Product? product = await _tenantContext.Products.FirstOrDefaultAsync<Product>
                                                   (a => a.ProductID == postId);

        if ( product != null )
        {
            return product;
        }

        return new Product ();
    }

    public async Task<bool> SaveNewProduct (Product productEntity)
    {
        _ = _tenantContext.Products.Add (productEntity);

        int result = await _tenantContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateProduct (Product productEntity)
    {
        _ = _tenantContext.Products.Update (productEntity);

        var result = await _tenantContext.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<List<Product>> GetSelectProducts ()
    {
        return await _tenantContext.Products
                .ToListAsync ();
    }
}

