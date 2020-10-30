using Common.MicroTests;
using Common.Optional;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class ExcursionTests
    {
        [Gwt("Given a trade",
            "when the maximum adverse excursion is read",
            "the the maximum adverse excursion points is none by default")]
        public void T0()
        {
            // Arrange
            var trade = new Trade();

            // Act 
            var actual = trade.MaxAdverseExcursion;

            // Assert
            Assert.IsType<Option.OptionNone<Excursion>>(actual);
        }

        [Gwt("Given a trade",
            "when the maximum favourable excursion is read",
            "the the maximum favourable excursion is none by default")]
        public void T1()
        {
            // Arrange
            var trade = new Trade();

            // Act 
            var actual = trade.MaxFavourableExcursion;

            // Assert
            Assert.IsType<Option.OptionNone<Excursion>>(actual);
        }
    }
}
