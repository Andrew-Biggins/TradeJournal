using Common.MicroTests;
using System;
using System.Windows.Media;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class DoubleToBrushConverterTests
    {
        [Gwt("Given a positive value",
            "when converted",
            "the green brush is returned")]
        public void T1()
        {
            // Arrange
            var testConverter = new DoubleToBrushConverter();

            // Act 
            var actual = testConverter.Convert(55.00, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(Brushes.LawnGreen, actual);
        }

        [Gwt("Given a negative value",
            "when converted",
            "the orange red brush is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new DoubleToBrushConverter();

            // Act 
            var actual = testConverter.Convert(-20.00, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(Brushes.OrangeRed, actual);
        }

        [Gwt("Given a converter",
            "when convert back is called",
            "a not supported exception is thrown")]
        public void T3()
        {
            var testConverter = new DoubleToBrushConverter();
            Assert.Throws<NotSupportedException>(() => testConverter.ConvertBack(null!, typeof(object), null!, InvariantCulture));
        }
    }
}
