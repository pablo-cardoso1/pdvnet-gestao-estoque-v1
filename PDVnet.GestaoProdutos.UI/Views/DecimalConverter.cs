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

            // Try parse using the provided culture first
            if (decimal.TryParse(text, NumberStyles.Number, culture, out decimal result))
                return result;

            // Fallback: try invariant culture
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                return result;

            // If parsing fails, throw to allow WPF's ValidatesOnExceptions to handle it
            throw new FormatException("Formato de número inválido.");
        }
    }
}