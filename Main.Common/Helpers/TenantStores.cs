using Main.Common.Models;
using ResourceLibrary.Resources;
namespace Main.Common;

public class TenantStores
{
    public static List<TenantVariableModel> ListTenantStoreMenu ()
    {
        List<TenantVariableModel> listTenantStoreMenu
        = [];


        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Beauty,
            ParentID = ( int ) EnumStoreMenu.Beauty,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Beauty"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Health,
            ParentID = ( int ) EnumStoreMenu.Health,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Health"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Fashion,
            ParentID = ( int ) EnumStoreMenu.Fashion,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Fashion"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Fitness,
            ParentID = ( int ) EnumStoreMenu.Fitness,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["FitnessAndLifeStyles"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.ARTS,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["ARTS"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.CRAFTS,
            ParentID = ( int ) EnumStoreMenu.CRAFTS,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["CRAFTS"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.COLLECTIBLES,
            ParentID = ( int ) EnumStoreMenu.COLLECTIBLES,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["COLLECTIBLES"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Makeup,
            ParentID = ( int ) EnumStoreMenu.Makeup,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Makeup"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.SkinCare,
            ParentID = ( int ) EnumStoreMenu.SkinCare,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["SkinCare"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.BeautyTools,
            ParentID = ( int ) EnumStoreMenu.BeautyTools,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["BeautyTools"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Wellbeing,
            ParentID = ( int ) EnumStoreMenu.Wellbeing,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Wellbeings"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.PharmacyProduct,
            ParentID = ( int ) EnumStoreMenu.PharmacyProduct,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Pharmacy"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.MedicalSupplies,
            ParentID = ( int ) EnumStoreMenu.MedicalSupplies,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["MedicalSupplies"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Painting,
            ParentID = ( int ) EnumStoreMenu.Painting,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Painting"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Drawing,
            ParentID = ( int ) EnumStoreMenu.Drawing,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Drawing"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Sculpture,
            ParentID = ( int ) EnumStoreMenu.Sculpture,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Sculpture"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Photography,
            ParentID = ( int ) EnumStoreMenu.Photography,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Photography"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.WaterColor,
            ParentID = ( int ) EnumStoreMenu.WaterColor,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["WaterColor"],
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        return listTenantStoreMenu.ToList ();
    }
}
