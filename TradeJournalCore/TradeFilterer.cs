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

        internal static ObservableCollection<ITrade> RemoveUnselectedStrategies(IEnumerable<ITrade> trades, IEnumerable<ISelectable> strategies)
        {
            var newList = new ObservableCollection<ITrade>();

            var selectedStrategies = strategies.ToList();

            foreach (var trade in trades)
            {
                foreach (var strategy in selectedStrategies)
                {
                    if (strategy.Name == trade.Strategy.Name)
                    {
                        newList.Add(trade);
                    }
                }
            }

            return newList;
        }
    }
}
