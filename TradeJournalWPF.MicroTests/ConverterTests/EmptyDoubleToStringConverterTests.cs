using Common.MicroTests;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class EmptyDoubleToStringConverterTests
    {
        [Gwt("Given a null value",
            "when converted",
            "the empty string is returned")]
        public void T0()
        {
            // Arrange
            var testConverter = new EmptyDoubleToStringConverter();

            // Act 
            var actual = testConverter.Convert(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given a value",
            "when converted",
            "the string of the value is returned")]
        public void T1()
        {
            // Arrange
            var testConverter = new EmptyDoubleToStringConverter();
            const double testValue = 6.00;

            // Act 
            var actual = testConverter.Convert(testValue, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(testValue.ToString(), actual);
        }

        [Gwt("Given a null target type",
            "when converted back",
            "null is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new EmptyDoubleToStringConverter();

            // Act 
            var actual = testConverter.ConvertBack(50, null, null!, InvariantCulture);

            // Assert
            Assert.Equal(null!, actual);
        }

        [Gwt("Given a null value",
            "when converted back",
            "default is returned")]
        public void T3()
        {
            // Arrange
            var testConverter = new EmptyDoubleToStringConverter();

            // Act 
            var actual = testConverter.ConvertBack(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(default, actual);
        }

        [Gwt("Given a value",
            "when converted back",
            "the same value is returned")]
        public void T4()
        {
            // Arrange
            var testConverter = new EmptyDoubleToStringConverter();
            const string testValue = "6.44";

            // Act 
            var actual = testConverter.ConvertBack(testValue, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(double.Parse(testValue), actual);
        }
    }
}
