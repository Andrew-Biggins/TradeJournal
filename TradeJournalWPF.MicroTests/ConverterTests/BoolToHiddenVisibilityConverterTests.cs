using Common.MicroTests;
using System;
using System.Windows;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class BoolToHiddenVisibilityConverterTests
    {
        [Gwt("Given a null value",
            "when converted",
            "a null reference exception is thrown")]
        public void T0()
        {
            var testConverter = new BoolToHiddenVisibilityConverter();
            Assert.Throws<NullReferenceException>(() => testConverter.Convert(null!, typeof(object), null!, InvariantCulture));
        }

        [Gwt("Given a wrong value type",
            "when converted",
            "an invalid cast exception is thrown")]
        public void T1()
        {
            var testConverter = new BoolToHiddenVisibilityConverter();
            Assert.Throws<InvalidCastException>(() => testConverter.Convert("String", typeof(object), null!, InvariantCulture));
        }

        [Gwt("Given true",
            "when converted",
            "visible is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new BoolToHiddenVisibilityConverter();

            // Act
            var actual = testConverter.Convert(true, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(Visibility.Visible, actual);
        }

        [Gwt("Given false",
            "when converted",
            "hidden is returned")]
        public void T3()
        {
            // Arrange
            var testConverter = new BoolToHiddenVisibilityConverter();

            // Act
            var actual = testConverter.Convert(false, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(Visibility.Hidden, actual);
        }
    }
}
