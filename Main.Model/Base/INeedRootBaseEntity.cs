using Main.Common;

namespace Main.Model.Base;

public interface INeedRootBaseEntity
{
    void CreateParameters (BaseDataModel modelBase);

    void ModifyParameters (BaseDataModel modelBase);

    void DeleteParameters (BaseDataModel modelBase);
}