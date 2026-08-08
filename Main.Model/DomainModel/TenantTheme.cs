using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class TenantTheme
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