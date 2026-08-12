using Main.Common;
using Main.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Main.Model.Identity;

public class TenantInvitation: RootBaseEntity
{
    public TenantInvitation ()
    {
    }

    [Key]
    public Guid InviteId
    {
        get; set;
    }

    public Guid TenantId
    {
        get; set;
    }

    [ForeignKey ("TenantId")]
    public virtual Tenant? Tenant
    {
        get; set;
    }

    public string Email
    {
        get; set;
    } = string.Empty;

    public string? InvitedByUserId
    {
        get; set;
    }

    public string? TenantRole
    {
        get; set;
    }

    public string Token
    {
        get; set;
    } = string.Empty;

    public InvitationStatus? Status
    {
        get; set;
    }

    public DateTime CreatedOn
    {
        get; set;
    } = DateTime.UtcNow;

    public DateTime ExpiresOn
    {
        get; set;
    } = DateTime.UtcNow.AddDays (7);

    public DateTime? AcceptedOn
    {
        get; set;
    }
}
