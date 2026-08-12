using Main.Common;
using Microsoft.AspNetCore.Identity;

namespace Main.Model.Base;

public class IdentityBase: IdentityUser, INeedRootBaseEntity
{
    public IdentityBase ()
    {
    }

    public void CreateParameters (BaseDataModel modelBase)
    {
        CreatedBy = modelBase.CreatedBy;
        CreatedDate = modelBase.CreatedDate;

        ModifiedBy = null;
        ModifiedDate = null;

        DeletedBy = null;
        DeletedDate = null;

        IsActive = true;

        AddSessionParameters (modelBase);
    }

    public void ModifyParameters (BaseDataModel modelBase)
    {
        ModifiedBy = modelBase.ModifiedBy;
        ModifiedDate = modelBase.ModifiedDate;
        DeletedDate = null;
        DeletedBy = null;
        IsActive = true;

        AddSessionParameters (modelBase);
    }

    public void DeleteParameters (BaseDataModel modelBase)
    {
        DeletedBy = modelBase.DeletedBy;
        DeletedDate = modelBase.DeletedDate;
        IsActive = false;

        AddSessionParameters (modelBase);
    }

    public void AddSessionParameters (BaseDataModel modelBase)
    {
        TenantCountry = modelBase.TenantCountry;
        TenantContinent = modelBase.TenantContinent?.Trim ();
    }

    public string? CreatedBy
    {
        get; set;
    }

    public string? ModifiedBy

    {
        get; set;
    }

    public string? DeletedBy
    {
        get; set;
    }

    public DateTime? CreatedDate
    {
        get; set;
    }

    public DateTime? ModifiedDate
    {
        get; set;
    }

    public DateTime? DeletedDate
    {
        get; set;
    }

    public bool IsActive
    {
        get; set;
    }

    public Country? TenantCountry
    {
        get; set;
    }

    public string? TenantContinent
    {
        get;
        set;
    }
}