using System.Globalization;

namespace InvoiceGen.Services
{
    public static class CurrencyFormatter
    {
        public static string Format(decimal amount, string currency)
        {
            CultureInfo culture = currency switch
            {
                "EUR" => new CultureInfo("de-DE"),
                "GBP" => new CultureInfo("en-GB"),
                "JPY" => new CultureInfo("ja-JP"),
                _ => new CultureInfo("en-US")
            };

            return amount.ToString("C", culture);
        }
    }
}