using Main.Common.Models;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.Common;

public class TenantStoreHelper
{

    public TenantStoreHelper ()
    {
    }

    public static List<TenantVariableModel> GetCategoryList (IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        return TenantStores.ListTenantStoreMenu (localizer,storeType).Where (x => x.Variable == EnumTenantVariable.ProductCategory).ToList ();
    }

    public static List<TenantVariableModel> GetSubCategoryList (IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        return TenantStores.ListTenantStoreMenu (localizer,storeType).Where (x => x.Variable == EnumTenantVariable.ProductSubCategory).ToList ();
    }

    public static string GetTextForCategoryId
    (int? categoryId,IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        List<TenantVariableModel>? listCategory =  TenantStores.ListTenantStoreMenu
        (localizer, storeType);

        try
        {
            TenantVariableModel? tenantVariableModel =
                listCategory?.FirstOrDefault<TenantVariableModel>
                ( m =>  m.ValueID == categoryId);

            return tenantVariableModel?.Text ?? string.Empty;

        }
        catch
        {
            return string.Empty;
        }
    }

    public static List<TenantVariableModel> GetFullCategoryList (IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        return TenantStores.ListTenantStoreMenu (localizer,storeType).ToList ();
    }

    public static List<TenantVariableModel> GetFullSubCategoryList (IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        return TenantStores.ListTenantStoreMenu (localizer,storeType).ToList ();
    }


    //public static List<TenantVariableModel>? GetSubCategoryListByID (int categoryId)
    //{
    //    List<TenantVariableModel>?  listSubCategory = new  List<TenantVariableModel>? ();
    //    listSubCategory =
    //        TenantStores.ListTenantStoreMenu ().Where<TenantVariableModel>
    //        (m =>
    //            m.Variable == EnumTenantVariable.ProductSubCategory &&
    //            m.ParentID == categoryId).ToList ();

    //    return listSubCategory?.ToList () ?? null;
    //}



    //public static string? GetTextForSubCategoryId (string? subCategoryId)
    //{
    //    List<TenantVariableModel>?  listSubCategory = new List<TenantVariableModel>? ();
    //    listSubCategory = GetSubCategoryList ();

    //    TenantVariableModel? tenantVariableModel =
    //            listSubCategory.FirstOrDefault<TenantVariableModel>
    //            ( m =>  m.ValueID == int.Parse(subCategoryId!) ?? -1);

    //    return tenantVariableModel?.Text! ?? null;
    //}
}
