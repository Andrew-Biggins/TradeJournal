using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public static class DataExporter
    {
        public static void WriteTradeDataCsv(IEnumerable<ITrade> trades, string filePath)
        {
            var csv = new StringBuilder();

            csv.AppendLine(
                "Market, Strategy, Strategy Entry, Stop, Target, Long/Short, Risk Reward Ratio, Open Date/Time," +
                " Open Level, Size, Close Date/Time, Close Level, Total Points, Profit/Loss, Result in R, MAE, MFA," +
                " Drawdown, Realised Profit %, Unrealised Profit Points, Unrealised Profit Cash");

            foreach (var trade in trades)
            {
                var closeLevel = string.Empty;
                var closeDateTime = string.Empty;
                var totalPoints = string.Empty;
                var profit = string.Empty;
                var resultInR = string.Empty;
                var mae = string.Empty;
                var mfe = string.Empty;
                var drawdown = string.Empty;
                var realisedProfitPercentage = string.Empty;
                var unrealisedPointsProfit = string.Empty;
                var unrealisedCashProfit = string.Empty;

                trade.Close.IfExistsThen(x =>
                {
                    closeLevel = x.Level.ToString(CultureInfo.CurrentCulture);
                    closeDateTime = x.Date.ToString(CultureInfo.CurrentCulture);
                });

                trade.PointsResult.IfExistsThen(x => { totalPoints = x.ToString(CultureInfo.CurrentCulture); });
                trade.CashResult.IfExistsThen(x => { profit = x.ToString(CultureInfo.CurrentCulture); });
                trade.ResultInR.IfExistsThen(x => { resultInR = x.ToString(CultureInfo.CurrentCulture); });
                trade.MaxAdverseExcursion.IfExistsThen(x => { mae = x.ToString(CultureInfo.CurrentCulture); });
                trade.MaxFavourableExcursion.IfExistsThen(x => { mfe = x.ToString(CultureInfo.CurrentCulture); });
                trade.DrawdownPercentage.IfExistsThen(x => { drawdown = x.ToString(CultureInfo.CurrentCulture); });
                trade.RealisedProfitPercentage.IfExistsThen(x => { realisedProfitPercentage = x.ToString(CultureInfo.CurrentCulture); });
                trade.UnrealisedPointsProfit.IfExistsThen(x => { unrealisedPointsProfit = x.ToString(CultureInfo.CurrentCulture); });
                trade.UnrealisedCashProfit.IfExistsThen(x => { unrealisedCashProfit = x.ToString(CultureInfo.CurrentCulture); });


                var newLine =
                    $"{trade.Market.Name},{trade.Strategy.Name},{trade.Levels.Entry},{trade.Levels.Stop},{trade.Levels.Target}," +
                    $"{trade.Direction},{trade.RiskRewardRatio},{trade.Open.Date},{trade.Open.Level},{trade.Open.Size},{closeDateTime}," +
                    $"{closeLevel},{totalPoints},{profit},{resultInR},{mae},{mfe},{drawdown},{realisedProfitPercentage}," +
                    $"{unrealisedPointsProfit},{unrealisedCashProfit}";
                csv.AppendLine(newLine);
            }

            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
