using System;
using System.Globalization;
using System.Windows.Data;
using TradeJournalCore;
using static TradeJournalCore.AssetClass;

namespace TradeJournalWPF.Converters
{
    public class AssetClassToStringConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((AssetClass)value)
            {
                case Currencies:
                    return "Currencies";
                case Commodities:
                    return "Commodities";
                case Crypto:
                    return "Crypto";
                case Indices:
                    return "Indices";
                case Shares:
                    return "Shares";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}