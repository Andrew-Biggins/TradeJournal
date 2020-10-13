using System;
using System.Globalization;
using System.Windows.Data;

namespace TradeJournalWPF.Converters
{
    public class EmptyDoubleToStringConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public new object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType is null)
            {
                return null;
            }

            return double.TryParse((string)value, out var d) ? d : default(object);
        }
    }
}
