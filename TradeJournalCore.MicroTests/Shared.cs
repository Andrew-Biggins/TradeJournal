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

        internal static ISelectable TestMarket => new Market("Test Market");

        internal static ISelectable TestStrategy => new Market("Test Market");

        internal static Execution TestClose => new Execution(12345.67, DateTime.MaxValue, 1);

        internal static ITrade TestOpenTrade => new Trade(TestMarket, TestStrategy,
            new Levels(100, 50, 200), new Execution(100, DateTime.Today, 1), Option.None<Execution>(),
            (Option.None<Excursion>(), new Option.OptionNone<Excursion>()));

        internal static ITrade TestClosedTrade => new Trade(TestMarket, TestStrategy,
            new Levels(100, 50, 200), new Execution(100, DateTime.Today, 1), Option.Some(TestClose),
            (Option.None<Excursion>(), new Option.OptionNone<Excursion>()));
    }
}
