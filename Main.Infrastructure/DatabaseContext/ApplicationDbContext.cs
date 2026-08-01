using Domain.Model;
using Main.Common;
using Main.Infrastructure.CrosscuttingHelperServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Main.Infrastructure.DatabaseContext;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    // 1. Store the interface instance, NOT the raw Guid value
    private readonly ITenantSetter _tenantSetter;
    private readonly ITenantContext _tenantContext;
    private readonly ILogger<ExceptionLoggingService> _logger;

    public static readonly Guid[] guidArray = new[]
    {
        new Guid(1, 0, 0, new byte[8]),
        new Guid(2, 0, 0, new byte[8]),
        new Guid(3, 0, 0, new byte[8]),
        new Guid(1, 0, 0, new byte[8]),
        new Guid(2, 0, 0, new byte[8]),
        new Guid(3, 0, 0, new byte[8]),
        new Guid(4, 0, 0, new byte[8]),
        new Guid(5, 0, 0, new byte[8]),
        new Guid(6, 0, 0, new byte[8]),
        new Guid(7, 0, 0, new byte[8]),
        new Guid(8, 0, 0, new byte[8]),
        new Guid(9, 0, 0, new byte[8]),
        new Guid(10, 0, 0, new byte[8]),
        new Guid(11, 0, 0, new byte[8]),
        new Guid(12, 0, 0, new byte[8]),
        new Guid(13, 0, 0, new byte[8]),
        new Guid(14, 0, 0, new byte[8]),
        new Guid(15, 0, 0, new byte[8])
    };

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options)
    {
    }

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options,
    ITenantSetter tenantSetter,ITenantContext tenantContext,ILogger<ExceptionLoggingService> logger) : base (options)
    {
        // Save the reference to the scoped service
        _tenantSetter = tenantSetter;
        _tenantContext = tenantContext;
        _logger = logger;
    }

    public DbSet<ApplicationUser> ApplicationUsers
    {
        get; set;
    }

    public DbSet<Tenant> Tenants
    {
        get; set;
    }

    public DbSet<EmailSmtp> EmailSmtps
    {
        get; set;
    }

    public DbSet<TenantUser> TenantUsers
    {
        get; set;
    }

    public DbSet<TenantInvitation> TenantInvitations
    {
        get; set;
    }

    public DbSet<EmailOutboxMessage> EmailOutboxMessages
    {
        get; set;
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

    public DbSet<AdminPost> AdPosts
    {
        get; set;
    }

    public DbSet<AdminPostComment> AdPostComments
    {
        get; set;
    }

    public DbSet<AdminImageFile> AdImageFiles
    {
        get; set;
    }

    public DbSet<AValue> AValues
    {
        get; set;
    }

    public DbSet<ExceptionLog> ExceptionLogs
    {
        get; set;
    }

    public DbSet<UserRefreshToken> UserRefreshTokens
    {
        get; set;
    }

    // 2. Create a dynamic property that always fetches the live value
    public Guid ResolvedTenantId => _tenantSetter.CurrentTenantId;

    protected override void OnModelCreating (ModelBuilder builder)
    {
        base.OnModelCreating (builder);

        // 1. SEED ROLES FIRST
        _ = builder.Entity<IdentityRole> ().HasData (
            new IdentityRole
            {
                Id = "GlobalAdmin", // Must match exactly
                Name = "GlobalAdmin",
                NormalizedName = "GLOBALADMIN"
            },
            new IdentityRole
            {
                Id = "User", // Must match exactly
                Name = "User",
                NormalizedName = "USER"
            }
        );

        FluentApiConfiguration (builder);

        SeedData (builder);

        TenantGlobalQueryFilter (builder);
    }

    private void FluentApiConfiguration (ModelBuilder builder)

    {
        _ = builder.Entity<Tenant> ()
       .HasOne (t => t.EmaiSmtp)
       .WithOne (e => e.Tenant)
       .HasForeignKey<EmailSmtp> (e => e.FkTenantId);

        // Tenant User
        _ = builder.Entity<TenantUser> ()
            .HasOne (ut => ut.User)
            .WithMany (au => au.TenantUsers)
            .HasForeignKey (ut => ut.UserId);


        // Tenant Invitation
        _ = builder.Entity<TenantInvitation> (static entity =>
        {
            _ = entity.Property (t => t.Email)
                  .IsRequired ();
            _ = entity.Property (t => t.Token)
                  .IsRequired ();
            _ = entity.Property (t => t.Status)
                  .IsRequired ();
            _ = entity.Property (t => t.CreatedOn)
                  .IsRequired ();
            _ = entity.Property (t => t.ExpiresOn)
                  .IsRequired ();
        });

        // 1. Locate this line in your DbContext / Configuration file:
        _ = builder.Entity<TenantInvitation> ()
            .Property (t => t.Status)
            .HasDefaultValue (InvitationStatus.Pending) // or your specific default
            .HasSentinel (( InvitationStatus ) ( -1 ));      // Add this line to silence validation [20601]


        // Application User
        _ = builder.Entity<ApplicationUser> ()
            .HasIndex (u => u.Email)
            .IsUnique ();

        // Post
        _ = builder.Entity<Post> ()
            .Property (p => p.Price)
            .HasColumnType ("decimal(18,2)")
            .IsRequired ();

        // Product
        _ = builder.Entity<Product> ()
           .Property (p => p.Discount)
           .HasPrecision (18,2);

        _ = builder.Entity<Product> ()
            .Property (p => p.Price)
            .HasPrecision (18,2); // Set precision to 18 and scale to 2 digits

        _ = builder.Entity<Product> ()
            .Property (p => p.SaleCommission)
            .HasPrecision (18,2);


        // Email Outbox Message
        _ = builder.Entity<EmailOutboxMessage> (static entity =>
        {
            _ = entity.HasKey (t => t.Id);
            _ = entity.Property (t => t.ReceiverEmail)
                      .IsRequired ();
            _ = entity.Property (t => t.Subject)
                      .IsRequired ();
            _ = entity.Property (t => t.Body)
                      .IsRequired ();
            _ = entity.Property (t => t.CreatedOnUtc)
                      .IsRequired ();
            _ = entity.Property (t => t.RetryCount)
                      .IsRequired ();
        });

        _ = builder.Entity<Tenant> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<TenantInvitation> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<TenantUser> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<Page> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<Post> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<Panel> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<Product> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<ProductComment> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<ProductImageFile> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<AdminPost> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<AdminPostComment> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<AdminImageFile> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<AValue> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<ExceptionLog> ()
          .HasIndex (ut => ut.MyTenantId);

        // FIX: Creates a highly optimized composite index tracking both tenant and token
        _ = builder.Entity<UserRefreshToken> ()
            .HasIndex (ut => new { ut.MyTenantId,ut.Token })
            .IsUnique (); // Ensure no duplicate active tokens can exist inside the same tenant footprint

        _ = builder.Entity<EmailOutboxMessage> ()
          .HasIndex (ut => ut.MyTenantId);
    }

    // TenantId based Query filters (comes from ITenantSetter.CurrentTenantId)
    // Initially, the CurrentTenantId is set in the TenantMiddleware, which is executed before the DbContext is created.
    private void TenantGlobalQueryFilter (ModelBuilder builder)
    {
        // Example: Access the live property when saving data
        _logger.LogWarning ("Query data for Tenant Id: " + ResolvedTenantId.ToString ());

        _ = builder.Entity<Tenant> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<TenantUser> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<TenantInvitation> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<Product> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<ProductImageFile> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<ProductComment> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<AdminPost> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<AdminImageFile> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<AdminPostComment> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<Post> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<Panel> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<Page> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<AValue> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<ExceptionLog> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);

        _ = builder.Entity<UserRefreshToken> ().HasQueryFilter (p => p.MyTenantId == ResolvedTenantId);
    }

    // Apply BaseData and TenantId to entities implementing IMustHaveTenant interface before saving changes for (entries with added, modified and deleted sattus)
    private void ApplyBaseDataTenantId ()
    {
        Guid?  myTenantId = (Guid?) ResolvedTenantId;

        BaseDataModel createDataModel = _tenantContext.GetCreateBaseDataModel ();
        BaseDataModel updateDataModel = _tenantContext.GetUpdateBaseDataModel ();
        BaseDataModel deleteDataModel = _tenantContext.GetDeleteBaseDataModel ();

        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IMustHaveTenant);

        foreach ( var entry in entries )
        {
            if ( myTenantId != null )
            {
                ( ( IMustHaveTenant ) entry.Entity ).MyTenantId = myTenantId.Value;
            }

            var entryState = entry.State;

            if ( entryState == EntityState.Added )
            {
                ( ( IMustHaveTenant ) entry.Entity ).CreateParameters (createDataModel);
            }
            else if ( entryState == EntityState.Modified )
            {
                ( ( IMustHaveTenant ) entry.Entity ).ModifyParameters (updateDataModel);
            }
            else if ( entryState == EntityState.Deleted )
            {
                ( ( IMustHaveTenant ) entry.Entity ).ModifyParameters (deleteDataModel);
            }
        }
    }

    public override int SaveChanges (bool acceptAllChangesOnSuccess)
    {
        ApplyBaseDataTenantId ();

        // Example: Access the live property when saving data
        _logger.LogWarning ("Saving data for Tenant Id: " + ResolvedTenantId.ToString ());

        return base.SaveChanges (acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync (bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = default)
    {
        ApplyBaseDataTenantId ();

        // Example: Access the live property when saving data
        _logger.LogWarning ("Saving data for Tenant Id: " + ResolvedTenantId.ToString ());

        return base.SaveChangesAsync (acceptAllChangesOnSuccess,cancellationToken);
    }




    public void SeedData (ModelBuilder builder)
    {
        // Convert all elements to an array of Guids

        // TENANT ID (1 & 2) 
        Guid TenantID1 = guidArray[0];
        Guid TenantID2 = guidArray[1];

        // Tenant 1
        var tenant1 = new Tenant (TenantID1, guidArray[13])
        {
            Name = "LifeStyle Store",
            HostName = "lifestyles",
            Store = StoreType.LifeStyles
        };

        // Tenant 1
        _ = builder.Entity<Tenant> ().HasData (tenant1);

        // Tenant 2
        var  tenant2 = new Tenant (TenantID2, guidArray[14])
        {
            Name = "Fine Arts Store",
            HostName = "finearts",
            Store = StoreType.FineArts
        };

        // Tenant 2
        _ = builder.Entity<Tenant> ().HasData (tenant2);

        // Page Seed (TENANT 1) 
        int pageCounterIDTenant1 = 1 ;
        PageSeed (builder,TenantID1,pageCounterIDTenant1);

        // Page Seed (TENANT 2)
        int pageCounterIDTenant2 = 10 ;
        PageSeed (builder,TenantID2,pageCounterIDTenant2);


        // 2. SEED GLOBAL ROLES  (IdentityRoles)
        _ = guidArray[2];
        Guid GlobalRoleID2 = guidArray[3];



        // Password Hasher
        var hasher = new PasswordHasher<ApplicationUser>();

        // Global Admin  (User ID) 
        Guid UserIdGlobal1 = guidArray[4];

        // Global Admin
        var adminGlobalEmail = "admin@system.com";
        var newAdmin = new ApplicationUser { Id = UserIdGlobal1.ToString(), UserName = adminGlobalEmail, Email = adminGlobalEmail, EmailConfirmed = true };

        // Hash the password (Global Admin Password Hash)
        newAdmin.PasswordHash = hasher.HashPassword (newAdmin,"Focus@1nm");

        // Create Global Admin User
        _ = builder.Entity<ApplicationUser> ().HasData (newAdmin);

        // 3. SEED THE MAPPING LAST
        _ = builder.Entity<IdentityUserRole<string>> ().HasData (
        new IdentityUserRole<string> { RoleId = "GlobalAdmin",UserId = UserIdGlobal1.ToString () });



        // SEED TENANT USERS WITH GLOBAL "User" ROLE
        Guid UserId2 = guidArray[5];
        Guid UserId3 = guidArray[6];
        Guid UserId4 = guidArray[7];
        Guid UserId5 = guidArray[8];
        Guid UserId6 = guidArray[9];
        Guid UserId7 = guidArray[10];

        // For each tenant create 3 users seed
        var testUsersConfigurationSeed = new[]
        {
            new { UserId = UserId2.ToString(),RoleId = GlobalRoleID2.ToString (), Email = "tenant1.admin@test.com", MyTenantId = tenant1.TenantId , TenantRole = "Admin", TenantRoleId = 1, EmailConfirmed = true },

            new { UserId = UserId3.ToString(),RoleId = GlobalRoleID2.ToString (), Email = "tenant1.content@test.com", MyTenantId = tenant1.TenantId , TenantRole = "ContentManager", TenantRoleId = 2, EmailConfirmed = true },

            new { UserId = UserId4.ToString(),RoleId = GlobalRoleID2.ToString (), Email = "tenant1.member@test.com", MyTenantId = tenant1.TenantId , TenantRole = "Member", TenantRoleId = 3, EmailConfirmed = true },

            new { UserId = UserId5.ToString(),RoleId = GlobalRoleID2.ToString (),  Email = "tenant2.admin@test.com", MyTenantId = tenant2.TenantId  , TenantRole = "Admin", TenantRoleId = 4, EmailConfirmed = true },

            new { UserId = UserId6.ToString(),RoleId = GlobalRoleID2.ToString (),  Email = "tenant2.content@test.com", MyTenantId = tenant2.TenantId  , TenantRole = "ContentManager", TenantRoleId = 5, EmailConfirmed = true },

            new { UserId = UserId7.ToString(),RoleId = GlobalRoleID2.ToString (),  Email = "tenant2.member@test.com", MyTenantId = tenant2.TenantId , TenantRole = "Member", TenantRoleId = 6, EmailConfirmed = true }
        };

        // Create Tenant Users 
        foreach ( var config in testUsersConfigurationSeed )
        {
            // Create user entity
            var user = new ApplicationUser(config.UserId) { UserName = config.Email, Email = config.Email, EmailConfirmed = true };
            // Hash the password
            user.PasswordHash = hasher.HashPassword (user,"Focus@1nm");
            // Creae Tenant User
            _ = builder.Entity<ApplicationUser> ().HasData (user);

            _ = builder.Entity<IdentityUserRole<string>> ().HasData (
             new IdentityUserRole<string> { UserId = config.UserId.ToString (),RoleId = "User" });



            // Create Tenant User (Role tenant specific)
            _ = builder.Entity<TenantUser> ().HasData
                (new TenantUser (config.TenantRoleId) { UserId = config.UserId,TenantRole = config.TenantRole,MyTenantId = config.MyTenantId });
        }
    }

    private void PageSeed (ModelBuilder builder,Guid seedTenancyId,int id)
    {
        _ = builder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.Home,seedTenancyId,true));

        _ = builder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.AdsDetail,seedTenancyId,true));

        _ = builder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.Resources,seedTenancyId,true));

        _ = builder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.CategoryButtonMarket,seedTenancyId,true));

        _ = builder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.SubCategoryDropdownMarket,seedTenancyId,true));

        _ = builder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.SpecialMarketButton,seedTenancyId,true));

        _ = builder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.AllMarket,seedTenancyId,true));

        _ = builder.Entity<Page> ().HasData
        (new Page (++id,EnumPublicPage.NoticeAndNews,seedTenancyId,true));
    }
}
