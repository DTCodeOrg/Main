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


    [Display (Name = "Product Category ")]
    public int? CategoryID
    {
        get; set;
    }

    [Display (Name = "Product Sub-Category ")]
    public int? SubCategoryID
    {
        get; set;
    }


    [Display (Name = "Product Name")]
    [Required (ErrorMessage = "Product Name is required!")]
    public string ProductName
    {
        get; set;
    }

    [Display (Name = "Product Owner")]
    public string? ProductOwner
    {
        get; set;
    }

    [Display (Name = "Description")]
    [StringLength (4000)]
    public string? Description
    {
        get; set;
    }


    [Display (Name = "Price (Taka)")]
    [DataType (DataType.Currency)]
    public decimal? UnitPrice
    {
        get; set;
    }


    [Display (Name = "Discount")]
    [DataType (DataType.Currency)]
    public decimal? Discount
    {
        get; set;
    }


    [Display (Name = "Sales Commission")]
    [DataType (DataType.Currency)]
    public decimal? SaleCommission
    {
        get; set;
    }


    [Display (Name = "Search Tags (comma-separated)")]
    [StringLength (4000)]
    public string? SearchTag
    {
        get; set;
    }


    [Display (Name = "Product Category")]
    public string? CategoryText
    {
        get; set;
    }

    [Display (Name = "Product Sub-Category")]
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






























































