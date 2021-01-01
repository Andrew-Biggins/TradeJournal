using System;
using Common.Optional;
using NSubstitute;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.MicroTests
{
    public static class Shared
    {
        internal static IRunner SubRunner => Substitute.For<IRunner>();

        internal static ITradeManager SubTradeManager => Substitute.For<ITradeManager>();

        internal static IMarket TestMarket => new Market("Gold", AssetClass.Commodities);

        internal static ISelectable TestStrategy => new Strategy("Triangle");

        internal static Levels TestLevels => new Levels(100, 50, 200);

        internal static Execution TestOpen => new Execution(100, DateTime.Today, 1);

        internal static Execution TestClose => new Execution(12345.67, DateTime.MaxValue, 1);

        internal static (Optional<double>, Optional<double>) TestEmptyExcursions =>
            (Option.None<double>(), Option.None<double>());

        internal static ITrade TestOpenTrade => new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
            Option.None<Execution>(), TestEmptyExcursions);

        internal static ITrade TestClosedTrade => new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
            Option.Some(TestClose), TestEmptyExcursions);
    }
}
