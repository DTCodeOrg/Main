using Domain.Model;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            TenantRole = roleName,
            MyTenantId = tenantId
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

        return applicationUser;
    }

    public async Task<ApplicationUser?> FindByNameIdAsync (string id)
    {
        ApplicationUser?  applicationUser
            = await _context.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>
            (a => a.Id == id.ToString() );

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

        TenantUser? userTenants =
        await _context.TenantUsers.FirstOrDefaultAsync<TenantUser>
        (a => a.MyTenantId == tenantId && a.UserId == applicationUser.Id);

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

    public async Task<ApplicationUser?> ApplicationUsers (string userId)
    {
        ApplicationUser? user = await _context.ApplicationUsers
        .FirstOrDefaultAsync<ApplicationUser>(a => a.Id == userId);

        return user;
    }

    public async Task<List<ApplicationUser?>> ApplicationUsers ()
    {
        List<ApplicationUser?> userList = await _context.ApplicationUsers
        .ToListAsync<ApplicationUser?>();

        return userList;
    }

    public async Task<bool> IsEmailConfirmedAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await FindByEmailAsync (email);

        if ( applicationUser != null )
        {
            bool result = applicationUser.EmailConfirmed;
            return result;
        }

        return false;
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
}
