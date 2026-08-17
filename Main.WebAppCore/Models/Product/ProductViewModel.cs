using Main.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

using WebAppCore.Helper;

namespace Main.WebAppCore.Models;

public class ProductViewModel: BaseViewModel
{
    public ProductViewModel ()
    {
        AV_Category = new List<SelectListItem> ();
        AV_SubCategory = new List<SelectListItem> ();
    }

    public int ProductID
    {
        get; set;
    }


    [Display (Name = "Product Category ")]
    [Required (ErrorMessage = "Category is required!")]
    public string CategoryID
    {
        get; set;
    }


    [Display (Name = "Product Sub Category")]
    [Required (ErrorMessage = "Sub category is required!")]
    public string SubCategoryID
    {
        get; set;
    }


    [Display (Name = "Product Name")]
    [Required (ErrorMessage = "Product Name is required!")]
    public string ProductName
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
    [Required (ErrorMessage = "Price is required!")]
    [DataType (DataType.Currency)]
    public decimal UnitPrice
    {
        get; set;
    }


    [Display (Name = "Discount")]
    [DataType (DataType.Currency)]
    public decimal Discount
    {
        get; set;
    }


    [Display (Name = "Sales Commission")]
    [DataType (DataType.Currency)]
    public decimal SaleCommission
    {
        get; set;
    }


    public bool? IsBrandNew
    {
        get; set;
    }


    public int? LikeCount
    {
        get; set;
    }


    [Display (Name = "Search Tags (Comma Seoerated)")]
    public string? SearchTag
    {
        get; set;
    }


    [Display (Name = "Product Category")]
    public string? CategoryText
    {
        get; set;
    }


    [Display (Name = "Sub Category")]
    public string? SubCategoryText
    {
        get; set;
    }

    public void SetDisplaytext ()
    {
        CategoryText = DropDownListItems.GetCategoryText (CategoryID);

        SubCategoryText = DropDownListItems.GetCategoryText (SubCategoryID);
    }

    public IEnumerable<SelectListItem> AV_Category
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AV_SubCategory
    {
        get; set;
    }

    public List<ImageFile> ImageFiles { get; set; } = new List<ImageFile> ();

}






























































