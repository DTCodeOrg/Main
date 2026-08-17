
using System.ComponentModel;

namespace Main.WebAppCore.Models;

public class AdminPostDisplayViewModel
{
    public AdminPostDisplayViewModel ()
    {
    }

    public int AdminPostID
    {
        get; set;
    }


    [DisplayName ("Poster Name")]
    public string PosterName
    {
        get; set;
    }


    [DisplayName ("Post Title")]
    public string PostTitle
    {
        get; set;
    }


    [DisplayName ("Post Type")]
    public string DiispayPostType
    {
        get; set;
    }


    [DisplayName ("Host Company")]
    public string DisplayCompanyName
    {
        get; set;
    }
}
