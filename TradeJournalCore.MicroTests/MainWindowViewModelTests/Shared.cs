using NSubstitute;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    internal static class Shared
    {
        internal static ITradePlot SubPlot => Substitute.For<ITradePlot>();
    }
}
