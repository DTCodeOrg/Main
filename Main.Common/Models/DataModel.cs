namespace Main.Common.Models;

public class DataModel
{
    public DataModel ()
    {
    }

    public BaseDataModel BaseDataModel
    {
        get;
        set;
    }

    public void SetBaseDataModel (BaseDataModel baseDataModel)
    {
        BaseDataModel = new BaseDataModel ();

        BaseDataModel = baseDataModel;
    }
}
