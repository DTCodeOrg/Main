namespace Main.Common.Models;

public class TenantDataModel
{
    public TenantDataModel ()
    {
        TenantThemeModel = new TenantThemeModel ();
    }

    public TenantDataModel
    (Guid tenantId,string tenantName,string host,string? key)
    {
        ResolvedTenantId = tenantId;
        TenantName = tenantName;
        Host = host;
        SecretKey = key;
        TenantThemeModel = new TenantThemeModel ();
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

    public StoreType StoreType
    {
        get; set;
    }

    public string? SecretKey
    {
        get; set;
    }

    public TenantThemeModel TenantThemeModel
    {
        get; set;
    }

}
