using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class ExcursionStatsTests
    {
        [GwtTheory("Given a long trade",
            "when the realised profit percentage is read",
            "the realised profit percentage is correct")]
        [InlineData(10, 110, 210, 0.5)]
        [InlineData(125, 200, 225, 0.75)]
        public void T0(double open, double close, double high, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 30),
                new Execution(open, DateTime.MinValue, 0), Option.Some(testClose), (Option.Some(high), Option.None<double>()), EntryOrderType.Limit);
            var outRealisedProfitPercentage = 0.00;

            // Act
            trade.RealisedProfitPercentage.IfExistsThen(x => { outRealisedProfitPercentage = x; });

            // Assert
            Assert.Equal(expected, outRealisedProfitPercentage);
        }

        [GwtTheory("Given a long trade",
            "when the unrealised points profit is read",
            "the unrealised points profit is correct")]
        [InlineData(10, 110, 210, 100)]
        [InlineData(125, 200, 225, 25)]
        public void T1(double open, double close, double high, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 30),
                new Execution(open, DateTime.MinValue, 0), Option.Some(testClose), (Option.Some(high), Option.None<double>()), EntryOrderType.Limit);
            var outUnrealisedPointsProfit = 0.00;

            // Act
            trade.UnrealisedPointsProfit.IfExistsThen(x => { outUnrealisedPointsProfit = x; });

            // Assert
            Assert.Equal(expected, outUnrealisedPointsProfit);
        }

        [GwtTheory("Given a trade",
            "when the unrealised cash profit is read",
            "the unrealised cash profit is correct")]
        [InlineData(10, 110, 210, 100)]
        [InlineData(125, 200, 225, 25)]
        public void T2(double open, double close, double high, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 30),
                new Execution(open, DateTime.MinValue, 1), Option.Some(testClose), (Option.Some(high), Option.None<double>()), EntryOrderType.Limit);
            var outUnrealisedCashProfit = 0.00;

            // Act
            trade.UnrealisedCashProfit.IfExistsThen(x => { outUnrealisedCashProfit = x; });

            // Assert
            Assert.Equal(expected, outUnrealisedCashProfit);
        }

        [Gwt("Given an open trade with",
            "when the realised profit percentage is read",
            "the realised profit percentage is none")]
        public void T3()
        {
            // Arrange
            var trade = TestOpenTrade;

            // Act 
            var actual = trade.RealisedProfitPercentage;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given an open trade with",
            "when the unrealised points profit is read",
            "the unrealised points profit  is none")]
        public void T4()
        {
            // Arrange
            var trade = TestOpenTrade;

            // Act 
            var actual = trade.UnrealisedPointsProfit;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given an open trade with",
            "when the unrealised cash profit is read",
            "the unrealised cash profit is none")]
        public void T5()
        {
            // Arrange
            var trade = TestOpenTrade;

            // Act 
            var actual = trade.UnrealisedCashProfit;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }
    }
}
