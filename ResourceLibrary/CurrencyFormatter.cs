using System.Globalization;

namespace ResourceLibrary.Utilities;

public static class CurrencyFormatter
{
    private static readonly CultureInfo BdEnglishCulture = new("en-BD");

    public static string ToLocalizedCurrency (this decimal? value,string fallback = "0.00")
    {
        CultureInfo currentCulture = CultureInfo.CurrentCulture;

        if ( currentCulture.Name.Equals ("en-US",StringComparison.OrdinalIgnoreCase) )
        {
            currentCulture = BdEnglishCulture;
        }


        if ( !value.HasValue )
        {
            string symbol = currentCulture.NumberFormat.CurrencySymbol;

            return $"{symbol} {fallback}".Trim ();
        }

        return value.Value.ToString ("C",currentCulture);
    }
}



