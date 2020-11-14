using Common.Optional;
using System;
using System.Globalization;
using System.Windows.Data;
using TradeJournalCore;

namespace TradeJournalWPF.Converters
{
    public sealed class OptionalExcursionPointsToStringConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return string.Empty;
            }

            object y = string.Empty;

            var d = (Optional<Excursion>) value;

            d.IfExistsThen(x => 
                { x.Points.IfExistsThen(z =>
                    {
                        y = z;
                    });
                });

            return y;
        }

        public new object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType is null)
            {
                return null;
            }

            return double.TryParse((string) value, out var d) ? Option.Some(new Excursion(d, 0)) : Option.None<Excursion>();
        }
    }
}
