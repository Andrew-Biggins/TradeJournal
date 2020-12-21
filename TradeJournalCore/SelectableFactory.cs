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
        internal static ObservableCollection<ISelectable> GetDefaultMarkets()
        {
            return new ObservableCollection<ISelectable>()
            {
                new Market("USDJPY", AssetClass.Currencies) { IsSelected = true },
                new Market("EURUSD", AssetClass.Currencies) { IsSelected = true },
                new Market("Nasdaq", AssetClass.Indices) { IsSelected = true },
                new Market("BTCUSD", AssetClass.Crypto) { IsSelected = true },
                new Market("Gold", AssetClass.Commodities) { IsSelected = true },
                new Market("Silver", AssetClass.Commodities) { IsSelected = true },
                new Market("TSLA", AssetClass.Shares) { IsSelected = true }
            };
        }

        internal static ObservableCollection<ISelectable> GetDefaultStrategies()
        {
            return new ObservableCollection<ISelectable>()
            {
                new Strategy("Triangle") { IsSelected = true },
                new Strategy("Gap fill") { IsSelected = true }
            };
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
