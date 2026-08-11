using Main.Common;

namespace Main.Infrastructure;

public interface ITenantSetter
{
    TenantDataModel CurrentTenant
    {
        get; set;
    }

    public Guid ResolvedTenantId
    {
        get; set;
    }

    Guid HttpContextTenantId
    {
        get;
    }

    string HttpContextUserId
    {
        get;
    }

    DateTime GetLocalNow ();

    BaseDataModel CreateMetaData
    {
        get;
    }

    BaseDataModel UpdateMetaData
    {
        get;
    }

    BaseDataModel DeleteMetaData
    {
        get;
    }
}
