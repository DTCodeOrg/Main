using Main.Common;
using Main.WebAppCore.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

namespace Main.WebAppCore.Models;

public class PanelViewModel: BaseViewModel
{
    public PanelViewModel ()
    {
        ListSelectPosts = new List<PostSelectViewModel> ();

        ListPosts = new List<PostViewModel> ();

        AVPanelTemplate = DropDownListItems.GetPanelTempletList ();
    }

    public PanelViewModel (EnumPanelTemplate enumPanelTemplate)
    {
        ListPosts = new List<PostViewModel> ();

        ListSelectPosts = new List<PostSelectViewModel> ();

        AVPanelTemplate = DropDownListItems.GetPanelTempletList ();

        PanelTemplate = enumPanelTemplate;
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


    [Display (Name = "Panel Title")]
    public string? PanelTitle
    {
        get; set;
    }


    [Display (Name = "Panel Template")]
    [Required (ErrorMessage = "Select a template!")]
    public EnumPanelTemplate PanelTemplate
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AVPanelTemplate
    {
        get; set;
    }

    public List<PostSelectViewModel> ListSelectPosts
    {
        get; set;
    }

    public List<PostViewModel> ListPosts
    {
        get; set;
    }

    public void CreatePanelPost (PostViewModel postViewModel)
    {
        ListPosts ??= new List<PostViewModel> ();

        if ( postViewModel != null )
        {
            ListPosts.Add (postViewModel);
        }
    }
}
