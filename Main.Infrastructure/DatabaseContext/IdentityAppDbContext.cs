using Domain.Model;
using Main.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure.DatabaseContext;

public class IdentityAppDbContext: IdentityDbContext<ApplicationUser>
{
    private readonly ITenantSetter _tenantSetter;

    public IdentityAppDbContext (DbContextOptions options) : base (options)
    {
    }

    public IdentityAppDbContext (DbContextOptions options,
    ITenantSetter tenantSetter) : base (options)
    {
        _tenantSetter = tenantSetter;
    }

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

    public DbSet<ApplicationUser> ApplicationUsers
    {
        get; set;
    }

    public DbSet<UserRefreshToken> ApplicationUserRefreshTokens
    {
        get; set;
    }

    public DbSet<Tenant> Tenants
    {
        get; set;
    }

    public DbSet<TenantSmtpServer> TenantSmtpServers
    {
        get; set;
    }

    public DbSet<TenantUserRole> TenantUserRoles
    {
        get; set;
    }

    public DbSet<TenantTheme> TenantThemes
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

    protected override void OnModelCreating (ModelBuilder builder)
    {
        base.OnModelCreating (builder);

        ConfigureEntitiesWithFluent (builder);

        ConfigureIndexes (builder);

        SeedData (builder);
    }

    private void ConfigureIndexes (ModelBuilder builder)
    {
        _ = builder.Entity<ApplicationUser> ()
            .HasIndex (u => u.Email)
            .IsUnique ();

        _ = builder.Entity<TenantInvitation> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<UserRefreshToken> ()
            .HasIndex (e => new { e.TenantId,e.Token });
    }

    private void ConfigureEntitiesWithFluent (ModelBuilder builder)
    {
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

        _ = builder.Entity<TenantInvitation> ()
            .Property (t => t.Status)
            .HasDefaultValue (InvitationStatus.Pending)
            .HasSentinel (( InvitationStatus ) ( -1 ));

        _ = builder.Entity<EmailOutboxMessage> (static entity =>
            {
                _ = entity.HasKey (t => t.Id);
                _ = entity.Property (t => t.ReceiverEmail).IsRequired ();
                _ = entity.Property (t => t.Subject).IsRequired ();
                _ = entity.Property (t => t.Body).IsRequired ();
                _ = entity.Property (t => t.CreatedOnUtc).IsRequired ();
                _ = entity.Property (t => t.RetryCount).IsRequired ();
            });

        _ = builder.Entity<UserRefreshToken> (entity =>
            {
                _ = entity.HasKey (e => e.Id);
                _ = entity.Property (e => e.Id)
                      .ValueGeneratedNever ();
                _ = entity.Property (e => e.Token)
                      .IsRequired ()
                      .HasMaxLength (2000);
                _ = entity.Property (e => e.UserId)
                      .IsRequired ()
                      .HasMaxLength (450);
                _ = entity.Property (e => e.CreatedBy).HasMaxLength (256).IsRequired (false);
                _ = entity.Property (e => e.ModifiedBy).HasMaxLength (256).IsRequired (false);
                _ = entity.Property (e => e.DeletedBy).HasMaxLength (256).IsRequired (false);
                _ = entity.Property (e => e.ReplacedByToken).HasMaxLength (2000).IsRequired (false);
                _ = entity.Property (e => e.TenantContinent).HasMaxLength (100).IsRequired (false);
                _ = entity.Property (e => e.TenantCountry).HasMaxLength (100).IsRequired (false);
            });
    }

    public void SeedData (ModelBuilder builder)
    {
        GlobalIdentityRoles (builder);

        Guid ThemeId1 = guidArray[16];
        Guid ThemeId2 = guidArray[17];

        _ = Tenant1ThemeSeed (builder,ThemeId1);
        _ = Tenant2ThemeSeed (builder,ThemeId2);

        Guid TenantId1 = guidArray[0];
        Guid TenantId2 = guidArray[1];

        TenantSeed1 (builder,TenantId1,ThemeId1);
        TenantSeed2 (builder,TenantId2,ThemeId2);

        Guid UserIdGlobal1 = guidArray[4];
        var adminGlobalEmail = "admin@system.com";
        GlobalUsers (builder,UserIdGlobal1,adminGlobalEmail);

        // UserId2, UserId3, UserId4, UserId5, UserId6, UserId7 are used for tenant users
        Guid UserId2 = guidArray[5];
        Guid UserId3 = guidArray[6];
        Guid UserId4 = guidArray[7];
        Guid UserId5 = guidArray[8];
        Guid UserId6 = guidArray[9];
        Guid UserId7 = guidArray[10];

        string GlobalTenantRole = "User";

        TenantUsers (builder,GlobalTenantRole,UserId2,UserId3,UserId4,
            UserId5,UserId6,UserId7,TenantId1,TenantId2);

    }

    private void TenantUsers (
        ModelBuilder builder,
        string globalTenantRole,Guid userId2,
        Guid userId3,Guid userId4,Guid userId5,
        Guid userId6,Guid userId7,Guid tenantId1,
        Guid tenantId2)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        // For each tenant create 3 users seed
        var testUsersConfigurationSeed = new[]
        {
            new {
                UserId = userId2.ToString(),
                RoleId = globalTenantRole,
                Email = "tenant1.admin@test.com",
                TenantId = tenantId1 ,
                TenantRole = "Admin",
                TenantRoleId = 1,
                EmailConfirmed = true
            },

            new {
                UserId = userId3.ToString(),
                RoleId = globalTenantRole,
                Email = "tenant1.content@test.com",
                TenantId = tenantId1 ,
                TenantRole = "ContentManager",
                TenantRoleId = 2,
                EmailConfirmed = true
            },

            new
            {
                UserId = userId4.ToString(),
                RoleId = globalTenantRole,
                Email = "tenant1.member@test.com",
                TenantId = tenantId1 ,
                TenantRole = "Member",
                TenantRoleId = 3,
                EmailConfirmed = true
            },

            new {
                UserId = userId5.ToString(),
                RoleId = globalTenantRole,
                Email = "tenant2.admin@test.com",
                TenantId = tenantId2  ,
                TenantRole = "Admin",
                TenantRoleId = 4,
                EmailConfirmed = true
            },

            new {
                UserId = userId6.ToString(),
                RoleId = globalTenantRole,
                Email = "tenant2.content@test.com",
                TenantId = tenantId2  ,
                TenantRole = "ContentManager",
                TenantRoleId = 5,
                EmailConfirmed = true
            },

            new {
                UserId = userId7.ToString(),
                RoleId = globalTenantRole,
                Email = "tenant2.member@test.com",
                TenantId = tenantId2 ,
                TenantRole = "Member",
                TenantRoleId = 6,
                EmailConfirmed = true
            }
        };

        // Create Tenant Users 
        foreach ( var config in testUsersConfigurationSeed )
        {

            var user = new ApplicationUser(config.UserId)
            {
                UserName = config.Email,
                Email = config.Email,
                EmailConfirmed = true
            };

            user.PasswordHash = hasher.HashPassword (user,"Focus@1nm");
            _ = builder.Entity<ApplicationUser> ().HasData (user);

            _ = builder.Entity<TenantUserRole> ().HasData (new TenantUserRole (config.TenantRoleId)
            {
                UserId = config.UserId,
                TenantRole = config.TenantRole,
                TenantId = config.TenantId
            });
        }
    }

    private void GlobalUsers (ModelBuilder builder,Guid UserIdGlobal1,string adminGlobalEmail)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        var newAdmin = new ApplicationUser
        {
            Id = UserIdGlobal1.ToString(),
            UserName = adminGlobalEmail,
            Email = adminGlobalEmail,
            EmailConfirmed = true
        };

        newAdmin.PasswordHash = hasher.HashPassword (newAdmin,"Focus@1nm");

        _ = builder.Entity<ApplicationUser> ().HasData (newAdmin);

        _ = builder.Entity<IdentityUserRole<string>> ().HasData (
       new IdentityUserRole<string>
       {
           RoleId = "GlobalAdmin",
           UserId = UserIdGlobal1.ToString ()
       });
    }

    private void TenantSeed1 (ModelBuilder builder,Guid tenantId1,Guid themeId1)
    {
        _ = builder.Entity<Tenant> ().HasData (new Tenant (tenantId1)
        {
            TenantName = "Tenant 1",
            Host = "tenant1.com",
            TenantThemeId = themeId1
        });
    }

    private void TenantSeed2 (ModelBuilder builder,Guid tenantId2,Guid themeId2)
    {
        _ = builder.Entity<Tenant> ().HasData (new Tenant (tenantId2)
        {
            TenantName = "Tenant 2",
            Host = "tenant2.com",
            TenantThemeId = themeId2
        });
    }

    private void GlobalIdentityRoles (ModelBuilder builder)
    {
        _ = builder.Entity<IdentityRole> ().HasData (
            new IdentityRole
            {
                Id = "GlobalAdmin",
                Name = "GlobalAdmin",
                NormalizedName = "GLOBALADMIN"
            },
            new IdentityRole
            {
                Id = "User",
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }

    private TenantTheme Tenant1ThemeSeed (ModelBuilder builder,Guid theme)
    {
        TenantTheme tenantTheme = new()
        {
            Id = theme,
            PrimaryColor = "#122A1E",
            SecondaryColor = "#879882",
            BackgroundColor = "#F7F8F5",
            FontStack = "Garamond, Baskerville, 'Baskerville Old Face', 'Hoefler Text', Georgia, 'Times New Roman', serif",
            LogoFileName = ""
        };

        _ = builder.Entity<TenantTheme> ().HasData (tenantTheme);

        return tenantTheme;
    }

    private TenantTheme Tenant2ThemeSeed (ModelBuilder builder,Guid theme)
    {
        TenantTheme tenantTheme = new()
        {
            Id = theme,
            PrimaryColor = "#1B3B2B",
            SecondaryColor = "#728C69",
            BackgroundColor = "#F4F6F4",
            FontStack = "system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif",
            LogoFileName = ""
        };

        _ = builder.Entity<TenantTheme> ().HasData (tenantTheme);

        return tenantTheme;
    }



    private void ApplyBaseDataTenantId ()
    {
        BaseDataModel createDataModel = _tenantSetter.CreateMetaData;
        BaseDataModel updateDataModel = _tenantSetter.UpdateMetaData;
        BaseDataModel deleteDataModel = _tenantSetter.DeleteMetaData;

        var entries = ChangeTracker.Entries()
        .Where(e => e.Entity is not IMustHaveTenant &&
              (e.State == EntityState.Added
               || e.State == EntityState.Modified
               || e.State == EntityState.Detached)).ToArray();

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
        ApplyBaseDataTenantId ();

        return base.SaveChanges (acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default)
    {
        ApplyBaseDataTenantId ();

        return await base.SaveChangesAsync (true,cancellationToken);
    }
}
