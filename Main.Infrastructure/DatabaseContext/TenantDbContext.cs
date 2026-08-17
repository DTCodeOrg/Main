using Main.Common;
using Main.Common.Models;
using Main.Model.Base;
using Main.Model.Identity;
using Main.Model.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Main.Infrastructure.DatabaseContext;

public class TenantDbContext: DbContext
{
    private readonly ITenantSetter _tenantSetter;

    public TenantDbContext (DbContextOptions<TenantDbContext> options) : base (options)
    {
    }

    public TenantDbContext (DbContextOptions<TenantDbContext> options,
    ITenantSetter tenantSetter) : base (options)
    {
        _tenantSetter = tenantSetter;
    }

    public DbSet<Page> Pages
    {
        get; set;
    }

    public DbSet<Panel> Panels
    {
        get; set;
    }

    public DbSet<Post> Posts
    {
        get; set;
    }

    public DbSet<Product> Products
    {
        get; set;
    }

    public DbSet<ProductImageFile> ProductImageFiles
    {
        get; set;
    }

    public DbSet<ProductComment> ProductComments
    {
        get; set;
    }

    public DbSet<AdminPost> AdminPosts
    {
        get; set;
    }

    public DbSet<AdminPostComment> AdminPostComments
    {
        get; set;
    }

    public DbSet<AdminImageFile> AdminImageFiles
    {
        get; set;
    }

    public DbSet<AllowedValue> AllowedValues
    {
        get; set;
    }

    public Guid CurrentTenantId => _tenantSetter?.ResolvedTenantId ?? Guid.Empty;

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        base.OnModelCreating (modelBuilder);

        FluentApiConfiguration (modelBuilder);

        foreach ( var entityType in modelBuilder.Model.GetEntityTypes () )
        {
            if ( typeof (IMustHaveTenant).IsAssignableFrom (entityType.ClrType) )
            {
                _ = modelBuilder.Entity (entityType.ClrType)
                    .HasQueryFilter (CreateTenantFilterExpression (entityType.ClrType));
            }
        }

        Guid[] guidTenantArray = new []
        {
            new Guid(1, 0, 0, new byte[8]),
            new Guid(2, 0, 0, new byte[8])
        };

        Guid TenantId1 = guidTenantArray[0];
        Guid TenantId2 = guidTenantArray[1];

        int pageCounterIDTenant1 = 1 ;
        PageSeed (modelBuilder,TenantId1,pageCounterIDTenant1);
        int pageCounterIDTenant2 = 10 ;
        PageSeed (modelBuilder,TenantId2,pageCounterIDTenant2);
    }

    private LambdaExpression CreateTenantFilterExpression (Type type)
    {
        var parameter = Expression.Parameter(type, "e");
        var property = Expression.Property(parameter, nameof(IMustHaveTenant.MyTenantId));

        var dbContextConst = Expression.Constant(this);
        var tenantIdValue = Expression.Property(dbContextConst, nameof(CurrentTenantId));

        var body = Expression.Equal(property, tenantIdValue);
        return Expression.Lambda (body,parameter);
    }

    private void PageSeed (ModelBuilder modelBuilder,Guid seedTenancyId,int id)
    {
        _ = modelBuilder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.Home,seedTenancyId,true));

        _ = modelBuilder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.AdsDetail,seedTenancyId,true));

        _ = modelBuilder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.Resources,seedTenancyId,true));

        _ = modelBuilder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.CategoryButtonMarket,seedTenancyId,true));

        _ = modelBuilder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.SubCategoryDropdownMarket,seedTenancyId,true));

        _ = modelBuilder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.SpecialMarketButton,seedTenancyId,true));

        _ = modelBuilder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.AllMarket,seedTenancyId,true));

        _ = modelBuilder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.NoticeAndNews,seedTenancyId,true));
    }

    private void FluentApiConfiguration (ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Post> ()
            .Property (p => p.Price)
            .HasColumnType ("decimal(18,2)")
            .IsRequired ();

        _ = modelBuilder.Entity<Product> ()
           .Property (p => p.Discount)
           .HasPrecision (18,2);

        _ = modelBuilder.Entity<Product> ()
            .Property (p => p.Price)
            .HasPrecision (18,2);

        _ = modelBuilder.Entity<Product> ()
            .Property (p => p.SaleCommission)
            .HasPrecision (18,2);

        _ = modelBuilder.Entity<Page> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<Post> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<Panel> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<Product> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<ProductComment> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<ProductImageFile> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<AdminPost> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<AdminPostComment> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<AdminImageFile> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = modelBuilder.Entity<AllowedValue> ()
          .HasIndex (ut => ut.MyTenantId);
    }

    private void ApplyBaseDataTenantId ()
    {
        Guid ResolvedTenantId = _tenantSetter.CurrentTenant.ResolvedTenantId;

        Guid[] guidTenantArray = new []
        {
            new Guid(1, 0, 0, new byte[8]),
            new Guid(2, 0, 0, new byte[8])
        };

        BaseDataModel createDataModel = _tenantSetter.CreateMetaData;
        BaseDataModel updateDataModel = _tenantSetter.UpdateMetaData;
        BaseDataModel deleteDataModel = _tenantSetter.DeleteMetaData;

        var entries = ChangeTracker.Entries()
        .Where(e => e.Entity is IMustHaveTenant &&
              (e.State == EntityState.Added
               || e.State == EntityState.Modified
               || e.State == EntityState.Deleted)).ToArray();

        foreach ( var entry in entries )
        {
            var tenantEntity = (IMustHaveTenant) entry.Entity;
            tenantEntity.MyTenantId = ResolvedTenantId;

            if ( entry.State == EntityState.Added )
            {
                tenantEntity.CreateParameters (createDataModel);
            }
            else if ( entry.State == EntityState.Deleted )
            {
                tenantEntity.DeleteParameters (deleteDataModel);
            }
            else if ( entry.State == EntityState.Modified )
            {
                tenantEntity.ModifyParameters (updateDataModel);
            }
        }
    }

    public override int SaveChanges (bool acceptAllChangesOnSuccess)
    {
        ApplyBaseDataTenantId ();
        return base.SaveChanges (acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default)
    {
        ApplyBaseDataTenantId ();
        return await base.SaveChangesAsync (true,cancellationToken);
    }
}
