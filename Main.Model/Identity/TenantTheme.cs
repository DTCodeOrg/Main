using Main.Model.Base;
using System.ComponentModel.DataAnnotations;

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


    public string? PrimaryColor
    {
        get; set;
    }
    public string? SecondaryColor
    {
        get; set;
    }

    public string? BackgroundColor
    {
        get; set;
    }

    public string? FontStack
    {
        get; set;
    }

    public string? LogoFileName
    {
        get; set;
    }

}