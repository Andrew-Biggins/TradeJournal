using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    internal static class TradeFilterer
    {
        internal static IEnumerable<ITrade> RemoveUnselectedAssetClasses(IEnumerable<ITrade> trades,
            IEnumerable<ISelectable> assetClasses)
        {
            var newList = new List<ITrade>();

            var selectedAssetClasses = assetClasses.ToList();

            foreach (var trade in trades)
            {
                newList.AddRange(from assetClass in selectedAssetClasses
                    where assetClass.Name == trade.Market.AssetClass.ToString()
                    select trade);
            }

            return newList;
        }

        internal static IEnumerable<ITrade> RemoveUnselectedMarkets(IEnumerable<ITrade> trades,
            IEnumerable<ISelectable> markets)
        {
            var newList = new List<ITrade>();

            var selectedMarkets = markets.ToList();

            foreach (var trade in trades)
            {
                newList.AddRange(from market in selectedMarkets where market.Name == trade.Market.Name select trade);
            }

            return newList;
        }

        internal static IEnumerable<ITrade> RemoveUnselectedStrategies(IEnumerable<ITrade> trades,
            IEnumerable<ISelectable> strategies)
        {
            var newList = new List<ITrade>();

            var selectedStrategies = strategies.ToList();

            foreach (var trade in trades)
            {
                newList.AddRange(from strategy in selectedStrategies
                    where strategy.Name == trade.Strategy.Name
                    select trade);
            }

            return newList;
        }

        internal static IEnumerable<ITrade> RemoveUnselectedDays(IEnumerable<ITrade> trades,
            IEnumerable<ISelectable> days)
        {
            var newList = new List<ITrade>();

            var selectedDays = days.ToList();

            foreach (var trade in trades)
            {
                newList.AddRange(from day in selectedDays
                    where day.Name == trade.Open.DateTime.DayOfWeek.ToString()
                    select trade);
            }

            return newList;
        }

        internal static IEnumerable<ITrade> RemoveTradesOutsideDateRange(IEnumerable<ITrade> trades, DateTime startDate,
            DateTime endDate)
        {
            var newList = new List<ITrade>();

            foreach (var trade in trades)
            {
                if (trade.Open.DateTime >= startDate && trade.Open.DateTime <= endDate)
                {
                    newList.Add(trade);
                }
            }

            return newList;
        }

        internal static IEnumerable<ITrade> RemoveTradesOutsideTimeRange(IEnumerable<ITrade> trades, DateTime startTime,
            DateTime endTime)
        {
            var newList = new List<ITrade>();

            foreach (var trade in trades)
            {
                if (trade.Open.DateTime.TimeOfDay >= startTime.TimeOfDay &&
                    trade.Open.DateTime.TimeOfDay <= endTime.TimeOfDay)
                {
                    newList.Add(trade);
                }
            }

            return newList;
        }

        internal static IEnumerable<ITrade> RemoveTradesOutsideRiskRewardRatioRange(IEnumerable<ITrade> trades,
            double minRiskRewardRatio, double maxRiskRewardRatio)
        {
            var newList = new List<ITrade>();

            foreach (var trade in trades)
            {
                if (trade.RiskRewardRatio >= minRiskRewardRatio &&
                    trade.RiskRewardRatio <= maxRiskRewardRatio)
                {
                    newList.Add(trade);
                }
            }

            return newList;
        }

        internal static IEnumerable<ITrade> RemoveUnselectedTradeStatuses(IEnumerable<ITrade> trades, TradeStatus status)
        {
            var newList = new List<ITrade>();

            foreach (var trade in trades)
            {
                if (status == TradeStatus.Both || status == TradeStatus.Closed)
                {
                    trade.Close.IfExistsThen(x => { newList.Add(trade); });
                }

                if (status == TradeStatus.Both || status == TradeStatus.Open)
                {
                    trade.Close.IfEmpty(() => { newList.Add(trade); });
                }
            }

            return newList;
        }

        internal static ObservableCollection<ITrade> RemoveUnselectedTradeDirections(IEnumerable<ITrade> trades,
            TradeDirection direction)
        {
            var newList = new ObservableCollection<ITrade>();

            foreach (var trade in trades)
            {
                if (direction == TradeDirection.Both || direction == trade.Direction)
                {
                    newList.Add(trade);
                }
            }

            return newList;
        }
    }
}
