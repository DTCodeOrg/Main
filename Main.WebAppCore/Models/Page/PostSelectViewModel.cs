using Main.Common;

using System.ComponentModel.DataAnnotations;
namespace Main.WebAppCore.Models;

public class PostSelectViewModel
{
    public PostSelectViewModel ()
    {
    }


    public PostSelectViewModel (EnumPostType enumPostType,int rootId,int imageId,int order)
    {
        EnumPostType = enumPostType;
        RootID = rootId;
        ImageFileID = imageId;
        ImageOrderID = order;
    }

    public int PanelPostID
    {
        get; set;
    }


    public EnumPostType EnumPostType
    {
        get; set;
    }


    public int RootID
    {
        get; set;
    } // Admin or Company (Key) of EnumPostType


    public int ImageOrderID
    {
        get; set;
    }


    public int ImageFileID
    {
        get; set;
    }

    public string CategoryName
    {
        get; set;
    }


    public byte[]? ImageFileContent { get; set; } = null;


    public string PostTitle
    {
        get; set;
    }

    [DataType (DataType.Currency)]
    public decimal Price
    {
        get; set;
    }

    public string Currency
    {
        get; set;
    }


    public int PanelID
    {
        get; set;
    }


    public int PageID
    {
        get; set;
    }
}
