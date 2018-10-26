using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LeakysBlueprinter.Converters
{
    /// <summary>
    /// Converts two values into a combination of the two values, separated by " - ".
    /// If second value is empty, returns only the first.
    /// Can tolerate if second value can't be cast to string.
    /// </summary>
    public class WindowTitleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string TitleBase = (string)values[0];
            string TitleExtension = values[1] as string;

            TitleExtension = string.IsNullOrEmpty(TitleExtension) ? String.Empty : " - " + TitleExtension;

            return TitleBase + TitleExtension;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}
