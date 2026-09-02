using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Globalization;
using System.Windows.Data;
using InvoiceGen.Services;

namespace InvoiceGen.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public string Currency { get; set; } = "USD";

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value is decimal amount)
            {
                return CurrencyFormatter.Format(amount, Currency);
            }

            return value;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}