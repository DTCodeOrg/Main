using Main.Common.Models;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;
namespace Main.Common;

public class TenantStores
{
    public static List<TenantVariableModel> ListTenantStoreMenu (IStringLocalizer<SharedResource> localizer)
    {
        List<TenantVariableModel> listTenantStoreMenu
        = [];

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Beauty,
            ParentID = ( int ) EnumStoreMenu.Beauty,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Beauty"] ?? "Beauty",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Health,
            ParentID = ( int ) EnumStoreMenu.Health,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Health"] ?? "Health",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Fashion,
            ParentID = ( int ) EnumStoreMenu.Fashion,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Fashion"] ?? "Fashion",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Fitness,
            ParentID = ( int ) EnumStoreMenu.Fitness,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["FitnessAndLifeStyles"]
            ?? "FitnessAndLifeStyles",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.ARTS,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["ARTS"] ?? "ARTS",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.CRAFTS,
            ParentID = ( int ) EnumStoreMenu.CRAFTS,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["CRAFTS"] ?? "CRAFTS",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.COLLECTIBLES,
            ParentID = ( int ) EnumStoreMenu.COLLECTIBLES,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["COLLECTIBLES"] ?? "COLLECTIBLES",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Makeup,
            ParentID = ( int ) EnumStoreMenu.Makeup,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Makeup"] ?? "Makeup",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.SkinCare,
            ParentID = ( int ) EnumStoreMenu.SkinCare,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["SkinCare"] ?? "SkinCare",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.BeautyTools,
            ParentID = ( int ) EnumStoreMenu.BeautyTools,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["BeautyTools"] ?? "BeautyTools",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Wellbeing,
            ParentID = ( int ) EnumStoreMenu.Wellbeing,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Wellbeings"] ?? "Wellbeings",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.PharmacyProduct,
            ParentID = ( int ) EnumStoreMenu.PharmacyProduct,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Pharmacy"] ?? "Pharmacy",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.MedicalSupplies,
            ParentID = ( int ) EnumStoreMenu.MedicalSupplies,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["MedicalSupplies"] ?? "MedicalSupplies",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Painting,
            ParentID = ( int ) EnumStoreMenu.Painting,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Painting"] ?? "Painting",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Drawing,
            ParentID = ( int ) EnumStoreMenu.Drawing,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Drawing"] ?? "Drawing",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Sculpture,
            ParentID = ( int ) EnumStoreMenu.Sculpture,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Sculpture"] ?? "Sculpture",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Photography,
            ParentID = ( int ) EnumStoreMenu.Photography,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["Photography"] ?? "Photography",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.WaterColor,
            ParentID = ( int ) EnumStoreMenu.WaterColor,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["WaterColor"] ?? "WaterColor",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        return listTenantStoreMenu.ToList ();
    }
}
