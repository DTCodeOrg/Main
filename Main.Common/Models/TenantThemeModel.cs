namespace Main.Common;

public class TenantThemeModel
{
    public TenantThemeModel ()
    {
        Default = false;
    }

    public bool Default
    {
        get; set;
    }

    public Guid Id
    {
        get; set;
    }

    public Guid TenantId
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

    public string? LogoFilePath
    {
        get; set;
    }

}