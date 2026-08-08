namespace Main.Common;

public static class TenantStoreHelper
{
    public static List<TenantVariableModel> GetCategoryList ()
    {
        List<TenantVariableModel>  listCategory = new  List<TenantVariableModel> ();

        listCategory =
            TenantStores.ListTenantStoreMenu ()
            .Where<TenantVariableModel> (m =>
            m.Variable == EnumTenantVariable.ProductCategory &&
            m.ValueID == m.ParentID).ToList ();


        return listCategory.ToList ();
    }

    public static List<TenantVariableModel> GetSubCategoryList ()
    {
        List<TenantVariableModel>  listSubCategory = new  List<TenantVariableModel> ();

        listSubCategory =
            TenantStores.ListTenantStoreMenu ()
            .Where<TenantVariableModel> (m =>
            m.Variable == EnumTenantVariable.ProductSubCategory).ToList ();


        return listSubCategory.ToList ();
    }

    public static List<TenantVariableModel>
    GetSubCategoryListByID (int categoryId)
    {
        List<TenantVariableModel>  listSubCategory = new  List<TenantVariableModel> ();
        listSubCategory =
            TenantStores.ListTenantStoreMenu ().Where<TenantVariableModel>
            (m =>
                m.Variable == EnumTenantVariable.ProductSubCategory &&
                m.ParentID == categoryId).ToList ();

        return listSubCategory.ToList ();
    }

    public static string GetTextForCategoryId
    (int categoryId)
    {
        List<TenantVariableModel> listCategory = GetCategoryList (  );

        TenantVariableModel? tenantVariableModel =
                listCategory.FirstOrDefault<TenantVariableModel>
                ( m =>  m.ValueID == categoryId);

        return tenantVariableModel!.Text;
    }

    public static string GetTextForSubCategoryId (int subCategoryId)
    {
        List<TenantVariableModel>  listSubCategory = new  List<TenantVariableModel> ();
        listSubCategory = GetSubCategoryList ();

        TenantVariableModel? tenantVariableModel =
                listSubCategory.FirstOrDefault<TenantVariableModel>
                ( m =>  m.ValueID == subCategoryId);

        return tenantVariableModel!.Text;
    }
}
