using Main.Common.Models;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.Common;

public class TenantStoreHelper
{

    public TenantStoreHelper ()
    {
    }

    public static List<TenantVariableModel> GetCategoryList (IStringLocalizer<SharedResource> localizer)
    {
        return TenantStores.ListTenantStoreMenu (localizer);
    }

    public static List<TenantVariableModel> GetSubCategoryList (IStringLocalizer<SharedResource> localizer)
    {
        return TenantStores.ListTenantStoreMenu (localizer);
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

    //public static string? GetTextForCategoryId (string categoryId)
    //{
    //    List<TenantVariableModel>? listCategory = GetCategoryList (  );

    //    TenantVariableModel? tenantVariableModel =
    //            listCategory?.FirstOrDefault<TenantVariableModel>
    //            ( m =>  m.ValueID == int.Parse (categoryId)) ?? null;

    //    return tenantVariableModel!.Text ?? null;
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
