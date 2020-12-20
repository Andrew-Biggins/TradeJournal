using System;
using Common.MicroTests;
using Common.Optional;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class TradeResultsTests
    {
        [Gwt("Given an open trade",
            "when the result in R is read",
            "the result in R is none")]
        public void T0()
        {
            // Arrange
            var trade = TestOpenTrade;

            // Act 
            var actual = trade.ResultInR;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given an open trade",
            "when the points result is read",
            "the points result is none")]
        public void T1()
        {
            // Arrange
            var trade = TestOpenTrade;

            // Act 
            var actual = trade.PointsResult;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given an open trade",
            "when the cash result is read",
            "the cash result is none")]
        public void T2()
        {
            // Arrange
            var trade = TestOpenTrade;

            // Act 
            var actual = trade.CashResult;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [GwtTheory("Given a trade",
            "when the result in R is read",
            "the result in R is correct")]
        [InlineData(50, 100, 5)]
        [InlineData(8, 66, 5.8)]
        [InlineData(200, 100, -10)]
        public void T3(double open, double close, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 100),
                new Execution(open, DateTime.MinValue, 0), Option.Some(testClose), (Option.None<double>(), Option.None<double>()));
            var outResultInR = 0.00;

            // Act
            trade.ResultInR.IfExistsThen(x => { outResultInR = x; });

            // Assert
            Assert.Equal(expected, outResultInR);
        }

        [GwtTheory("Given a long trade",
            "when the points result is read",
            "the points result is correct")]
        [InlineData(50, 100, 50)]
        [InlineData(8, 66, 58)]
        [InlineData(200, 100, -100)]
        public void T4(double open, double close, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 100),
                new Execution(open, DateTime.MinValue, 0), Option.Some(testClose), (Option.None<double>(), Option.None<double>()));
            var outPointsResult = 0.00;

            // Act
            trade.PointsResult.IfExistsThen(x => { outPointsResult = x; });

            // Assert
            Assert.Equal(expected, outPointsResult);
        }

        [GwtTheory("Given a long trade",
            "when the points result is read",
            "the points result is correct")]
        [InlineData(200, 100, 100)]
        [InlineData(66, 8, 58)]
        [InlineData(200, 300, -100)]
        public void T5(double open, double close, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(200, 210, 100),
                new Execution(open, DateTime.MinValue, 0), Option.Some(testClose), (Option.None<double>(), Option.None<double>()));
            var outPointsResult = 0.00;

            // Act
            trade.PointsResult.IfExistsThen(x => { outPointsResult = x; });

            // Assert
            Assert.Equal(expected, outPointsResult);
        }

        [GwtTheory("Given a trade",
            "when the cash result is read",
            "the points cash is correct")]
        [InlineData(200, 100, 200)]
        [InlineData(66, 8, 116)]
        [InlineData(200, 300, -200)]
        public void T6(double open, double close, double expected)
        {
            // Arrange
            var testClose = new Execution(close, DateTime.Today, 1);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(200, 210, 100),
                new Execution(open, DateTime.MinValue, 2), Option.Some(testClose), (Option.None<double>(), Option.None<double>()));
            var outCashResult = 0.00;

            // Act
            trade.CashResult.IfExistsThen(x => { outCashResult = x; });

            // Assert
            Assert.Equal(expected, outCashResult);
        }
    }
}
