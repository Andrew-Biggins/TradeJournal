using Common.MicroTests;
using System;
using TradeJournalWPF.Converters;
using Xunit;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class MarkupConverterTests
    {
        [Gwt("Given a markup converter",
            "when the value is provided",
            "the value is the markup converter itself")]
        public void T0()
        {
            // Arrange
            var converter = new Derived();

            // Act
            var value = converter.ProvideValue(null!);

            // Assert
            Assert.Same(converter, value);
        }

        [Gwt("Given a markup converter",
            "when attempting to convert back a single value",
            "a not supported exception is thrown")]
        public void T1()
        {
            var converter = new Derived();
            Assert.Throws<NotSupportedException>(() => converter.ConvertBack(null!, typeof(int), null!, null!));
        }

        [Gwt("Given a markup converter",
            "when attempting to convert back multiple values",
            "a not supported exception is thrown")]
        public void T2()
        {
            var converter = new Derived();
            Assert.Throws<NotSupportedException>(() => converter.ConvertBack(null!, new Type[0], null!, null!));
        }

        private sealed class Derived : MarkupConverter
        {
        }
    }
}
