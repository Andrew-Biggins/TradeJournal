using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class DrawdownTests
    {
        [GwtTheory("Given a long trade",
            "when the drawdown is read",
            "the drawdown is correct")]
        [InlineData(20, 10, 40, 10, 15, 0.5)]
        [InlineData(200, 100, 500, 290, 190, 0.1)]
        public void T0(double entry, double stop, double target, double close, double low, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(entry, stop, target),
                new Execution(entry, DateTime.MinValue, 0), Option.Some(testClose), (Option.None<double>(), Option.Some(low)), EntryOrderType.Limit);
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
