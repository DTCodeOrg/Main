using Domain.Model;
using Main.Common;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Main.Infrastructure.DatabaseContext;

public class LogDbContext: DbContext
{
    private readonly ITenantSetter _tenantSetter;

    public static readonly Guid[] guidArray
    = new []
    {
        new Guid(1, 0, 0, new byte[8]),
        new Guid(2, 0, 0, new byte[8])
    };

    public LogDbContext (DbContextOptions<LogDbContext> options) : base (options)
    {
    }

    public LogDbContext
    (DbContextOptions<LogDbContext> options,ITenantSetter tenantSetter) : base (options)
    {
        _tenantSetter = tenantSetter;
    }

    public DbSet<ExceptionLog> ExceptionLogs
    {
        get; set;
    }

    protected override void OnModelCreating (ModelBuilder builder)
    {
        base.OnModelCreating (builder);

        FluentApiConfiguration (builder);
    }

    private void FluentApiConfiguration (ModelBuilder builder)
    {
    }

    private void ApplyBaseMetaData ()
    {
        BaseDataModel createDataModel = _tenantSetter.CreateMetaData;
        BaseDataModel updateDataModel = _tenantSetter.UpdateMetaData;
        BaseDataModel deleteDataModel = _tenantSetter.DeleteMetaData;

        var entries = ChangeTracker.Entries()
        .Where(e =>
               e.State == EntityState.Added
               || e.State == EntityState.Modified
               || e.State == EntityState.Detached).ToArray();

        foreach ( var entry in entries )
        {
            var tenantEntity = (IMustHaveTenant) entry.Entity;

            if ( entry.State == EntityState.Added )
            {
                tenantEntity.CreateParameters (createDataModel);
            }
            else if ( entry.State == EntityState.Deleted )
            {
                tenantEntity.ModifyParameters (deleteDataModel);
            }
            else if ( entry.State == EntityState.Modified )
            {
                tenantEntity.ModifyParameters (updateDataModel);
            }
        }
    }

    public override int SaveChanges (bool acceptAllChangesOnSuccess)
    {
        ApplyBaseMetaData ();
        return base.SaveChanges (acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default)
    {
        ApplyBaseMetaData ();

        return await base.SaveChangesAsync (true,cancellationToken);
    }
}
