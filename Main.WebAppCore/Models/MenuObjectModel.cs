using Main.Common;
using Main.Common.Models;
using Main.WebAppCore.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.WebAppCore.Models;

public class MenuObjectModel
{

    public MenuObjectModel (bool isAdvancedSearch)
    {
        AV_Category = DropDownListItems.GetCategoryList ();

        AV_SubCategory = DropDownListItems.GetSubCategoryList ();
    }

    public MenuObjectModel ()
    {
        ListCategory = new List<TenantVariableModel> ();
        ListSubCategory = new List<TenantVariableModel> ();
        ListCategory = TenantStoreHelper.GetCategoryList ();
        ListSubCategory = TenantStoreHelper.GetSubCategoryList ();
    }

    public string TenantName
    {
        get; set;
    }

    public long? CategoryID
    {
        get; set;
    }

    public long? SubCategoryID
    {
        get; set;
    }

    public string SearchKey
    {
        get; set;
    }

    public string SimpleSearchKey
    {
        get; set;
    }

    public string SearchTag
    {
        get; set;
    }

    public string CategoryText
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AV_Category
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AV_SubCategory
    {
        get; set;
    }

    public List<TenantVariableModel> ListCategory
    {
        get; set;
    }

    public List<TenantVariableModel> ListSubCategory
    {
        get; set;
    }
}
