using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Optional;
using NSubstitute;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.SelectableFactory;

namespace TradeJournalCore.MicroTests
{
    internal static class Shared
    {
        internal static IRunner SubRunner => Substitute.For<IRunner>();

        internal static ITradeManager SubTradeManager
        {
            get
            {
                var subTradeManager = Substitute.For<ITradeManager>();
                subTradeManager.Trades.Returns(new ObservableCollection<ITrade>());
                return subTradeManager;
            }
        }

        internal static IMarket TestMarket => new Market("Gold", AssetClass.Commodities, PipDivisor.One);

        internal static ISelectable TestStrategy => new Strategy("Triangle");

        internal static Levels TestLevels => new Levels(100, 50, 200);

        internal static Execution TestOpen => new Execution(TestsOpenLevel, DateTime.Today, TestSize);

        internal static Execution TestClose => new Execution(TestsCloseLevel, DateTime.MaxValue, TestSize);

        internal static (Optional<double>, Optional<double>) TestEmptyExcursions =>
            (Option.None<double>(), Option.None<double>());

        internal static ITrade TestOpenTrade => new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
            Option.None<Execution>(), TestEmptyExcursions, EntryOrderType.Limit);

        internal static ITrade TestClosedTrade => new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
            Option.Some(TestClose), TestEmptyExcursions, EntryOrderType.Limit);

        internal static Filters TestFilters => new Filters(TestMarkets, TestStrategies, GetAssetTypes(),
            GetDays(), DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999,
            TradeStatus.Both, TradeDirection.Both);

        internal static TradeDetailsViewModel TestTradeDetailsViewModel => new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel())
        {
            SelectedMarket = TestMarket,
            SelectedStrategy = TestStrategy,
            CloseLevel = Option.Some(TestsCloseLevel),
            CloseDateTime = DateTime.MaxValue,
            Open = { DateTime = DateTime.MaxValue, Level = TestsOpenLevel, Size = TestSize}
        };

        private static IReadOnlyList<ISelectable> TestMarkets
        {
            get
            {
                var markets = new List<ISelectable>
                {
                    TestMarket,
                    new Market("USDJPY", AssetClass.Currencies, PipDivisor.One)
                };

                return markets;
            }
        }

        private static IReadOnlyList<ISelectable> TestStrategies
        {
            get
            {
                var markets = new List<ISelectable>
                {
                    TestStrategy,
                    new Strategy("Gap fill"),
                };

                return markets;
            }
        }

        private const double TestsOpenLevel = 6000.00;
        private const double TestsCloseLevel = 6250.00;
        private const double TestSize = 1;
    }
}
