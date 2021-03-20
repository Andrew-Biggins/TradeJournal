using System.Collections.Generic;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    internal static class StatisticsGenerator
    {
        public static TradeStatistics GetStatistics(IEnumerable<ITrade> trades, double startBalance)
        {
            double pointsTotal = 0,
                cashTotal = 0,
                biggestPointsWin = 0,
                biggestCashWin = 0,
                biggestPointsLoss = 0,
                biggestCashLoss = 0,
                pointsWinTotal = 0,
                pointsLossTotal = 0,
                cashWinTotal = 0,
                cashLossTotal = 0,
                maeTotal = 0,
                mfeTotal = 0,
                realisedProfitTotal = 0,
                unrealisedProfitPointsTotal = 0,
                unrealisedProfitCashTotal = 0,
                resultInRTotal = 0,
                riskRewardRatioTotal = 0,
                drawdownTotal = 0;

            int longestLosingStreak = 0,
                longestWinningStreak = 0,
                loseStreak = 0,
                winStreak = 0,
                wins = 0,
                loses = 0,
                maeCount = 0,
                mfeCount = 0;
            
            var previousTradeWon = false;

            foreach (var trade in trades)
            { 
                // Only use closed trades for statistics calculations
                trade.PointsResult.IfExistsThen(x =>
                {
                    pointsTotal += x;
                    riskRewardRatioTotal += trade.RiskRewardRatio;

                    trade.CashResult.IfExistsThen(y =>
                    {
                        cashTotal += y;
                        if (y > biggestCashWin)
                        {
                            biggestCashWin = y;
                        }

                        if (y < biggestCashLoss)
                        {
                            biggestCashLoss = y;
                        }
                    });

                    if (x > biggestPointsWin)
                    {
                        biggestPointsWin = x;
                    }

                    if (x < biggestPointsLoss)
                    {
                        biggestPointsLoss = x;
                    }

                    if (x > 0)
                    {
                        if (!previousTradeWon)
                        {
                            CheckLoseStreak();
                            loseStreak = 0;
                        }

                        wins++;
                        pointsWinTotal += x;

                        trade.CashResult.IfExistsThen(y =>
                        {
                            cashWinTotal += y;
                        });

                        winStreak++;
                        previousTradeWon = true;
                    }
                    else if (x < 0)
                    {
                        if (previousTradeWon)
                        {
                            CheckWinStreak();
                            winStreak = 0;
                        }

                        loses++;
                        pointsLossTotal += x;

                        trade.CashResult.IfExistsThen(y =>
                        {
                            cashLossTotal += y;
                        });

                        loseStreak++;
                        previousTradeWon = false;
                    }

                    trade.MaxAdverseExcursion.IfExistsThen(y => { maeTotal += y; maeCount++; });
                    trade.DrawdownPercentage.IfExistsThen(y => { drawdownTotal += y; });

                    trade.MaxFavourableExcursion.IfExistsThen(y => { mfeTotal += y; mfeCount++; });
                    trade.RealisedProfitPercentage.IfExistsThen(y => { realisedProfitTotal += y; });

                    trade.UnrealisedPointsProfit.IfExistsThen(y => { unrealisedProfitPointsTotal += y; });
                    trade.UnrealisedCashProfit.IfExistsThen(y => { unrealisedProfitCashTotal += y; });

                    trade.ResultInR.IfExistsThen(y => { resultInRTotal += y; });
                });
            }

            CheckLoseStreak();
            CheckWinStreak();

            var averagePointsWin = pointsWinTotal / wins;
            var averagePointsLoss = pointsLossTotal / loses;
            var averageCashWin = cashWinTotal / wins;
            var averageCashLoss = cashLossTotal / loses;
            var winProbability = (double) wins / (wins + loses);
            var total = wins + loses;
         
            return new TradeStatistics(wins, loses, winProbability, longestWinningStreak, longestLosingStreak,
                pointsTotal, cashTotal, biggestPointsWin, biggestCashWin, biggestPointsLoss, biggestCashLoss,
                averagePointsWin, averagePointsLoss, averageCashWin, averageCashLoss, cashWinTotal / -cashLossTotal,
                drawdownTotal / maeCount, maeTotal / maeCount, mfeTotal / mfeCount, riskRewardRatioTotal / total,
                resultInRTotal / total, realisedProfitTotal / mfeCount, unrealisedProfitPointsTotal / mfeCount,
                unrealisedProfitCashTotal / mfeCount, cashTotal / startBalance,
                CalculateExpectancy(averageCashWin, averageCashLoss),
                CalculateExpectancy(averagePointsWin, averagePointsLoss));

            void CheckWinStreak()
            {
                if (winStreak > longestWinningStreak)
                {
                    longestWinningStreak = winStreak;
                }
            }

            void CheckLoseStreak()
            {
                if (loseStreak > longestLosingStreak)
                {
                    longestLosingStreak = loseStreak;
                }
            }

            double CalculateExpectancy(double averageWin, double averageLoss)
            {
                return winProbability * averageWin - (1 - winProbability) * -averageLoss;
            }
        }
    }
}
