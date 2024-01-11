using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NavitasBeta
{
    public class FloatConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0.0";

            float floatValue = (float)value;
            return string.Format("{0}", floatValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                return 0;

            float resultFloat;
            if (float.TryParse(strValue, out resultFloat))
            {
                return resultFloat;
            }

            return 0;
        }
    }
}
