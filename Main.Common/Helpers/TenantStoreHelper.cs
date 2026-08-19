using Main.Common.Models;

namespace Main.Common;

public class TenantStoreHelper
{
    public TenantStoreHelper ()
    {
    }

    public static List<TenantVariableModel> GetCategoryList ()
    {
        List<TenantVariableModel> listCategory  = new List<TenantVariableModel> ();

        listCategory = TenantStores.ListTenantStoreMenu ().Where<TenantVariableModel>
                                    (m => m.Variable == EnumTenantVariable.ProductCategory).ToList ();

        return listCategory.ToList ();
    }

    public static List<TenantVariableModel> GetSubCategoryList ()
    {
        List<TenantVariableModel> listCategory  = new List<TenantVariableModel> ();

        List<TenantVariableModel> listSubCategory = TenantStores.ListTenantStoreMenu ().Where<TenantVariableModel>
                                                    (m =>  m.Variable == EnumTenantVariable.ProductSubCategory).ToList ();

        return listSubCategory.ToList ();
    }


    public static List<TenantVariableModel> GetSubCategoryListByID (int categoryId)
    {
        List<TenantVariableModel>  listSubCategory = new  List<TenantVariableModel> ();
        listSubCategory =
            TenantStores.ListTenantStoreMenu ().Where<TenantVariableModel>
            (m =>
                m.Variable == EnumTenantVariable.ProductSubCategory &&
                m.ParentID == categoryId).ToList ();

        return listSubCategory.ToList ();
    }

    public static string GetTextForCategoryId (string categoryId)
    {
        List<TenantVariableModel> listCategory = GetCategoryList (  );

        TenantVariableModel? tenantVariableModel =
                listCategory.FirstOrDefault<TenantVariableModel>
                ( m =>  m.ValueID == int.Parse (categoryId));

        return tenantVariableModel!.Text;
    }

    public static string GetTextForSubCategoryId (string subCategoryId)
    {
        List<TenantVariableModel>  listSubCategory = new  List<TenantVariableModel> ();
        listSubCategory = GetSubCategoryList ();

        TenantVariableModel? tenantVariableModel =
                listSubCategory.FirstOrDefault<TenantVariableModel>
                ( m =>  m.ValueID == int.Parse(subCategoryId));

        return tenantVariableModel!.Text;
    }
}
