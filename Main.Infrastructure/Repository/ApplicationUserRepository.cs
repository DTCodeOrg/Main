using Domain.Model;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Main.Repository;

public class ApplicationUserRepository: IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ExceptionLoggingService>  _logger;

    public ApplicationUserRepository (UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,ApplicationDbContext context,
    ILogger<ExceptionLoggingService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _logger = logger;
    }

    public async Task<bool> AddToRoleAsync (string email,string roleName)
    {
        ApplicationUser? applicationUser = await FindByEmailAsync (email);

        var result = await _userManager.AddToRoleAsync (applicationUser!,roleName);

        return result.Succeeded == true;
    }

    public async Task<bool> AddToTenantRoleAsync (string email,Guid tenantId,string roleName)
    {
        ApplicationUser? applicationUser = await FindByEmailAsync (email);

        TenantUser userTenant = new ()
        {
            UserId = applicationUser!.Id,
            TenantRole = roleName
        };

        _ = _context.TenantUsers.Add (userTenant);
        var result = await _context.SaveChangesAsync ();
        return result > 0;
    }

    public async Task<ApplicationUser?> FindByEmailAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        _logger.LogWarning ("Repo Email:" + applicationUser?.Email!);

        return applicationUser;
    }

    public async Task<ApplicationUser?> FindByNameIdAsync (string id)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Id == id.ToString() );

        _logger.LogWarning ("Repo Email (by id):" + applicationUser?.Email!);

        return applicationUser;
    }

    public async Task<bool> PasswordSignInAsync (string email,string password,bool isPersistent,bool lockoutFailure)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );


        if ( applicationUser == null )
        {
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync (
            applicationUser!,
            password,
            isPersistent,
            lockoutOnFailure: lockoutFailure);

        _logger.LogWarning ("Repo signin resut (by eail..):" + result);

        if ( result.Succeeded )
        {
            return true;
        }


        return false;
    }

    public async Task<bool> CreateAsync (ApplicationUser userIdentityEntity,
    string password)
    {
        var result = await _userManager.CreateAsync (userIdentityEntity, password);

        return result.Succeeded == true;
    }

    public async Task<bool> ChangePasswordAsync (string email,string password,string rePassword)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser == null )
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(applicationUser, password, rePassword);

        return result.Succeeded == true;
    }

    public async Task<string?> GenerateEmailConfirmationTokenAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser == null )
        {
            return "";
        }

        string? code = await _userManager.GenerateEmailConfirmationTokenAsync (applicationUser);

        return code;
    }

    public async Task<bool> ConfirmEmailAsync (string email,string token)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser == null )
        {
            return false;
        }

        var result = await _userManager.ConfirmEmailAsync (applicationUser,token);

        return result.Succeeded == true;
    }

    public async Task<List<string>> GetRolesAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser == null )
        {
            return new List<string> ();
        }

        var roles = await _userManager.GetRolesAsync (applicationUser);

        if ( roles == null )
        {
            return new List<string> ();
        }

        List<string> listRoles = [];

        if ( roles != null && roles.Any () )
        {
            listRoles.Add (roles[0]);
            return listRoles;
        }

        return new List<string> ();
    }

    public async Task<string> GetTenantRolesAsync (string email,Guid tenantId)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser == null )
        {
            return "";
        }

        TenantUser? userTenants =
        await _context.TenantUsers.FirstOrDefaultAsync<TenantUser>
        (a => a.MyTenantId == tenantId && a.UserId == applicationUser.Id);

        return userTenants == null ? "" : userTenants.TenantRole;
    }

    public async Task<bool> SetLockoutEndDateAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser == null )
        {
            return false;
        }

        IdentityResult result = await _userManager.SetLockoutEndDateAsync(applicationUser, null);

        return result.Succeeded == true;
    }

    public async Task<bool> ResetAccessFailedCountAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser == null )
        {
            return false;
        }

        IdentityResult result = await _userManager.ResetAccessFailedCountAsync(applicationUser);

        return result.Succeeded == true;
    }

    public async Task<List<ApplicationUser>?> ApplicationUsers ()
    {

        List<ApplicationUser> identityUsers = await _context.ApplicationUsers.ToListAsync<ApplicationUser>();

        return identityUsers.ToList ();
    }

    public async Task<bool> IsEmailConfirmedAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser != null )
        {
            bool result = applicationUser.EmailConfirmed;
            return result;
        }

        return false;
    }

    public async Task AddClaimAsync (string email,Claim claimType)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser != null )
        {
            _ = await _userManager.AddClaimAsync (applicationUser,claimType);
        }
    }

    public async Task SignOutAsync ()
    {
        await _signInManager.SignOutAsync ();
    }

    public async Task<string> GeneratePasswordResetTokenAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

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
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Email == email.ToString() );

        if ( applicationUser != null )
        {
            _ = await _userManager.ResetPasswordAsync (applicationUser,token,confirmPassword);
            return true;
        }

        return false;
    }
}
