using System;
using System.Collections.Generic;
using System.Text;
using Common.MicroTests;
using Common.Optional;
using TradeJournalCore.Interfaces;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests
{
    public sealed class StatisticsGeneratorTests
    {
        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the wins are read",
            "they have been counted correctly")]
        public void T1()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(11, actual.Wins);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the loses are read",
            "they have been counted correctly")]
        public void T2()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(4, actual.Loses);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the longest winning streak is read",
            "it has been counted correctly")]
        public void T3()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(3, actual.LongestWinningStreak);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the longest losing streak is read",
            "it has been counted correctly")]
        public void T4()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(2, actual.LongestLosingStreak);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the points total is read",
            "it has been counted correctly")]
        public void T5()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(1585, actual.PointsTotal);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the cash total is read",
            "it has been counted correctly")]
        public void T6()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(1170, actual.CashTotal);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the biggest points win is read",
            "it is the correct value")]
        public void T7()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(260, actual.BiggestPointsWin);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the biggest cash win is read",
            "it is the correct value")]
        public void T8()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(520, actual.BiggestCashWin);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the biggest points loss is read",
            "it is the correct value")]
        public void T9()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(-425, actual.BiggestPointsLoss);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the biggest cash loss is read",
            "it is the correct value")]
        public void T10()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(-850, actual.BiggestCashLoss);
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average cash win is read",
            "it is the correct value")]
        public void T11()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(274.55, Math.Round(actual.AverageCashWin, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average cash loss is read",
            "it is the correct value")]
        public void T12()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(-462.5, Math.Round(actual.AverageCashLoss, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average points win is read",
            "it is the correct value")]
        public void T13()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(250.91, Math.Round(actual.AveragePointsWin, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average points loss is read",
            "it is the correct value")]
        public void T14()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(-293.75, Math.Round(actual.AveragePointsLoss, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the profit factor is read",
            "it is the correct value")]
        public void T15()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(1.63, Math.Round(actual.ProfitFactor, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average drawdown is read",
            "it is the correct value")]
        public void T16()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(0.78, Math.Round(actual.AverageDrawdown, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average maximum adverse excursion is read",
            "it is the correct value")]
        public void T17()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(39, Math.Round(actual.AverageMaximumAdverseExcursion, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average favourable adverse excursion is read",
            "it is the correct value")]
        public void T18()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(510.67, Math.Round(actual.AverageMaximumFavourableExcursion, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average risk reward ratio is read",
            "it is the correct value")]
        public void T20()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(2.00, Math.Round(actual.AverageRiskRewardRatio, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average result in R is read",
            "it is the correct value")]
        public void T21()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(2.11, Math.Round(actual.AverageResultInR, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average realised profit percentage is read",
            "it is the correct value")]
        public void T22()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(8.46, Math.Round(actual.AverageRealisedProfitPercentage, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average realised profit percentage is read",
            "it is the correct value")]
        public void T23()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(326.67, Math.Round(actual.AverageUnrealisedProfitPoints, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the average realised profit percentage is read",
            "it is the correct value")]
        public void T24()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(427.33, Math.Round(actual.AverageUnrealisedProfitCash, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the win probability is read",
            "it is the correct value")]
        public void T25()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(0.73, Math.Round(actual.WinProbability, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the cash expectancy is read",
            "it is the correct value")]
        public void T26()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(78, Math.Round(actual.CashExpectancy, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the points expectancy is read",
            "it is the correct value")]
        public void T27()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(105.67, Math.Round(actual.PointsExpectancy, 2));
        }

        [Gwt("Given a statistics generator calculates a trade collection's statistics",
            "when the gain is read",
            "it is the correct value")]
        public void T28()
        {
            // Arrange
            var actual = StatisticsGenerator.GetStatistics(_testTrades, 10000);

            // Assert
            Assert.Equal(0.12, Math.Round(actual.Gain, 2));
        }

        private readonly List<ITrade> _testTrades = new List<ITrade>
        {
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(7250.00), Option.Some(5990.00)), EntryOrderType.Limit), 
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(6800.00), Option.Some(5990.00)), EntryOrderType.Limit), 
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(7250.00), Option.Some(5950.00)), EntryOrderType.Limit),
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.None<Execution>(), (Option.None<double>(), Option.None<double>()), EntryOrderType.Limit),
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(new Execution(5750, DateTime.MaxValue, 1)), (Option.Some(7250.00), Option.Some(5750.00)), EntryOrderType.Limit),
            new Trade(TestMarket, TestStrategy, TestLevels, new Execution(6000, DateTime.MaxValue, 2), Option.Some(new Execution(5750, DateTime.MaxValue, 2)), (Option.Some(7250.00), Option.Some(5750.00)), EntryOrderType.Limit), 
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(7250.00), Option.Some(5935.00)), EntryOrderType.Limit), 
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(6010.00), Option.Some(5990.00)), EntryOrderType.Limit), 
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(6010.00), Option.Some(5990.00)), EntryOrderType.Limit),
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(new Execution(5750, DateTime.MaxValue, 1)), (Option.Some(6010.00), Option.Some(5750.00)), EntryOrderType.Limit), 
            new Trade(TestMarket, TestStrategy, TestLevels, new Execution(5750, DateTime.MaxValue, 2), Option.Some(new Execution(6010, DateTime.MaxValue, 2)), (Option.Some(6010.00), Option.Some(5990.00)), EntryOrderType.Limit),
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(6020.00), Option.Some(5930.00)), EntryOrderType.Limit), 
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(6010.00), Option.Some(5990.00)), EntryOrderType.Limit),
            new Trade(TestMarket, TestStrategy, TestLevels, new Execution(5750, DateTime.MaxValue, 2), Option.Some(new Execution(5325, DateTime.MaxValue, 2)), (Option.Some(6010.00), Option.Some(5990.00)), EntryOrderType.Limit),
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(6010.00), Option.Some(5990.00)), EntryOrderType.Limit), 
            new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(TestClose), (Option.Some(6020.00), Option.Some(5930.00)), EntryOrderType.Limit)
        };
    }
}
