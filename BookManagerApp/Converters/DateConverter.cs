using System.Globalization;
using System.Windows.Data;

namespace BookManagerApp.Converters
{
    public class DateConverter : IValueConverter
    {
        // Converts between DateTime and string representations when user interacts with the date cells in the DataGrid.
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
