using Main.Model.Base;
namespace Main.Model.Identity;

public class ApplicationUser: IdentityBase
{
    public ApplicationUser (string id)
    {
        Id = id;
    }

    public ApplicationUser ()
    {
    }

    public virtual ICollection<TenantUserRole> TenantUsers
    {
        get; set;

    } = new HashSet<TenantUserRole> ();

    public virtual ICollection<UserRefreshToken> UserRefreshTokens
    {
        get; set;

    } = new HashSet<UserRefreshToken> ();

}
