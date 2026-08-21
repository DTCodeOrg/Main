using Main.Common.Models;

namespace DataTransferModel;

public class AdminImageFileDataModel: DataModel
{
    public AdminImageFileDataModel ()
    {
        BaseDataModel = new BaseDataModel ();
    }

    public AdminImageFileDataModel (byte[] imageFileContent)
    {
        FileContent = imageFileContent;
        BaseDataModel = new BaseDataModel ();
    }

    public int? AdminImageFileID
    {
        get; set;
    }

    public byte[]? FileContent
    {
        get; set;
    }

    public string? FilePath
    {
        get; set;
    }

    public string? FileName
    {
        get; set;
    }

    public int? AdminPostID
    {
        get; set;
    }
}
