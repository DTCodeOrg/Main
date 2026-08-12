using Main.Common;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Main.Model.Identity;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TenantInvitationRepository: ITenantInvitationRepository
{
    private readonly IdentityAppDbContext _db;

    public TenantInvitationRepository (IdentityAppDbContext db)
    {
        _db = db;
    }

    public async Task<TenantInvitation?> GetByTokenAsync (string token,CancellationToken ct = default)
        => await _db.TenantInvitations.FirstOrDefaultAsync (x => x.Token == token,ct);

    public async Task<TenantInvitation?> GetByEmailAndTenantAsync (Guid tenantId,string email,CancellationToken ct = default)
        => await _db.TenantInvitations.FirstOrDefaultAsync (x => x.TenantId == tenantId && x.Email == email,ct);

    public async Task AddAsync (TenantInvitation invitation,CancellationToken ct = default)
    {
        _ = await _db.TenantInvitations.AddAsync (invitation,ct);
        _ = await _db.SaveChangesAsync (ct);
    }

    public async Task UpdateAsync (TenantInvitation invitation,CancellationToken ct = default)
    {
        _ = _db.TenantInvitations.Update (invitation);
        _ = await _db.SaveChangesAsync (ct);
    }

    public async Task<bool> ExistsAsync (Guid tenantId,string email,CancellationToken ct = default)
        => await _db.TenantInvitations.AnyAsync
        (x => x.TenantId == tenantId && x.Email == email && x.Status == InvitationStatus.Pending,ct);
}
