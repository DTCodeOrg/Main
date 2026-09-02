using Main.Common;
using System.ComponentModel.DataAnnotations;
namespace Main.WebAppCore.Models;

public class PostSelectViewModel: BaseViewModel
{
    public PostSelectViewModel ()
    {
        if ( EnumPostType == EnumPostType.Product )
        {
            EnumPostTypeName = "Product";
        }
        else if ( EnumPostType == EnumPostType.AdSpace )
        {
            EnumPostTypeName = "Ad Post";
        }
    }

    public PostSelectViewModel (EnumPostType enumPostType,int rootId,int imageId,int order)
    {
        EnumPostType = enumPostType;
        RootID = rootId;
        ImageFileID = imageId;
        ImageOrderID = order;

        if ( EnumPostType == EnumPostType.Product )
        {
            EnumPostTypeName = "Product";
        }
        else if ( EnumPostType == EnumPostType.AdSpace )
        {
            EnumPostTypeName = "Ad Post";
        }
    }

    public int PanelPostID
    {
        get; set;
    }

    public EnumPostType EnumPostType
    {
        get; set;
    }

    public string? EnumPostTypeName
    {
        get; set;
    }


    public int RootID
    {
        get; set;
    }

    public int ImageOrderID
    {
        get; set;
    }

    public int ImageFileID
    {
        get; set;
    }

    public string? CategoryName
    {
        get; set;
    }

    public string? SubCategoryName
    {
        get; set;
    }

    public byte[]? ImageFileContent
    {
        get; set;
    }

    public string? FilePath
    {
        get; set;
    }


    public string? PostTitle
    {
        get; set;
    }

    public string? ProductOwner
    {
        get; set;
    }

    [DataType (DataType.Currency)]
    public decimal? Price
    {
        get; set;
    }

    public string? Currency
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
