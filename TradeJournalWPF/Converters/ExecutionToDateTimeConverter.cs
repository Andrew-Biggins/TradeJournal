using System;
using System.Globalization;
using System.Windows.Data;
using TradeJournalCore;
using static TradeJournalCore.DateTimeHelper;

namespace TradeJournalWPF.Converters
{
    public sealed class ExecutionToDateTimeConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var execution = (Execution) value;

            return CombineDateTime(execution.Date, execution.Time);
        }
    }
}