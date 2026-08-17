using Main.Common;
using Main.Common.Models;

namespace DataTransferModel;

public class PanelDataModel: DataModel
{
    public PanelDataModel ()
    {
        ListPosts = new List<PostDataModel> ();
    }

    public PanelDataModel (EnumPanelTemplate enumPanelTemplate,int pageId,string panelTitle)
    {
        ListPosts = new List<PostDataModel> ();

        PanelTemplate = enumPanelTemplate;

        PageID = pageId;

        PanelTitle = panelTitle;
    }


    public int PanelID
    {
        get; set;
    }

    public int PageID
    {
        get; set;
    }

    public int PanelPosition
    {
        get; set;
    }

    public string PanelTitle
    {
        get; set;
    }

    public EnumPanelTemplate PanelTemplate
    {
        get; set;
    }

    public List<PostDataModel> ListPosts
    {
        get; set;
    }

    public void CreatePost (PostDataModel panelPost)
    {
        ListPosts ??= new List<PostDataModel> ();

        if ( panelPost != null )
        {
            ListPosts.Add (panelPost);
        }
    }
}
