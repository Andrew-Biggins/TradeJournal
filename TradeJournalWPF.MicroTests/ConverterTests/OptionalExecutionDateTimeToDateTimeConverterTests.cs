using System;
using Common.MicroTests;
using Common.Optional;
using TradeJournalCore;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class OptionalExecutionDateTimeToDateTimeConverterTests
    {
        [Gwt("Given a null value",
            "when converted",
            "the empty string is returned")]
        public void T0()
        {
            // Arrange
            var testConverter = new OptionalExecutionDateTimeToDateTimeConverter();

            // Act 
            var actual = testConverter.Convert(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option none execution",
            "when converted",
            "the empty string is returned")]
        public void T1()
        {
            // Arrange
            var testConverter = new OptionalExecutionDateTimeToDateTimeConverter();
            var optional = Option.None<Execution>();

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option some execution",
            "when converted",
            "the execution date time is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new OptionalExecutionDateTimeToDateTimeConverter();
            var testDateTime = new DateTime(2019, 6, 6, 12, 16, 30);
            var execution = new Execution(0, testDateTime, 100);
            var optional = Option.Some(execution);

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(testDateTime, actual);
        }
    }
}
