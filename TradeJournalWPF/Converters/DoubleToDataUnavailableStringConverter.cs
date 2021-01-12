using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TradeJournalWPF.Converters
{
    public class DoubleToDataUnavailableStringConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = (double) value;

            if (double.IsNaN(x) || double.IsNegativeInfinity(x) || double.IsPositiveInfinity(x))
            {
                return "---";
            }

            return Math.Round(x, 2);
        }

        public new object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}