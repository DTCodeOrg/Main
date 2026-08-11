using Domain.Model;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TenantUserRepository: ITenantUserRepository
{
    private readonly IdentityAppDbContext _db;

    public TenantUserRepository (IdentityAppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync (TenantUserRole membership,CancellationToken ct = default)
    {
        _ = await _db.TenantUserRoles.AddAsync (membership,ct);
        _ = await _db.SaveChangesAsync (ct);
    }

    public async Task<bool> ExistsAsync (Guid tenantId,string userId,CancellationToken ct = default)
        => await _db.TenantUserRoles.AnyAsync (x => x.TenantId == tenantId && x.UserId == userId);

    public async Task<TenantUserRole?> GetByUserIdAsync (string userId,Guid tenantId,CancellationToken ct = default)
        => await _db.TenantUserRoles.FirstOrDefaultAsync (x => x.TenantId == tenantId && x.UserId == userId);
}
