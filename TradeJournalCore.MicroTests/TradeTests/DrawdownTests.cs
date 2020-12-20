using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class DrawdownTests
    {
        [GwtTheory("Given a trade",
            "when the drawdown is read",
            "the drawdown is correct")]
        [InlineData(20, 10, 10, 5, 0.5)]
        [InlineData(20, 30, 10, 5, 0.5)]
        [InlineData(200, 100, 10, 10, 0.1)]
        public void T0(double entry, double stop, double close, double mae, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(entry, stop, 100),
                new Execution(0, DateTime.MinValue, 0), Option.Some(testClose), (Option.Some(mae), Option.None<double>()));
            var outDrawdown = 0.00;

            // Act
            trade.DrawdownPercentage.IfExistsThen(x => { outDrawdown = x; });

            // Assert
            Assert.Equal(expected, outDrawdown);
        }

        [Gwt("Given a trade with no maximum adverse excursion",
            "when the drawdown percentage is read",
            "the drawdown percentage is none")]
        public void T1()
        {
            // Arrange
            var trade = TestOpenTrade;

            // Act 
            var actual = trade.DrawdownPercentage;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }
    }
}
