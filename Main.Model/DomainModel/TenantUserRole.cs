using Main.Common;
using Main.Model.DomainModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class TenantUserRole: RootBaseEntity
{
    public TenantUserRole (int id)
    {
        TenantUserId = id;

        TenantCountry = Country.Bangladesh;

        IsActive = true;
    }

    public TenantUserRole ()
    {
    }

    [Key]
    public int TenantUserId
    {
        get; set;
    }

    public string UserId
    {
        get; set;
    }


    [ForeignKey ("UserId")]
    public virtual ApplicationUser User
    {
        get; set;
    }


    public string TenantRole
    {
        get; set;
    }


    public Guid TenantId
    {
        get; set;
    }


    [ForeignKey ("TenantId")]
    public Tenant? Tenant
    {
        get; set;
    }

}