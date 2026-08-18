using Main.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model.Tenant;

public class AdminImageFile: BaseEntity
{
    public AdminImageFile ()
    {

    }

    public AdminImageFile (byte[] ImageContent)
    {
        ImageFileContent = ImageContent;
    }

    [Key]
    public int AdminImageFileID
    {
        get; set;
    }



    public byte[]? ImageFileContent
    {
        get; set;
    }

    [Required]
    public string FilePath
    {
        get; set;
    }



    // Foreign key to AdminPost
    public int AdminPostID
    {
        get; set;
    }


    [ForeignKey ("AdminPostID")]
    public virtual AdminPost AdminPost
    {
        get; set;
    }
}
