using System;
using System.Globalization;
using Xamarin.Forms;

namespace NavitasBeta
{
    public class BoolInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? b = value as bool?;
            if (b != null)
                return !b.Value;
            double? d = value as double?;
            if (d != null)
                return (d.Value == 0);

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
