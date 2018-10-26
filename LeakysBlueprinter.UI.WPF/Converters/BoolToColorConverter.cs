using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LeakysBlueprinter.UI.WPF.Converters
{
    /// <summary>
    /// Converts boolean values to green (true) or black (false) color.
    /// </summary>
    public class BoolToColorConverter_GreenOrBlack : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value to convert must be boolean.");

            if ((bool)value == true)
                return System.Windows.Media.Brushes.LimeGreen;
            else
                return System.Windows.Media.Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("Converting from icon to boolean is not supported.");
        }
    }
}
