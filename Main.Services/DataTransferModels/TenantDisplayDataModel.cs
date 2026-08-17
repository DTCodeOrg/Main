using Main.Common.Models;

namespace DataTransferModel;

public class TenantDisplayDataModel
{
    public TenantDisplayDataModel ()
    {
    }

    public TenantDisplayDataModel
    (Guid tenantId,string tenantName,string host,string? key)
    {
        MyTenantId = tenantId;
        TenantName = tenantName;
        Host = host;
        SecretKey = key;
    }

    public Guid MyTenantId
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
