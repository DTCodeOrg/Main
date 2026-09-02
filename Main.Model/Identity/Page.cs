using Main.Common;
using Main.Model.Base;
using Main.Model.Tenant;
using System.ComponentModel.DataAnnotations;
namespace Main.Model.Identity;

public class Page: RootBaseEntity
{
    public Page (int id,EnumPublicPage page,Guid seedTenancyId,bool isSeed)
    {
        PageID = id;
        EnumPublicPage = page;
        MyTenantId = seedTenancyId;
        TenantCountry = Country.Bangladesh;
        IsActive = true;
    }

    public Page ()
    {
        ListPanels = new List<Panel> ();
    }

    public Page (EnumPublicPage enumPublicPage)
    {
        ListPanels = new List<Panel> ();
        EnumPublicPage = enumPublicPage;
    }

    public Page (EnumPublicPage enumPublicPage,Guid tenantId,bool isSeed)
    {
        ListPanels = new List<Panel> ();
        EnumPublicPage = enumPublicPage;
        MyTenantId = tenantId;
        TenantCountry = Country.Bangladesh;
        IsActive = true;
    }


    [Key]
    public int PageID
    {
        get; set;
    }


    [Required]
    public EnumPublicPage EnumPublicPage
    {
        get; set;
    }

    public virtual ICollection<Panel> ListPanels { get; set; } = new HashSet<Panel> ();


    public void CreatePanel (Panel panel)
    {
        ListPanels ??= new List<Panel> ();

        if ( panel != null )
        {
            if ( ListPanels.Any<Panel> () )
            {
                int? position = ListPanels.OrderBy ( a => a.PanelPosition ).Last().PanelPosition;

                panel.PanelPosition = position + 1;

                panel.PageID = PageID;
            }
            else
            {
                panel.PanelPosition = 1;

                panel.PageID = PageID;
            }

            ListPanels.Add (panel);
        }
    }

    public Guid MyTenantId
    {
        get; set;
    }
}
