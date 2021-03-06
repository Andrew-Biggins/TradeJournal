﻿using System;
using System.Globalization;
using System.Windows.Data;
using Common.Optional;

namespace TradeJournalWPF.Converters
{
    // todo possible deletion candidate
    public sealed class OptionalDateTimeToDateTimeConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return string.Empty;
            }

            object y = string.Empty;

            var d = (Optional<DateTime>) value;

            d.IfExistsThen(x => { y = x; });

            return y;
        }

        public new object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return targetType is null ? null : Option.None<DateTime>();
        }
    }
}
