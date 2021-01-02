using System;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.SelectableFactory;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeManagerTests
{
    public sealed class Shared
    {
        internal static Filters TestFilters => new Filters(GetDefaultMarkets(), GetDefaultStrategies(), GetAssetTypes(),
            GetDays(), DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999,
            TradeStatus.Both, TradeDirection.Both);

        internal static TradeDetailsViewModel TestTradeDetailsViewModel => new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel())
        {
            SelectedMarket = new Market("Gold", AssetClass.Commodities),
            SelectedStrategy = new Strategy("Triangle")
        };
    }
}
