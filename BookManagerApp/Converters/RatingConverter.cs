using System.Globalization;
using System.Windows.Data;

namespace BookManagerApp.Converters
{
    public class RatingConverter : IValueConverter
    {
        // Converts between int and string representations when user interacts with the rating cells in the DataGrid.
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (int.TryParse(value.ToString(), out int rating))
            {
                return Math.Clamp(rating, 0, 5);
            }
            return null;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? str = value as string ?? null;

            if (string.IsNullOrWhiteSpace(str))
                return null;

            if (int.TryParse(str, out int rating))
                return rating;

            return null;
        }
    }
}
