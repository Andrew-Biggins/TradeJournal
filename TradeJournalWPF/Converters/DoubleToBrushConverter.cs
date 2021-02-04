using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TradeJournalWPF.Converters
{
    public class DoubleToBrushConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value >=0 ? Brushes.LawnGreen : Brushes.OrangeRed;
        }

        public new object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}