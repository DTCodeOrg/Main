using Main.Model.Identity;

namespace Main.IRepository;

public interface ITenantRepository
{
    Task<Tenant?> GetTenantByIdAsync (Guid tenantId);

    Task<Tenant?> CreateTenantAsync (Tenant tenant);

    Task<Tenant?> FindHostAsync (string hostName);

}
