using System;
using TradeJournalCore.Optional;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public class OptionalTests
    {
        [Gwt("Given a null value",
            "when some optional is created",
            "an argument null exception is thrown")]
        public void T0()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some((object?)null));
        }

        [Gwt("Given a non-null value",
            "when some optional string is created",
            "an optional string is returned")]
        public void T1()
        {
            // Arrange   
            var value = string.Empty;

            // Act
            var optional = Option.Some(value);

            // Assert
            Assert.IsType<Option.OptionSome<string>>(optional);
        }

        [Gwt("Given a non-null value",
            "when none optional string is created",
            "an optional string is returned")]
        public void T2()
        {
            // Act
            var optional = Option.None<string>();

            // Assert
            Assert.IsType<Option.OptionNone<string>>(optional);
        }

        [Gwt("Given some optional string",
            "when the optional string is obtained",
            "the optional string is the value used at construction")]
        public void T3()
        {
            // Arrange
            const string testValue = "String";
            var optional = Option.Some(testValue);
            var outString = string.Empty;

            // Act
            optional.IfExistsThen(optionalString => { outString = optionalString; });

            // Assert
            Assert.Equal(testValue, outString);
        }

        [Gwt("Given a some optional string",
            "when if empty is used",
            "the otherwise action is not invoked")]
        public void T4()
        {
            // Arrange
            var invoked = false;
            var optional = Option.Some(string.Empty);

            // Act
            optional.IfEmpty(() => { invoked = true; });

            // Assert
            Assert.False(invoked);
        }

        [Gwt("Given a none optional string",
            "when if empty is used",
            "the otherwise action is invoked")]
        public void T5()
        {
            // Arrange
            var invoked = false;
            var optional = Option.None<string>();

            // Act
            optional.IfEmpty(() => { invoked = true; });

            // Assert
            Assert.True(invoked);
        }
    }
}
