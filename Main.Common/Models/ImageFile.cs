using Microsoft.AspNetCore.Http;

namespace Main.Common.Models;

public class ImageFile
{
    public ImageFile ()
    {
    }

    public ImageFile (byte[] fileContent)
    {
        FileContent = fileContent;
    }

    public ImageFile (byte[] fileContent,int? postId,int fileId)
    {
        FileContent = fileContent;
        PostID = postId;
    }

    public int FileID
    {
        get; set;
    }

    public byte[]? FileContent
    {
        get; set;
    }

    public string? SessionFilePath
    {
        get; set;
    }

    public string? RelativeFilePath
    {
        get; set;
    }

    public string? FileName
    {
        get; set;
    }

    public IFormFile? File
    {
        get; set;
    }

    public int? PostID
    {
        get; set;
    }

    public EnumPostType? PostType
    {
        get; set;
    }
}
