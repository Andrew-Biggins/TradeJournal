using Common.MicroTests;
using Common.Optional;
using System;
using TradeJournalCore;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class OptionalExecutionLevelToStringConverterTests
    {
        [Gwt("Given a null value",
            "when converted",
            "the empty string is returned")]
        public void T0()
        {
            // Arrange
            var testConverter = new OptionalExecutionLevelToStringConverter();

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
            var testConverter = new OptionalExecutionLevelToStringConverter();
            var optional = Option.None<Execution>();

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option some execution",
            "when converted",
            "the execution level is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new OptionalExecutionLevelToStringConverter();
            const double testLevel = 1234.56;
            var execution = new Execution(testLevel, DateTime.Now, 100);
            var optional = Option.Some(execution);

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(testLevel, actual);
        }
    }
}
