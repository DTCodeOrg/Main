using Main.Common;
using Main.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.WebAppCore.Helpers;

public class DropDownListItems
{
    public DropDownListItems ()
    {
    }

    public static IEnumerable<SelectListItem> GetPostTypeList ()
    {
        var listCountries = ListEnum.GetPostTypeList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objOfferTypeListItems = new();

        foreach ( var item in listCountries.ToList () )
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

        foreach ( var item in listCountries.ToList () )
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
            SelectListItem objItem = new()
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

        foreach ( TenantVariableModel? item in listCountries.ToList () )
        {
            SelectListItem objItem = new()
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };

            objCountryListItems.Add (objItem);
        }

        return objCountryListItems.AsEnumerable ();
    }

    public static IEnumerable<SelectListItem> GetSubCategoryList (IStringLocalizer<SharedResource> localizer)
    {
        var listCategory = GetSelectList (TenantStoreHelper.GetSubCategoryList (localizer), "");
        return listCategory;
    }

    public static IEnumerable<SelectListItem> GetCategoryList (IStringLocalizer<SharedResource> localizer)
    {
        var listCategory = GetSelectList (TenantStoreHelper.GetCategoryList (localizer),"");
        return listCategory;
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

        return objListItems.ToList ();
    }

    private static IEnumerable<SelectListItem>
        GetSelectList (List<TenantVariableModel> listTenantVariableModel,string selectText)
    {
        List<SelectListItem> objList = [];

        SelectListItem objItem;

        listTenantVariableModel.ToList ().ForEach (a =>
        {
            objItem = new SelectListItem ()
            {
                Text = a.Text.Trim (),
                Value = a.ValueID.ToString ().Trim ()
            };

            objList.Add (objItem);
        });

        objItem = new SelectListItem ()
        {
            Text = "",
            Value = "",
            Selected = true
        };

        objList.Add (objItem);

        return objList.AsEnumerable<SelectListItem> ();
    }

    public static string GetTextForCategoryId (string categoryId,IStringLocalizer<SharedResource> localizer)
    {
        var resultText = TenantStoreHelper.GetTextForCategoryId (categoryId, localizer);
        return resultText;
    }
}