using Main.Common.Models;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;
namespace Main.Common;

public class TenantStores
{
    public static List<TenantVariableModel> ListTenantStoreMenu
        (IStringLocalizer<SharedResource> localizer,StoreType storeType)
    {
        List<TenantVariableModel> listTenantStoreMenu  = [];


        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.ARTS,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["ARTS"] ?? "ARTS",
            TenantStore = StoreType.FineArts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Painting,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Painting"] ?? "Painting",
            TenantStore = StoreType.FineArts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Drawing,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Drawing"] ?? "Drawing",
            TenantStore = StoreType.FineArts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Sculpture,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Sculpture"] ?? "Sculpture",
            TenantStore = StoreType.FineArts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Photography,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Photography"] ?? "Photography",
            TenantStore = StoreType.FineArts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.WaterColor,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["WaterColor"] ?? "WaterColor",
            TenantStore = StoreType.FineArts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.CRAFTS,
            ParentID = ( int ) EnumStoreMenu.CRAFTS,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["CRAFTS"] ?? "CRAFTS",
            TenantStore = StoreType.FineCrafts,
            TenantId = ""
        });


        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Handicraft,
            ParentID = ( int ) EnumStoreMenu.CRAFTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Handicraft"] ?? "Handicraft",
            TenantStore = StoreType.FineCrafts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Jute,
            ParentID = ( int ) EnumStoreMenu.CRAFTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Jute"] ?? "Jute",
            TenantStore = StoreType.FineCrafts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Metal,
            ParentID = ( int ) EnumStoreMenu.CRAFTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Metal"] ?? "Metal",
            TenantStore = StoreType.FineCrafts,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.COLLECTIBLES,
            ParentID = ( int ) EnumStoreMenu.COLLECTIBLES,
            Variable = EnumTenantVariable.ProductCategory,
            Text = localizer["COLLECTIBLES"] ?? "COLLECTIBLES",
            TenantStore = StoreType.FineCollections,
            TenantId = ""
        });


        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.CoinAndCurrency,
            ParentID = ( int ) EnumStoreMenu.COLLECTIBLES,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Handicraft"] ?? "Handicraft",
            TenantStore = StoreType.FineCollections,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Stamps,
            ParentID = ( int ) EnumStoreMenu.COLLECTIBLES,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Stamps"] ?? "Stamps",
            TenantStore = StoreType.FineCollections,
            TenantId = ""
        });

        listTenantStoreMenu.Add (new TenantVariableModel ()
        {
            ValueID = ( int ) EnumStoreMenu.Stationery,
            ParentID = ( int ) EnumStoreMenu.COLLECTIBLES,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = localizer["Stationery"] ?? "Stationery",
            TenantStore = StoreType.FineCollections,
            TenantId = ""
        });

        return listTenantStoreMenu.Where (a => a.TenantStore == storeType).ToList ();
    }
}
