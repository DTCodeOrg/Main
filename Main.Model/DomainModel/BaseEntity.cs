using Main.Model.DomainModel;
namespace Domain.Model;

public class BaseEntity: RootBaseEntity, IMustHaveTenant
{
    public BaseEntity ()
    {
        IsActive = true;
    }

    public Guid MyTenantId
    {
        get;
        set;
    }
}
