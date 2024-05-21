using System.Globalization;

namespace Travely.Client.Utilities
{
    public class ExpandedToHeightConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isExpanded && isExpanded)
            {
                return double.NaN; 
            }
            return 100;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
