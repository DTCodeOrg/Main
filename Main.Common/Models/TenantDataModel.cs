namespace Main.Common;

public class TenantDataModel
{
    public TenantDataModel ()
    {
        ThemeModel = new TenantThemeModel ();
    }

    public TenantDataModel
    (Guid tenantId,string tenantName,string host,string? key)
    {
        ResolvedTenantId = tenantId;
        TenantName = tenantName;
        Host = host;
        SecretKey = key;
        ThemeModel = new TenantThemeModel ();
    }

    public Guid ResolvedTenantId
    {
        get; set;
    }

    public string TenantName
    {
        get; set;
    }

    public string Host
    {
        get; set;
    }

    public string? SecretKey
    {
        get; set;
    }

    public TenantThemeModel ThemeModel
    {
        get; set;
    }

}
