using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public enum TradeStatus
    {
        Open,
        Closed,
        Both
    }

    internal static class SelectableFactory
    {
        internal static SelectableCollection<IMarket> GetDefaultMarkets()
        {
            var collection = new SelectableCollection<IMarket>();

            collection.AddSelectable(new Market("USDJPY", AssetClass.Currencies) { IsSelected = true });
            collection.AddSelectable(new Market("EURUSD", AssetClass.Currencies) { IsSelected = true });
            collection.AddSelectable(new Market("Nasdaq", AssetClass.Indices) { IsSelected = true });
            collection.AddSelectable(new Market("BTCUSD", AssetClass.Crypto) { IsSelected = true });
            collection.AddSelectable(new Market("Silver", AssetClass.Commodities) { IsSelected = true });
            collection.AddSelectable(new Market("Gold", AssetClass.Commodities) { IsSelected = true });
            collection.AddSelectable(new Market("TSLA", AssetClass.Shares) { IsSelected = true });

            return collection;
        }

        internal static SelectableCollection<ISelectable> GetDefaultStrategies()
        {
            var collection = new SelectableCollection<ISelectable>();

            collection.AddSelectable(new Strategy("Triangle") { IsSelected = true });
            collection.AddSelectable(new Strategy("Gap fill") { IsSelected = true });

            return collection;
        }

        internal static IReadOnlyList<ISelectable> GetAssetTypes()
        {
            var list = new List<ISelectable>();

            var assetClasses = (AssetClass[])Enum.GetValues(typeof(AssetClass));

            foreach (var assetClass in assetClasses)
            {
                list.Add(new AssetType(assetClass) { IsSelected = true });
            }

            return list;
        }

        internal static IReadOnlyList<ISelectable> GetDays()
        {
            var list = new List<ISelectable>();

            var days = (DayOfWeek[])Enum.GetValues(typeof(DayOfWeek));

            foreach (var day in days)
            {
                list.Add(new Day(day) { IsSelected = true });
            }

            return list;
        }

        internal static IReadOnlyList<TradeStatus> GetTradeStatuses()
        {
            var list = new List<TradeStatus>();

            var statuses = (TradeStatus[])Enum.GetValues(typeof(TradeStatus));

            foreach (var status in statuses)
            {
                list.Add(status);
            }

            return list;
        }

        internal static IReadOnlyList<TradeDirection> GetTradeDirections()
        {
            var list = new List<TradeDirection>();

            var directions = (TradeDirection[])Enum.GetValues(typeof(TradeDirection));

            foreach (var direction in directions)
            {
                list.Add(direction);
            }

            return list;
        }
    }
}
