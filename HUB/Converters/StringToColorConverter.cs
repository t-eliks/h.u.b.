namespace HUB.Converters
{
    using System;
    using System.Windows.Media;
    using System.Windows.Data;

    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return (Color)ColorConverter.ConvertFromString((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            Color color = (Color)ColorConverter.ConvertFromString((string)value);

            return color.ToString();
        }
    }
}
