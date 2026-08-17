using Main.Common;

namespace DataTransferModel;

public class ProductFileDataModel: DataModel
{

    public ProductFileDataModel ()
    {
        BaseDataModel = new BaseDataModel ();
    }

    public int ProductImageFileID
    {
        get; set;
    }

    public byte[] ImageFileContent
    {
        get; set;
    }

    public int ProductID
    {
        get; set;
    }

    public ProductDataModel Product
    {
        get; set;
    }
}
