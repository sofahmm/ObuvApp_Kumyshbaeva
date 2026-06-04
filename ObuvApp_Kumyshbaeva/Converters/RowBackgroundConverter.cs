using ObuvApp_Kumyshbaeva.DbConnection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ObuvApp_Kumyshbaeva.Converters
{
    internal class RowBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var product = value as Product;

            if (product.WorkshopCount == 0)
                return Brushes.Blue;

            if (product.ActiveDiscount > 15)
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
