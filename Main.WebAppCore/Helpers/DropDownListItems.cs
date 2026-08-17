using Main.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppCore.Helper;

public class DropDownListItems
{
    public DropDownListItems ()
    {
    }



    public static IEnumerable<SelectListItem> GetPostTypeList ()
    {
        var listCountries = ListEnum.GetPostTypeList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objOfferTypeListItems = new();
        foreach ( var item in listCountries )
        {
            SelectListItem objItem = new ()
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };
            objOfferTypeListItems.Add (objItem);
        }
        return objOfferTypeListItems.AsEnumerable ();
    }



    public static IEnumerable<SelectListItem> GetAdminPostTypeList ()
    {
        var listCountries = ListEnum.GetAdminPostTypeList().ToList();

        List<SelectListItem> objOfferTypeListItems = new();
        SelectListItem objItem;

        foreach ( var item in listCountries )
        {
            objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString ()
            };
            objOfferTypeListItems.Add (objItem);
        }

        return objOfferTypeListItems.AsEnumerable ();
    }



    public static IEnumerable<SelectListItem> GetPageList ()
    {
        var listCountries = ListEnum.GetPublicPages().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objCoutryListItems = new();
        foreach ( var item in listCountries )
        {
            SelectListItem objItem = new ()
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };
            objCoutryListItems.Add (objItem);
        }
        return objCoutryListItems.AsEnumerable ();
    }



    public static IEnumerable<SelectListItem> GetPanelTempletList ()
    {
        var listColumns = ListEnum.GetPanelTempletList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCurrencyListItems = new();

        foreach ( var item in listColumns )
        {
            SelectListItem objItem = new ()
            {
                Text = item.Text,
                Value = item.ValueID.ToString()
            };
            objCurrencyListItems.Add (objItem);
        }

        SelectListItem objItem1 = new ()
        {
            Text = "",
            Value = ""
        };

        objCurrencyListItems.Add (objItem1);

        return objCurrencyListItems.AsEnumerable ();
    }


    public static IEnumerable<SelectListItem> GetCurrencyList ()
    {
        var listCurrency = ListEnum.GetCurrencyList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCurrencyListItems = [new SelectListItem ( ) { Value = null,Text = "" }];

        foreach ( TenantVariableModel? item in listCurrency )
        {
            SelectListItem objItem = new ()
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };
            objCurrencyListItems.Add (objItem);
        }

        return objCurrencyListItems.AsEnumerable ();
    }


    public static IEnumerable<SelectListItem> GetCountryList ()
    {
        var listCountries = ListEnum.GetCountryList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCountryListItems = new();

        foreach ( TenantVariableModel? item in listCountries )
        {
            SelectListItem objItem = new ()
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };

            objCountryListItems.Add (objItem);
        }

        return objCountryListItems.AsEnumerable ();
    }


    public static IEnumerable<SelectListItem> GetSubCategoryList ()
    {
        return GetSelectList (TenantStoreHelper.GetSubCategoryList (),"");
    }


    public static IEnumerable<SelectListItem> GetSubCategories
    (int categoryId)
    {
        return GetSelectList
        (TenantStoreHelper.GetSubCategoryListByID (categoryId),"");
    }


    public static IEnumerable<SelectListItem> GetShowHideList ()
    {
        var listShowHideList = ListEnum.GetShowHideList();

        List<SelectListItem> objListItems = new();

        listShowHideList.ForEach (a =>
        {
            SelectListItem objItem = new ()
            {
                Text = a.Text.Trim(),
                Value = a.ValueID.ToString().Trim()
            };

            objListItems.Add (objItem);
        });

        return objListItems.AsEnumerable ();
    }

    private static IEnumerable<SelectListItem> GetSelectList (List<TenantVariableModel> listTenantVariableModel,string selectText)
    {
        List<SelectListItem> objList =
            new()
            {
                new SelectListItem() {
                    Text = "",
                    Value = "",
                    Selected = true
                } };

        listTenantVariableModel.ToList ().ForEach (a =>
        {
            SelectListItem objItem = new ()
            {
                Text = a.Text.Trim(),
                Value = a.ValueID.ToString().Trim()
            };

            objList.Add (objItem);
        });

        return objList.AsEnumerable ();
    }

    public static IEnumerable<SelectListItem> GetSelectList (List<TenantVariableModel> listTenantVariableModel)
    {
        List<SelectListItem> objList =
                            new ()
                            {
                                new SelectListItem() { Text = "", Value = "" }
                            };

        listTenantVariableModel.ForEach (a =>
        {
            SelectListItem objItem = new ()
            {
                Text = a.Text.Trim ( ),
                Value = a.ValueID.ToString ( ).Trim ( )
            };

            objList.Add (objItem);

        });

        return objList.AsEnumerable ();
    }


    public static IEnumerable<SelectListItem> GetCategoryList ()
    {
        return GetSelectList (TenantStoreHelper.GetCategoryList ());
    }

    public static string GetCategoryText (string categoryId)
    {
        return TenantStoreHelper.GetTextForCategoryId (categoryId);
    }

    public static string GetSubCategoryText (string subCategoryId)
    {
        return TenantStoreHelper.GetTextForSubCategoryId (subCategoryId);
    }
}