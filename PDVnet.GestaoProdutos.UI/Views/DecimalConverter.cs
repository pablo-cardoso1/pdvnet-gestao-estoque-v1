using System;
using System.Globalization;
using System.Windows.Data;

namespace PDVnet.GestaoProdutos.UI.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            if (value is decimal dec)
            {
                // Use the provided culture to format decimal correctly
                return dec.ToString("N2", culture);
            }

            // Try to handle other numeric types
            if (decimal.TryParse(value.ToString(), NumberStyles.Any, culture, out var parsed))
                return parsed.ToString("N2", culture);

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value?.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return 0m; // treat empty as zero

            text = text.Trim();

            // Try parse using the provided culture first
            if (decimal.TryParse(text, NumberStyles.Number, culture, out decimal result))
                return result;

            // Normalize alternate decimal separator (dot vs comma) to current culture and try again
            var decimalSep = culture.NumberFormat.NumberDecimalSeparator;
            var altSep = decimalSep == "," ? "." : ",";

            if (text.Contains(altSep) && !text.Contains(decimalSep))
            {
                var normalized = text.Replace(altSep, decimalSep);
                if (decimal.TryParse(normalized, NumberStyles.Number, culture, out result))
                    return result;
            }

            // Fallback: try invariant culture
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                return result;

            // If parsing fails (likely partial input while typing), don't throw — prevent updating the source
            // Returning Binding.DoNothing tells the binding engine to ignore this value and keep the current source value.
            return Binding.DoNothing;
        }
    }
}