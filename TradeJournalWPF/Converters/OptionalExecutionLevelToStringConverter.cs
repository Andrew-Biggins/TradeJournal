using Common.Optional;
using System;
using System.Globalization;
using System.Windows.Data;
using TradeJournalCore;

namespace TradeJournalWPF.Converters
{
    public sealed class OptionalExecutionLevelToStringConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return string.Empty;
            }

            object y = string.Empty;

            var d = (Optional<Execution>)value;

            d.IfExistsThen(x => { y = x.Level;});

            return y;
        }
    }
}
