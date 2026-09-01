using Main.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Main.WebAppCore.Models;

public class ProductViewModel: BaseViewModel
{
    public ProductViewModel ()
    {
        AVCategory = new List<SelectListItem> ();
        AVSubCategory = new List<SelectListItem> ();
        ImageFiles = new List<ImageFile> ();
        PageName = "Product Page";
    }

    public int? ProductID
    {
        get; set;
    }


    [Display (Name = "Type")]
    public int? CategoryID
    {
        get; set;
    }

    [Display (Name = "Sub-Type")]
    public int? SubCategoryID
    {
        get; set;
    }


    [Display (Name = "Product")]
    [Required (ErrorMessage = "Product is required!")]
    public string ProductName
    {
        get; set;
    }

    [Display (Name = "From (Individual/Team)")]
    public string? ProductOwner
    {
        get; set;
    }

    [Display (Name = "Brief notes")]
    [StringLength (4000)]
    public string? Description
    {
        get; set;
    }


    [Display (Name = "Price (taka)")]
    [DataType (DataType.Currency)]
    public decimal? UnitPrice
    {
        get; set;
    }


    [Display (Name = "Discount (%)")]
    [DataType (DataType.Currency)]
    public decimal? Discount
    {
        get; set;
    }


    [Display (Name = "Sales Commission (%)")]
    [DataType (DataType.Currency)]
    public decimal? SaleCommission
    {
        get; set;
    }


    [Display (Name = "Name tags (comma-separated for search in web)")]
    [StringLength (4000)]
    public string? SearchTag
    {
        get; set;
    }


    [Display (Name = "Type")]
    public string? CategoryText
    {
        get; set;
    }

    [Display (Name = "Sub-Type")]
    public string? SubCategoryText
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AVCategory
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AVSubCategory
    {
        get; set;
    }

    public List<ImageFile> ImageFiles
    {
        get; set;
    }
}






























































