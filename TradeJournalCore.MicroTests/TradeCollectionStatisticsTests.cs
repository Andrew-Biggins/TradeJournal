using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class TradeCollectionStatisticsTests
    {
        [Gwt("Given a trade collection statistics",
            "when the trade count is read",
            "it is the correct value")]
        public void T0()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);

            // Act 
            var actual = stats.TradeCount;

            // Assert
            Assert.Equal(5, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the wins value is read",
            "it is the same as the one given at construction")]
        public void T1()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1);

            // Act 
            var actual = stats.Wins;

            // Assert
            Assert.Equal(2, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the loses value is read",
            "it is the same as the one given at construction")]
        public void T2()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1, 1,1);

            // Act 
            var actual = stats.Loses;

            // Assert
            Assert.Equal(3, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the loses value is read",
            "it is the same as the one given at construction")]
        public void T3()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 0.66, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.WinProbability;

            // Assert
            Assert.Equal(0.66, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the longest winning streak is read",
            "it is the same as the one given at construction")]
        public void T4()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 55, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1);

            // Act 
            var actual = stats.LongestWinningStreak;

            // Assert
            Assert.Equal(55, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the longest losing streak is read",
            "it is the same as the one given at construction")]
        public void T5()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 66, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1);

            // Act 
            var actual = stats.LongestLosingStreak;

            // Assert
            Assert.Equal(66, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the points total is read",
            "it is the same as the one given at construction")]
        public void T6()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 77, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1, 1);

            // Act 
            var actual = stats.PointsTotal;

            // Assert
            Assert.Equal(77, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the cash total is read",
            "it is the same as the one given at construction")]
        public void T7()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 88, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.CashTotal;

            // Assert
            Assert.Equal(88, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the biggest points win is read",
            "it is the same as the one given at construction")]
        public void T8()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 99, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.BiggestPointsWin;

            // Assert
            Assert.Equal(99, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the biggest cash win is read",
            "it is the same as the one given at construction")]
        public void T9()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 321, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1);

            // Act 
            var actual = stats.BiggestCashWin;

            // Assert
            Assert.Equal(321, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the biggest points loss is read",
            "it is the same as the one given at construction")]
        public void T10()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 567, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.BiggestPointsLoss;

            // Assert
            Assert.Equal(567, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the biggest cash loss is read",
            "it is the same as the one given at construction")]
        public void T11()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 789, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.BiggestCashLoss;

            // Assert
            Assert.Equal(789, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average points win is read",
            "it is the same as the one given at construction")]
        public void T12()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 367, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AveragePointsWin;

            // Assert
            Assert.Equal(367, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average points loss is read",
            "it is the same as the one given at construction")]
        public void T13()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 987, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AveragePointsLoss;

            // Assert
            Assert.Equal(987, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average cash win is read",
            "it is the same as the one given at construction")]
        public void T14()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 592, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageCashWin;

            // Assert
            Assert.Equal(592, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average cash loss is read",
            "it is the same as the one given at construction")]
        public void T15()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 786, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageCashLoss;

            // Assert
            Assert.Equal(786, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average drawdown is read",
            "it is the same as the one given at construction")]
        public void T16()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.77, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageDrawdown;

            // Assert
            Assert.Equal(0.77, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average MAE is read",
            "it is the same as the one given at construction")]
        public void T17()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 45, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageMaximumAdverseExcursion;

            // Assert
            Assert.Equal(45, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average MFE is read",
            "it is the same as the one given at construction")]
        public void T18()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 72, 1, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageMaximumFavourableExcursion;

            // Assert
            Assert.Equal(72, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average RRR is read",
            "it is the same as the one given at construction")]
        public void T19()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7.45, 1, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageRiskRewardRatio;

            // Assert
            Assert.Equal(7.45, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average Result in R is read",
            "it is the same as the one given at construction")]
        public void T20()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3.65, 1, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageResultInR;

            // Assert
            Assert.Equal(3.65, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average realised profit percentage is read",
            "it is the same as the one given at construction")]
        public void T21()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.87, 1, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageRealisedProfitPercentage;

            // Assert
            Assert.Equal(0.87, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average unrealised profit points is read",
            "it is the same as the one given at construction")]
        public void T22()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1.23, 1, 1, 1, 1);

            // Act 
            var actual = stats.AverageUnrealisedProfitPoints;

            // Assert
            Assert.Equal(1.23, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the average unrealised profit cash is read",
            "it is the same as the one given at construction")]
        public void T24()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5.33, 1, 1, 1);

            // Act 
            var actual = stats.AverageUnrealisedProfitCash;

            // Assert
            Assert.Equal(5.33, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the gain is read",
            "it is the same as the one given at construction")]
        public void T25()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1.23, 1, 1.76, 1, 1);

            // Act 
            var actual = stats.Gain;

            // Assert
            Assert.Equal(1.76, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the cash expectancy is read",
            "it is the same as the one given at construction")]
        public void T26()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1.23, 1, 1, 23.65, 1);

            // Act 
            var actual = stats.CashExpectancy;

            // Assert
            Assert.Equal(23.65, actual);
        }

        [Gwt("Given a trade collection statistics",
            "when the points expectancy is read",
            "it is the same as the one given at construction")]
        public void T27()
        {
            // Arrange
            var stats = new TradeStatistics(2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1.23, 1, 1, 1, 432.66);

            // Act 
            var actual = stats.PointsExpectancy;

            // Assert
            Assert.Equal(432.66, actual);
        }
    }
}
