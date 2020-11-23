using Common.MicroTests;
using System;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class InverseBoolConverterTests
    {
        [Gwt("Given an inverse bool converter",
            "when attempting to convert a null value",
            "a null reference exception is thrown")]
        public void T0()
        {
            var converter = new InverseBoolConverter();
            Assert.Throws<NullReferenceException>(() => converter.Convert(null!, typeof(bool), null!, InvariantCulture));
        }

        [Gwt("Given an inverse bool converter",
            "when attempting to convert to a non-bool target type",
            "an exception is thrown")]
        public void T1()
        {
            var converter = new InverseBoolConverter();
            Assert.Throws<InvalidOperationException>(() => converter.Convert(true, typeof(object), null!, InvariantCulture));
        }

        [Gwt("Given a true bool",
            "when converted",
            "false is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new InverseBoolConverter();

            // Act 
            var actual = testConverter.Convert(true, typeof(bool), null!, InvariantCulture);

            // Assert
            Assert.False((bool)actual);
        }

        [Gwt("Given a false bool",
            "when converted",
            "true is returned")]
        public void T3()
        {
            // Arrange
            var testConverter = new InverseBoolConverter();

            // Act 
            var actual = testConverter.Convert(false, typeof(bool), null!, InvariantCulture);

            // Assert
            Assert.True((bool)actual);
        }
    }
}
