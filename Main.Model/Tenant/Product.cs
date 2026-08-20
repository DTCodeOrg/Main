using Main.Common;
using Main.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Main.Model.Tenant;

public class Product: BaseEntity
{
    public Product ()
    {
    }

    [Key]
    public int ProductID
    {
        get; set;
    }

    [Required]
    public EnumPostType PostType
    {
        get; set;
    }

    [Required]
    public string ProductName
    {
        get; set;
    }

    [MaxLength (4000)]
    public string? Description
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


    public decimal? Price
    {
        get; set;
    }

    public decimal? Discount
    {
        get; set;
    }

    public decimal? SaleCommission
    {
        get; set;
    }

    public string? SearchTag
    {
        get; set;
    }

    public virtual ICollection<ProductImageFile> ListImageFiles { get; set; } = new HashSet<ProductImageFile> ();

    public virtual ICollection<ProductComment> ListComments { get; set; } = new HashSet<ProductComment> ();

}
