using Main.Common;
using Main.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model.Tenant;

public class Post: BaseEntity
{
    public Post ()
    {
        WebsiteUrl = "www.dummy.com";
    }

    public Post (
        EnumPostType postType,
        decimal? price,
        int rootId
        )
    {
        EnumPostType = postType;
        Price = price;
        RootID = rootId;
        WebsiteUrl = "www.dummy.com";
    }

    [Key]
    public int PostID
    {
        get; set;
    }


    public int? Order
    {
        get; set;
    }

    [Required]
    public EnumPostType EnumPostType
    {
        get; set;
    }

    // Product or Admin Post
    [Required]
    public int RootID
    {
        get; set;
    }

    public int? CategoryID
    {
        get; set;
    }

    public int? SubCategoryID
    {
        get; set;
    }

    public byte[]? FileContent
    {
        get; set;
    }


    public string? FilePath
    {
        get; set;
    }

    public string? Title
    {
        get; set;
    }

    public string? ProductOwner
    {
        get; set;
    }


    [DataType (DataType.Currency)]
    public decimal? Price
    {
        get; set;
    }

    public string? WebsiteUrl
    {
        get; set;
    }

    [Required]
    public int PanelID
    {
        get; set;
    }


    [ForeignKey ("PanelID")]
    public virtual Panel Panel
    {
        get; set;
    }
}
