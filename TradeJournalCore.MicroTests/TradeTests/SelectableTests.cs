using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class SelectableTests
    {
        [Gwt("Given a trade",
            "when the market is read",
            "the market is the same as the one given at construction")]
        public void T0()
        {
            // Arrange
            var testMarket = new Market("test market");
            var trade = new Trade(testMarket, new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<Excursion>(), Option.None<Excursion>()));

            // Act 
            var actual = trade.Market;

            // Assert
            Assert.Equal(testMarket, actual);
        }

        [Gwt("Given a trade",
            "when the strategy is read",
            "the strategy is the same as the one given at construction")]
        public void T1()
        {
            // Arrange
            var testStrategy = new Market("test strategy");
            var trade = new Trade(new Market(string.Empty), testStrategy, new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<Excursion>(), Option.None<Excursion>()));

            // Act 
            var actual = trade.Strategy;

            // Assert
            Assert.Equal(testStrategy, actual);
        }
    }
}
