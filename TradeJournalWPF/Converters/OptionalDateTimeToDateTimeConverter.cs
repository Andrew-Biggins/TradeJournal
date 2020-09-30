﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using TradeJournalCore.Optional;

namespace TradeJournalWPF.Converters
{
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
            if (targetType is null)
            {
                return null;
            }

            if (double.TryParse((string) value, out double d))
            {
                return Option.Some(d);
            }

            return Option.None<DateTime>();
        }
    }
}
