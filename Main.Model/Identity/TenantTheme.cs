using Main.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model.Identity;

public class TenantTheme: RootBaseEntity
{
    public TenantTheme ()
    {
    }

    [Key]
    public Guid Id
    {
        get; set;
    }

    public string? ButtonBGBorderColor
    {
        get; set;
    }

    public string? BodyBackgroundColor
    {
        get; set;
    }

    public string? BodyColor
    {
        get; set;
    }

    public string? MenuBackgroundColor
    {
        get; set;
    }

    public string? LogoColor
    {
        get; set;
    }

    public string? MenuItemHoverBGColor
    {
        get; set;
    }

    public string? MenuItemHoverColor
    {
        get; set;
    }

    public string? HeaderColor
    {
        get; set;
    }

    public string? FontStack
    {
        get; set;
    }

    public string? LogoFilePath
    {
        get; set;
    }

    public Guid TenantId
    {
        get; set;
    } = Guid.Empty;


    [ForeignKey ("TenantId")]
    public virtual Tenant Tenant
    {
        get; set;
    }
}