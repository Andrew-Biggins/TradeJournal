using System;
using System.Collections.ObjectModel;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
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

        internal static ObservableCollection<ISelectable> GetAssetTypes()
        {
            var list = new ObservableCollection<ISelectable>();

            var assetClasses = (AssetClass[])Enum.GetValues(typeof(AssetClass));

            foreach (var assetClass in assetClasses)
            {
                list.Add(new AssetType(assetClass) { IsSelected = true });
            }

            return list;
        }

        internal static ObservableCollection<ISelectable> GetDays()
        {
            var list = new ObservableCollection<ISelectable>();

            var days = (DayOfWeek[])Enum.GetValues(typeof(DayOfWeek));

            foreach (var day in days)
            {
                list.Add(new Day(day) { IsSelected = true });
            }

            return list;
        }
    }
}
