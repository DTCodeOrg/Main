using Main.Common;
namespace Main.Model.Base;

public interface IMustHaveTenant
{
    Guid MyTenantId
    {
        get; set;
    }

    Country? TenantCountry
    {
        get; set;
    }

    string? TenantContinent
    {
        get; set;
    }

    void ModifyParameters (BaseDataModel modelBase);

    void CreateParameters (BaseDataModel modelBase);

    void DeleteParameters (BaseDataModel modelBase);

    void AddSessionParameters (BaseDataModel modelBase);
}

