using Common.MicroTests;
using Common.Optional;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class OptionalDoubleToStringConverterTests
    {
        [Gwt("Given a null value",
            "when converted",
            "the empty string is returned")]
        public void T0()
        {
            // Arrange
            var testConverter = new OptionalDoubleToStringConverter();

            // Act 
            var actual = testConverter.Convert(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option none double",
            "when converted",
            "the empty string is returned")]
        public void T1()
        {
            // Arrange
            var testConverter = new OptionalDoubleToStringConverter();
            var optional = Option.None<double>();

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option some double",
            "when converted",
            "the same double is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new OptionalDoubleToStringConverter();
            const double testValue = 5.34;
            var optional = Option.Some(testValue);

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(testValue, actual);
        }

        [Gwt("Given a null target type",
            "when converted back",
            "null is returned")]
        public void T3()
        {
            // Arrange
            var testConverter = new OptionalDoubleToStringConverter();

            // Act 
            var actual = testConverter.ConvertBack(null, null, null!, InvariantCulture);

            // Assert
            Assert.Equal(null!, actual);
        }

        [Gwt("Given a null value",
            "when converted back",
            "an option none double is returned")]
        public void T4()
        {
            // Arrange
            var testConverter = new OptionalDoubleToStringConverter();

            // Act 
            var actual = testConverter.ConvertBack(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given a value",
            "when converted back",
            "an option some double of the same value is returned")]
        public void T5()
        {
            // Arrange
            var testConverter = new OptionalDoubleToStringConverter(); 
            const double testValue = 5.34;
            
            // Act 
            var actual = testConverter.ConvertBack(testValue.ToString(), typeof(object), null!, InvariantCulture);

            // Assert
            var d = (Optional<double>)actual;
            d.IfExistsThen(x => { Assert.Equal(testValue, x); });
        }
    }
}
