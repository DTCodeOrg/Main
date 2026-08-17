using Main.Common;

namespace Main.Model.DomainModel;

public interface INeedRootBaseEntity
{
    void CreateParameters (BaseDataModel modelBase);

    void ModifyParameters (BaseDataModel modelBase);

    void DeleteParameters (BaseDataModel modelBase);
}