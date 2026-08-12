using Main.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model.Identity;

public class UserRefreshToken: RootBaseEntity
{
    public UserRefreshToken ()
    {
    }

    [Key]
    public Guid Id
    {
        get; set;
    }

    [Required]
    public string UserId
    {
        get; set;
    }

    [ForeignKey (nameof (UserId))]
    public virtual ApplicationUser User
    {
        get;
        set;
    }

    public string Token
    {
        get; set;
    }

    public DateTime ExpiresAt
    {
        get; set;
    }

    public bool IsRevoked
    {
        get; set;
    }

    public DateTime CreatedAt
    {
        get; set;
    }

    public string? ReplacedByToken
    {
        get; set;
    }

    public Guid TenantId
    {
        get; set;
    }

    [ForeignKey (nameof (TenantId))]
    public virtual Tenant? Tenant
    {
        get; set;
    }

}