using Main.Common;
using Main.WebAppCore.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.WebAppCore.Models;

public class PostViewModel: BaseViewModel
{
    public PostViewModel ()
    {
    }

    public PostViewModel (StoreType storeType,IStringLocalizer<SharedResource> localizer)
    {
        AVCategory = DropDownListItems.GetCategoryList (localizer,storeType);
    }


    public PostViewModel (StoreType storeType,EnumPostType enumPostType,int rootId,int imageId,
        int order,IStringLocalizer<SharedResource> localizer)
    {
        AVCategory = DropDownListItems.GetCategoryList (localizer,storeType);
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
    }


    public int ImageOrderID
    {
        get; set;
    }


    public int ImageFileID
    {
        get; set;
    }


    public int CategoryID
    {
        get; set;
    }


    public string GetTextCategory ()
    {
        var CategoryText = string.Empty;

        AVCategory.ToList ().ForEach (x =>
        {
            if ( x.Value == CategoryID.ToString () )
            {
                CategoryText = x.Text;
            }
        });

        return CategoryText;
    }


    public IEnumerable<SelectListItem> AVCategory
    {
        get; set;
    }


    public string CategoryName
    {
        get
        {
            return GetTextCategory ();
        }
    }


    public byte[]? ImageFileContent
    {
        get; set;
    }

    public string FilePath
    {
        get; set;
    }

    public string PostTitle
    {
        get; set;
    }


    public string? PostDescription
    {
        get; set;
    }


    public decimal? Price
    {
        get; set;
    }


    public string? WebsiteUrl
    {
        get; set;
    }


    public int PanelID
    {
        get; set;
    }


    public PanelViewModel? PagePanel
    {
        get; set;
    }


    public int PageID
    {
        get; set;
    }


    public int ImageArea
    {
        get; set;
    }
}
