namespace Main.WebAppCore.Models;

public class HomeViewModel: BaseViewModel
{
    public HomeViewModel ()
    {
    }

    public HomeViewModel (string pageName)
    {
        PageName = pageName;
    }
}
