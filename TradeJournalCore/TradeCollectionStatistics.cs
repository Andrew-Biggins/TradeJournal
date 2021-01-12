namespace TradeJournalCore
{
    public sealed class TradeCollectionStatistics
    {
        public int TradeCount { get; }

        public int Wins { get; }

        public int Loses { get; } 

        public int LongestWinningStreak { get; }

        public int LongestLosingStreak { get; }

        public double PointsTotal { get; } 

        public double CashTotal { get; }

        public double BiggestPointsWin { get; } 

        public double BiggestCashWin { get; } 

        public double BiggestPointsLoss { get; } 

        public double BiggestCashLoss { get; } 

        public double AveragePointsWin { get; }

        public double AveragePointsLoss { get; } 

        public double AverageCashWin { get; } 

        public double AverageCashLoss { get; }

        public double WinProbability { get; } 

        public double ProfitFactor { get; }

        public double AverageDrawdown { get; } 

        public double AverageMaximumAdverseExcursion { get; }

        public double AverageMaximumFavourableExcursion { get; } 

        public double AverageRiskRewardRatio { get; }

        public double AverageResultInR { get; } 

        public double AverageRealisedProfitPercentage { get; }

        public double AverageUnrealisedProfitPoints { get; }

        public double AverageUnrealisedProfitCash { get; }

        public double Gain { get; } 

        public double CashExpectancy { get; } 

        public double PointsExpectancy { get; }

        public TradeCollectionStatistics(int wins, int loses, double winProbability, int longestWinningStreak,
            int longestLosingStreak, double pointsTotal, double cashTotal, double biggestPointsWin,
            double biggestCashWin, double biggestPointsLoss, double biggestCashLoss, double averagePointsWin,
            double averagePointsLoss, double averageCashWin, double averageCashLoss, double profitFactor, 
            double averageDrawdown, double averageMaximumAdverseExcursion, double averageMaximumFavourableExcursion,
            double averageRiskRewardRatio, double averageResultInR, double averageRealisedProfitPercentage,
            double averageUnrealisedProfitPoints, double averageUnrealisedProfitCash, double gain, 
            double cashExpectancy, double pointsExpectancy)
        {
            TradeCount = wins + loses;
            Wins = wins;
            Loses = loses;
            LongestWinningStreak = longestWinningStreak;
            LongestLosingStreak = longestLosingStreak;
            PointsTotal = pointsTotal;
            CashTotal = cashTotal;
            BiggestPointsWin = biggestPointsWin;
            BiggestCashWin = biggestCashWin;
            BiggestPointsLoss = biggestPointsLoss;
            BiggestCashLoss = biggestCashLoss;
            AveragePointsWin = averagePointsWin;
            AveragePointsLoss = averagePointsLoss;
            AverageCashWin = averageCashWin;
            AverageCashLoss = averageCashLoss;
            ProfitFactor = profitFactor;
            AverageDrawdown = averageDrawdown;
            AverageMaximumAdverseExcursion = averageMaximumAdverseExcursion;
            AverageMaximumFavourableExcursion = averageMaximumFavourableExcursion;
            AverageRiskRewardRatio = averageRiskRewardRatio;
            AverageResultInR = averageResultInR;
            AverageRealisedProfitPercentage = averageRealisedProfitPercentage;
            AverageUnrealisedProfitPoints = averageUnrealisedProfitPoints;
            AverageUnrealisedProfitCash = averageUnrealisedProfitCash;
            WinProbability = winProbability;
            CashExpectancy = cashExpectancy;
            PointsExpectancy = pointsExpectancy;
            Gain = gain;
        }
    }
}