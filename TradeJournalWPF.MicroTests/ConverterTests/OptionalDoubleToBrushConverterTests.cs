using Common.MicroTests;
using Common.Optional;
using System;
using System.Windows.Media;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class OptionalDoubleToBrushConverterTests
    {
        [Gwt("Given a null value",
            "when converted",
            "the orange red brush is returned")]
        public void T0()
        {
            // Arrange
            var testConverter = new OptionalDoubleToBrushConverter();

            // Act 
            var actual = testConverter.Convert(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(Brushes.OrangeRed, actual);
        }

        [Gwt("Given a positive value",
            "when converted",
            "the green brush is returned")]
        public void T1()
        {
            // Arrange
            var testConverter = new OptionalDoubleToBrushConverter();
            var testValue = Option.Some(55.00);

            // Act 
            var actual = testConverter.Convert(testValue, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(Brushes.Green, actual);
        }

        [Gwt("Given a negative value",
            "when converted",
            "the orange red brush is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new OptionalDoubleToBrushConverter();
            var testValue = Option.Some(-20.00);

            // Act 
            var actual = testConverter.Convert(testValue, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(Brushes.OrangeRed, actual);
        }

        [Gwt("Given a converter",
            "when convert back is called",
            "a not supported exception is thrown")]
        public void T3()
        {
            var testConverter = new OptionalDoubleToBrushConverter();
            Assert.Throws<NotSupportedException>(() => testConverter.ConvertBack(null!, typeof(object), null!, InvariantCulture));
        }
    }
}
