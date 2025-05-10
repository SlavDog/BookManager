using System.Globalization;
using System.Windows.Data;

namespace BookManagerApp.Converters
{
    public class DateConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) {
                return null;
            }
            if (value.ToString() == "") {
                return null;
            }
            return value;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result)) {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
