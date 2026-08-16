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
}