using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using UserManagement.Common.Constants;

namespace UserManagement.UI.Converters
{
    public class AMPMColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToString(value);

            return "AM".Equals(val) ? ColorNames.BrightOrange : ColorNames.NavyBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
