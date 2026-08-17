using Main.Common;
using Main.Common.Models;
namespace DataTransferModel;

public class AdminPostDataModel: DataModel
{
    public AdminPostDataModel ()
    {
        BaseDataModel = new BaseDataModel ();
    }

    public int AdminPostID
    {
        get; set;
    }

    public string PosterName
    {
        get; set;
    }

    public string PostTitle
    {
        get; set;
    }

    public string? PosterContactNumber
    {
        get; set;
    }

    public string? WebsiteUrl
    {
        get; set;
    }

    public string? ShortNote
    {
        get; set;
    }

    public string? SearchTag
    {
        get; set;
    }

    public List<AdminImageFileDataModel> ListAdminPostFileImages { get; set; } = new List<AdminImageFileDataModel> ();

    public List<AdminPostCommentDataModel> ListAdminPostComments { get; set; } = new List<AdminPostCommentDataModel> ();

    public EnumPostType PostType
    {
        get; set;
    }

    public string? EnumAdminPostTypeDescription
    {
        get; set;
    }

}
