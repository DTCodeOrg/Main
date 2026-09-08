using System.ComponentModel.DataAnnotations;

namespace Main.WebAppCore.Models;

public class ProductDisplayViewModel
{
    public ProductDisplayViewModel ()
    {
    }

    public int? ProductID
    {
        get; set;
    }


    [Display (Name = "Type")]
    public string DisplayCategory
    {
        get; set;
    }


    [Display (Name = "Sub-Type")]
    public string? DisplaySubCategory
    {
        get; set;
    }

    [Display (Name = "Product")]
    public string ProductName
    {
        get; set;
    }

    [Display (Name = "From")]
    public string? ProductOwner
    {
        get; set;
    }


    [Display (Name = "Price (Taka)")]
    [DataType (DataType.Currency)]
    public decimal? UnitPrice
    {
        get; set;
    }

    [Display (Name = "Tenant Name")]
    public string TenantName
    {
        get; set;
    }
}






























































