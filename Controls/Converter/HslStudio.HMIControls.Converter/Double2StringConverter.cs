using Avalonia.Data.Converters;
using System.Globalization;

namespace HslStudio.HMIControls.Converter
{

    public class Double2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "-";
            }

            double dVal = (double)value;
            return dVal == double.NaN ? "-" : string.Format("{0:F1}", dVal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
