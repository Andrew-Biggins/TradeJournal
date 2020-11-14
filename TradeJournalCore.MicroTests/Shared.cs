using NSubstitute;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.MicroTests
{
    public static class Shared
    {
        internal static IRunner SubRunner => Substitute.For<IRunner>();

        internal static ITradeManager SubTradeManager => Substitute.For<ITradeManager>();
    }
}
