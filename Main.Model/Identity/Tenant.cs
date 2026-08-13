using Main.Common;
using Main.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Main.Model.Identity;

public class Tenant: RootBaseEntity
{
    public Tenant (Guid id)
    {
        TenantId = id;
        CreatedBy = null;
        CreatedDate = null;
        ModifiedDate = null;
        TenantCountry = null;
        IsActive = true;
    }

    public Tenant ()
    {

    }

    public Tenant (HostType hostType)
    {
        HostType = hostType;
    }

    [Key]
    public Guid TenantId
    {
        get; set;
    }

    [Required]
    public string TenantName
    {
        get; set;
    }

    [Required]
    public HostType HostType
    {
        get; set;
    }

    [Required]
    public string Host
    {
        get; set;
    }

    public string? SecretKey
    {
        get; set;
    }
}