using Main.Common;
using Main.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model.Identity;

public class TenantUserRole: RootBaseEntity
{
    public TenantUserRole (int id)
    {
        TenantUserRoleId = id;

        TenantCountry = Country.Bangladesh;

        IsActive = true;
    }

    public TenantUserRole ()
    {
    }

    [Key]
    public int TenantUserRoleId
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
    public virtual Tenant? Tenant
    {
        get; set;
    }

}