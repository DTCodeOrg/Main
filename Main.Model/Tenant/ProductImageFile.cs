using Main.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model.Tenant;

public class ProductImageFile: BaseEntity
{
    public ProductImageFile ()
    {
    }

    public ProductImageFile (byte[]? imageFileContent)
    {
        FileContent = imageFileContent;
    }

    [Key]
    public int ProductImageFileID
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

    public int ProductID
    {
        get; set;
    }


    [ForeignKey ("ProductID")]
    public virtual Product Product
    {
        get; set;
    }

}
