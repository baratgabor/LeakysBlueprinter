using System;
using System.Windows.Data;
using FontAwesome.WPF;

namespace LeakysBlueprinter.UI.WPF.Converters
{
    /// <summary>
    /// Convert boolean value to a FontAwesome-based icon.
    /// </summary>
    public class BoolToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value to convert must be boolean.");

            if ((bool)value == true)
                return FontAwesomeIcon.Check;
            else
                return FontAwesomeIcon.Question;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("Converting from icon to boolean is not supported.");
        }
    }
}
