using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PDVnet.GestaoProdutos.UI.Converters
{
    public class EstoqueBaixoColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count > 0 ? Brushes.Red : Brushes.Black;
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}