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
            ValueID = ( int ) EnumStoreMenu.Painting,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Painting"] ?? "Painting",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Drawing,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Drawing"] ?? "Drawing",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Sculpture,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Sculpture"] ?? "Sculpture",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Photography,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Photography"] ?? "Photography",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.WaterColor,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["WaterColor"] ?? "WaterColor",
            TenantStore = StoreType.Default,
            TenantId = ""
        });

        return listTenantStoreMenu.ToList ();
    }
}
