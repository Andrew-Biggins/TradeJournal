using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Common.Optional;
using TradeJournalCore.Interfaces;

namespace TradeJournalWPF.CustomSorters
{
    public enum TradeListColumn
    {
        CloseLevel,
        CloseTime,
        ResultInR,
        TotalPoints,
        CashProfit,
        Mae,
        Mfa,
        Drawdown,
        RealisedProfit,
        UnrealisedProfitPoints,
        UnrealisedProfitCash
    }

    public sealed class OptionalDoubleSorter : IComparer
    {
        public OptionalDoubleSorter(ListSortDirection direction, TradeListColumn column)
        {
            _direction = direction;
            _column = column;
        }

        public int Compare(object x, object y)
        {
            var result = 0;

            var tx = (ITrade)x;
            var ty = (ITrade)y;

            var xData = Option.None<double>();
            var yData = Option.None<double>();

            switch (_column)
            {
                case TradeListColumn.ResultInR:
                    xData = tx.ResultInR;
                    yData = ty.ResultInR;
                    break;
                case TradeListColumn.TotalPoints:
                    xData = tx.PointsResult;
                    yData = ty.PointsResult;
                    break;
                case TradeListColumn.CashProfit:
                    xData = tx.CashResult;
                    yData = ty.CashResult;
                    break;
                case TradeListColumn.Mae:
                    xData = tx.MaxAdverseExcursion;
                    yData = ty.MaxAdverseExcursion;
                    break;
                case TradeListColumn.Mfa:
                    xData = tx.MaxFavourableExcursion;
                    yData = ty.MaxFavourableExcursion;
                    break;
                case TradeListColumn.Drawdown:
                    xData = tx.DrawdownPercentage;
                    yData = ty.DrawdownPercentage;
                    break;
                case TradeListColumn.RealisedProfit:
                    xData = tx.RealisedProfitPercentage;
                    yData = ty.RealisedProfitPercentage;
                    break;
                case TradeListColumn.UnrealisedProfitPoints:
                    xData = tx.UnrealisedPointsProfit;
                    yData = ty.UnrealisedPointsProfit;
                    break;
                case TradeListColumn.UnrealisedProfitCash:
                    xData = tx.UnrealisedCashProfit;
                    yData = ty.UnrealisedCashProfit;
                    break;
            }


            xData.IfExistsThen(px =>
            {
                yData.IfExistsThen(py =>
                {
                    if (px.CompareTo(py) < 0)
                    {
                        result = -1;
                    }

                    if (px.CompareTo(py) > 0)
                    {
                        result = 1;
                    }

                }).IfEmpty(() => { result = -1; });
            }).IfEmpty(() =>
            {
                yData.IfExistsThen(py =>
                {
                    result = 1;
                });
            });

            if (_direction == ListSortDirection.Ascending)
            {
                result *= -1;
            }

            return result;
        }

        private readonly ListSortDirection _direction;
        private readonly TradeListColumn _column;
    }
}
