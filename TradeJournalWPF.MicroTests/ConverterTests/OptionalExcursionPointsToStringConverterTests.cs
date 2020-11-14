using Common.MicroTests;
using Common.Optional;
using TradeJournalCore;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class OptionalExcursionPointsToStringConverterTests
    {
        [Gwt("Given a null value",
            "when converted",
            "the empty string is returned")]
        public void T0()
        {
            // Arrange
            var testConverter = new OptionalExcursionPointsToStringConverter();

            // Act 
            var actual = testConverter.Convert(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option none excursion",
            "when converted",
            "the empty string is returned")]
        public void T1()
        {
            // Arrange
            var testConverter = new OptionalExcursionPointsToStringConverter();
            var optional = Option.None<Excursion>();

            // Act 
            var actual = testConverter.Convert(optional, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Gwt("Given an option some excursion",
            "when converted",
            "the excursion points value string is returned")]
        public void T2()
        {
            // Arrange
            var testConverter = new OptionalExcursionPointsToStringConverter();
            const double testValue = 5.34;
            var excursion = new Excursion(testValue, 100);
            var optional = Option.Some(excursion);

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
            var testConverter = new OptionalExcursionPointsToStringConverter();

            // Act 
            var actual = testConverter.ConvertBack(null, null, null!, InvariantCulture);

            // Assert
            Assert.Equal(null!, actual);
        }

        [Gwt("Given a null value",
            "when converted back",
            "an option none excursion is returned")]
        public void T4()
        {
            // Arrange
            var testConverter = new OptionalExcursionPointsToStringConverter();

            // Act 
            var actual = testConverter.ConvertBack(null, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.IsType<Option.OptionNone<Excursion>>(actual);
        }

        [Gwt("Given a value",
            "when converted back",
            "an option some excursion with the same points value is returned")]
        public void T5()
        {
            // Arrange
            var testConverter = new OptionalExcursionPointsToStringConverter();
            const double testValue = 5.34;

            // Act 
            var actual = testConverter.ConvertBack(testValue.ToString(), typeof(object), null!, InvariantCulture);

            // Assert
            var d = (Optional<Excursion>)actual;
            d.IfExistsThen(x => 
                { x.Points.IfExistsThen(y =>
                    {
                        Assert.Equal(testValue, y);
                    });
                });
        }
    }
}
