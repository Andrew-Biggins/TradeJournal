using Common.MicroTests;
using Common.Optional;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class ExcursionTests
    {
        [Gwt("Given an excursion",
            "when the excursion points is read",
            "the excursion points is none by default")]
        public void T0()
        {
            // Arrange
            var excursion = new Excursion();

            // Act 
            var actual = excursion.Points;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given an excursion",
            "when the excursion percentage is read",
            "the excursion percentage is none by default")]
        public void T1()
        {
            // Arrange
            var excursion = new Excursion();

            // Act 
            var actual = excursion.Percentage;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }
    }
}
