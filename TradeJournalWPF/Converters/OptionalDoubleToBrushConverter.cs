﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using TradeJournalCore.Optional;

namespace TradeJournalWPF.Converters
{
    public sealed class OptionalDoubleToBrushConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var input = (Optional<double>)value;

            var colour = Brushes.OrangeRed;

            input.IfExistsThen(x =>
            {
                if (x > 0)
                {
                    colour = Brushes.Green;

                }
            });

            return colour;
        }

        public new object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
