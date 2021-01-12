using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.MainWindowViewModelTests.Shared;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class GraphTests
    {
        // Tests subscription to the Trade manager's Trades property changed event
        [Gwt("Given a main window view model",
            "when the trade manager filters trades",
            "the plot is told to update data")]
        public void T0()
        {
            // Arrange
            var plot = SubPlot;
            var viewModel = new MainWindowViewModel(SubRunner, new TradeManager(), TestTradeDetailsViewModel, plot);
            plot.ClearReceivedCalls();

            // Act 
            viewModel.TradeManager.FilterTrades(TestFilters);

            // Assert
            plot.Received(1).UpdateData(viewModel.AccountStartSize, viewModel.TradeManager.Trades);
        }
    }
}
