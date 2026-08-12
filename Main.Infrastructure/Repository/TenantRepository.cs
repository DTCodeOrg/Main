using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Main.Model.Identity;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TenantRepository: ITenantRepository
{
    private readonly IdentityAppDbContext _tenantContext;

    public TenantRepository (IdentityAppDbContext context)
    {
        _tenantContext = context;
    }

    public async Task<Tenant?> FindHostAsync (string hostName)
    {
        Tenant? tenant = await _tenantContext.Tenants
            .FirstOrDefaultAsync<Tenant> ( a => a.Host.ToLower() == hostName.ToString ());

        return tenant;
    }

    public async Task<Tenant?> GetTenantByIdAsync (Guid tenantId)
    {
        Tenant? tenant = await _tenantContext.Tenants.FirstOrDefaultAsync (tenant => tenant.TenantId == tenantId);

        return tenant;
    }

    public async Task<Tenant?> CreateTenantAsync (Tenant tenant)
    {
        if ( tenant != null )
        {
            _ = _tenantContext.Tenants.Add (tenant);
            _ = await _tenantContext.SaveChangesAsync ();
        }

        return tenant;
    }
}
