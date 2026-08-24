namespace Main.WebAppCore.Models;

public class HomeViewModel: BaseViewModel
{
    public HomeViewModel ()
    {
        PageViewModel = new PageViewModel ();
    }

    public HomeViewModel (string pageName)
    {
        PageName = pageName;
        PageViewModel = new PageViewModel ();
    }

    public PageViewModel PageViewModel
    {
        get; set;
    }
}
