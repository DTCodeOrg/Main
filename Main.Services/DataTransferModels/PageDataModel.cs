using Main.Common;

namespace DataTransferModel;

public class PageDataModel
{
    public PageDataModel ()
    {
        ListPanels = new List<PanelDataModel> ();
    }

    public int PageID
    {
        get; set;
    }

    public EnumPublicPage EnumPublicPage
    {
        get; set;
    }

    public string? PageName
    {
        get; set;
    }

    public List<PanelDataModel> ListPanels
    {
        get; set;
    }

    public void CreatePanel (PanelDataModel pageDataModel)
    {
        ListPanels ??= new List<PanelDataModel> ();

        if ( pageDataModel != null )
        {
            ListPanels.Add (pageDataModel);
        }
    }
}
