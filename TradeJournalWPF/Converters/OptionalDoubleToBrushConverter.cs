using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Common.Optional;

namespace TradeJournalWPF.Converters
{
    public sealed class OptionalDoubleToBrushConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (Optional<double>)value;

            var colour = Brushes.OrangeRed;

            input?.IfExistsThen(x =>
            {
                if (x > 0)
                {
                    colour = Brushes.Green;
                }
            });

            return colour;
        }

        public new object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
