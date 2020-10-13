using System;
using TradeJournalCore.MicroTests;
using TradeJournalCore.Optional;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.ConverterTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class OptionalDateTimeToDateTimeConverterTests
    {
        [Gwt("Given a null value",
            "when converted",
            "the empty string is returned")]
        public void T0()
        {
            // Arrange
            var testConverter = new OptionalDateTimeToDateTimeConverter();

            // Act 
            var actual = testConverter.Convert(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option none date time",
            "when converted",
            "the empty string is returned")]
        public void T1()
        {
            // Arrange
            var testConverter = new OptionalDateTimeToDateTimeConverter();
            var optional = Option.None<DateTime>();

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option some date time",
            "when converted",
            "the same date time is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new OptionalDateTimeToDateTimeConverter();
            var testDate = new DateTime(2020, 9, 11, 21, 6, 10);
            var optional = Option.Some(testDate);

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(testDate, actual);
        }

        [Gwt("Given a null value",
            "when converted back",
            "an option none date time is returned")]
        public void T3()
        {
            // Arrange
            var testConverter = new OptionalDateTimeToDateTimeConverter();

            // Act 
            var actual = testConverter.ConvertBack(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.IsType<Option.OptionNone<DateTime>>(actual);
        }

        [Gwt("Given a null target type",
            "when converted back",
            "null is returned")]
        public void T4()
        {
            // Arrange
            var testConverter = new OptionalDateTimeToDateTimeConverter();

            // Act 
            var actual = testConverter.ConvertBack(DateTime.MaxValue, null, null!, InvariantCulture);

            // Assert
            Assert.Equal(null!, actual);
        }
    }
}
