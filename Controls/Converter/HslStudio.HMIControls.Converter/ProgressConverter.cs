using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;

namespace HslStudio.HMIControls.Converter
{
    public class ProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                double progress = (double)value;
                return 100d - progress;
            }
            else
            {
                double progress = (double)value;
                return 100d - progress + System.Convert.ToDouble(parameter);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double progress = (double)value;
            return 100d - progress;
        }
    }

    public class AngleBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                bool check = (bool)value;
                return check ? 35d : -35d;
            }
            else
            {
                bool check = (bool)value;
                return check ? System.Convert.ToDouble(parameter) : -System.Convert.ToDouble(parameter);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ColorLightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            int degree = 40;
            if (parameter != null)
            {
                degree = System.Convert.ToInt32(parameter);
            }
            return Color.FromRgb((byte)(color.R + (255 - color.R) * degree / 100), (byte)(color.G + (255 - color.G) * degree / 100), (byte)(color.B + (255 - color.B) * degree / 100));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class MultiplesValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return value;
            }
            else
            {
                return System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class AdditionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return value;
            }
            else
            {
                return System.Convert.ToDouble(value) + System.Convert.ToDouble(parameter);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class HslVacuumPumpValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) * -1 - 3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                if (System.Convert.ToBoolean(value))
                {
                    return ScrollBarVisibility.Visible;
                }
                else
                {
                    return ScrollBarVisibility.Hidden;
                }
            }
            else
            {
                if (System.Convert.ToBoolean(value))
                {
                    return ScrollBarVisibility.Hidden;
                }
                else
                {
                    return ScrollBarVisibility.Visible;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}
