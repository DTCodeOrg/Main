using Main.Common;
using Main.Common.Models;
using Main.WebAppCore.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.WebAppCore.Models;

public class MenuObjectModel
{
    private IStringLocalizer<SharedResources> stringLocalizer;

    public MenuObjectModel (bool isAdvancedSearch,IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        AV_Category = DropDownListItems.GetCategoryList (localizer,storeType);

        AV_SubCategory = DropDownListItems.GetSubCategoryList (localizer,storeType);
    }

    public MenuObjectModel (IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        ListCategory = new List<TenantVariableModel> ();
        ListSubCategory = new List<TenantVariableModel> ();

        ListCategory =
        TenantStoreHelper.GetFullCategoryList (localizer,storeType)
        .Where (a => a.Variable == EnumTenantVariable.ProductCategory).ToList ();

        ListSubCategory =
        TenantStoreHelper.GetFullSubCategoryList (localizer,storeType).Where
        (a => a.Variable == EnumTenantVariable.ProductSubCategory).ToList ();

        CategoryText = "Browse by Categories";
    }

    public MenuObjectModel (IStringLocalizer<SharedResources> stringLocalizer)
    {
        this.stringLocalizer = stringLocalizer;
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
