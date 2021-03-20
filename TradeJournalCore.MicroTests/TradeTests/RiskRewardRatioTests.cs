using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class RiskRewardRatioTests
    {
        [GwtTheory("Given a trade",
            "when the risk reward ratio is read",
            "the risk reward ratio is correct")]
        [InlineData(20, 10, 30, 1)]
        [InlineData(20, 30, 10, 1)]
        [InlineData(200, 100, 1000, 8)]
        public void T0(double entry, double stop, double target, double expected)
        {
            // Arrange
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(entry, stop, target),
                new Execution(0, DateTime.MinValue, 0), Option.None<Execution>(), (Option.None<double>(), Option.None<double>()), EntryOrderType.Limit);

            // Act 
            var actual = trade.RiskRewardRatio;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
