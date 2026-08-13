using Main.Model.Identity;

namespace Main.IRepository;

public interface ITenantUserRepository
{
    Task<bool> AddAsync (TenantUserRole membership,CancellationToken ct = default);

    Task<bool> ExistsAsync (Guid tenantId,string userId,CancellationToken ct = default);

    Task<TenantUserRole?> GetByUserIdAsync (string userId,Guid tenantId,
    CancellationToken ct = default);
}
