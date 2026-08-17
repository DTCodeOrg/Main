namespace Main.WebAppCore.Models;

public class AdminImageFileViewModel
{
    public AdminImageFileViewModel ()
    {
    }

    public int AdminImageFileID
    {
        get; set;
    }

    public byte[] ImageFileContent
    {
        get; set;
    }

    public int AdminPostID
    {
        get; set;
    }
}
