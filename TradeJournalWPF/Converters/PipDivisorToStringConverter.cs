using System;
using System.Globalization;
using System.Windows.Data;
using TradeJournalCore;

namespace TradeJournalWPF.Converters
{
    public class PipDivisorToStringConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((PipDivisor) value)
            {
                case PipDivisor.One:
                    return "X.0000";
                case PipDivisor.Ten:
                    return "0.X000";
                case PipDivisor.OneHundred:
                    return "0.0X00";
                case PipDivisor.OneThousand:
                    return "0.00X0";
                case PipDivisor.TenThousand:
                    return "0.000X";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}