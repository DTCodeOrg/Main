using Main.Infrastructure;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Main.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class ApplicationUserRepository: IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IdentityAppDbContext _idetityContext;
    private readonly ITenantSetter _tenantSetter;

    public ApplicationUserRepository (UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,IdentityAppDbContext context,
     ITenantSetter tenantSetter)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _idetityContext = context;
        _tenantSetter = tenantSetter;
    }


    public async Task<bool> AddToRoleAsync (string email)
    {
        ApplicationUser? applicationUser =  await _userManager.FindByEmailAsync (email);

        if ( applicationUser != null )
        {
            IdentityResult result = await _userManager.AddToRoleAsync (applicationUser,"User");

            return result.Succeeded;
        }

        return false;
    }

    public async Task<bool> AddToTenantRoleAsync (string email,Guid tenantId,string roleName)
    {
        ApplicationUser? applicationUser = await FindByEmailAsync (email);

        TenantUserRole userTenant = new ()
        {
            UserId = applicationUser!.Id,
            TenantRole = roleName,
            TenantId = tenantId
        };

        _ = _idetityContext.TenantUserRoles.Add (userTenant);
        var result = await _idetityContext.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<ApplicationUser?> FindByEmailAsync (string email)
    {
        ApplicationUser? applicationUser = await _idetityContext.Users.FirstOrDefaultAsync(u => u.Email == email.Trim());

        return applicationUser;
    }

    public async Task<ApplicationUser?> FindByNameIdAsync (string id)
    {
        ApplicationUser? applicationUser = await _idetityContext.Users.FirstOrDefaultAsync(u => u.Id == id.Trim());

        return applicationUser;
    }

    public async Task<bool> PasswordSignInAsync (string email,string password)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync ( email);

        if ( applicationUser == null )
        {
            return false;
        }

        var result  = await _userManager.CheckPasswordAsync (applicationUser,password!);

        return result == true;
    }


    public async Task<bool> CreateAsync (ApplicationUser userIdentityEntity,
    string password)
    {
        userIdentityEntity.CreateParameters (_tenantSetter.CreateMetaData);

        var result = await _userManager.CreateAsync (userIdentityEntity, password);

        return result.Succeeded == true;
    }


    public async Task<bool> ChangePasswordAsync (string email,string password,string rePassword)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync ( email);

        if ( applicationUser == null )
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(applicationUser, password, rePassword);

        return result.Succeeded == true;
    }


    public async Task<string> GenerateEmailConfirmationTokenAsync (string email)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync ( email);

        if ( applicationUser == null )
        {
            return string.Empty;
        }

        string? code = await _userManager.GenerateEmailConfirmationTokenAsync (applicationUser);

        return code;
    }


    public async Task<bool> ConfirmEmailAsync (string email,string token)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync ( email);


        if ( applicationUser == null )
        {
            return false;
        }

        var result = await _userManager.ConfirmEmailAsync (applicationUser,token);

        return result.Succeeded == true;
    }


    public async Task<string> GetRolesAsync (string email)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync ( email);

        if ( applicationUser == null )
        {
            return string.Empty;
        }

        var roles = await _userManager.GetRolesAsync (applicationUser);

        if ( roles == null )
        {
            return string.Empty;
        }

        return roles.First<string> ();
    }


    public async Task<string> GetTenantRolesAsync (string email,Guid tenantId)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync ( email);

        if ( applicationUser == null )
        {
            return string.Empty;
        }

        TenantUserRole? userTenants =
        await _idetityContext.TenantUserRoles.FirstOrDefaultAsync<TenantUserRole>
        (a => a.TenantId == tenantId && a.UserId == applicationUser.Id);

        return userTenants == null ? string.Empty : userTenants.TenantRole;
    }


    public async Task<bool> SetLockoutEndDateAsync (string email)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync ( email);

        if ( applicationUser == null )
        {
            return false;
        }

        IdentityResult result = await _userManager.SetLockoutEndDateAsync(applicationUser, null);

        return result.Succeeded == true;
    }


    public async Task<bool> ResetAccessFailedCountAsync (string email)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync ( email);

        if ( applicationUser == null )
        {
            return false;
        }

        IdentityResult result = await _userManager.ResetAccessFailedCountAsync(applicationUser);

        return result.Succeeded == true;
    }


    public async Task<List<ApplicationUser?>> ApplicationUsers ()
    {
        List<ApplicationUser?> userList = await _userManager.Users.ToListAsync<ApplicationUser?>();
        return userList.ToList ();
    }

    public async Task<bool> IsEmailConfirmedAsync (string email)
    {
        ApplicationUser?  applicationUser = await FindByEmailAsync (email);

        bool result = false;

        if ( applicationUser != null )
        {
            result = applicationUser.EmailConfirmed;
        }

        return result;
    }

    public async Task<string> GeneratePasswordResetTokenAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await FindByEmailAsync (email);

        if ( applicationUser != null )
        {
            string result = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            return result;
        }

        return string.Empty;
    }

    public async Task<bool> ResetPasswordAsync (string email,string token,string confirmPassword)
    {
        ApplicationUser?  applicationUser
            = await FindByEmailAsync (email);

        if ( applicationUser != null )
        {
            _ = await _userManager.ResetPasswordAsync (applicationUser,token,confirmPassword);
            return true;
        }

        return false;
    }

    public async Task<ApplicationUser?> ApplicationUsers (string userId)
    {
        return await _userManager.FindByIdAsync (userId);
    }
}
