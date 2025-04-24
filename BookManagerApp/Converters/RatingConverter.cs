using BookManagerApp.DataAccessLayer;
using BookManagerApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookManagerApp.Converters
{
    public class RatingConverter : IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;

            if (string.IsNullOrWhiteSpace(str))
                return null;

            if (int.TryParse(str, out int rating))
                return rating;

            return null;
        }
    }
}
