using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using Common.Optional;
using TradeJournalCore;

namespace TradeJournalWPF.Converters
{
    public sealed class OptionalExecutionDateTimeToDateTimeConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return string.Empty;
            }

            object y = string.Empty;

            var d = (Optional<Execution>)value;

            d.IfExistsThen(x => { y = x.DateTime; });

            return y;
        }
    }
}
