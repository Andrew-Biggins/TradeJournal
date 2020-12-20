using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class DirectionTests
    {
        [Gwt("Given a trade with a target higher than stop",
            "when the direction is read",
            "the direction is long")]
        public void T0()
        {
            // Arrange
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(1, 2, 3),
                new Execution(0, DateTime.MinValue, 0), Option.None<Execution>(), (Option.None<double>(), Option.None<double>()));

            // Act 
            var actual = trade.Direction;

            // Assert
            Assert.Equal(TradeDirection.Long, actual);
        }

        [Gwt("Given a trade with a target Lower than stop",
            "when the direction is read",
            "the direction is short")]
        public void T1()
        {
            // Arrange
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(10, 15, 5),
                new Execution(0, DateTime.MinValue, 0), Option.None<Execution>(), (Option.None<double>(), Option.None<double>()));

            // Act 
            var actual = trade.Direction;

            // Assert
            Assert.Equal(TradeDirection.Short, actual);
        }
    }
}
