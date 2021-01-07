using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModelAdapters;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.MainWindowViewModelTests.Shared;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class GraphTests
    {
        // Tests subscription to the Trade manager's property changed event
        [Gwt("Given a main window view model",
            "when a new trade is confirmed",
            "the plot is told to update data")]
        public void T0()
        {
            // Arrange
            var plot = SubPlot;
            var addTradeViewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(SubRunner, new TradeManager(), addTradeViewModel, plot);
            plot.ClearReceivedCalls();

            // Act 
            addTradeViewModel.ConfirmNewTradeCommand.Execute(null!);

            // Assert
            plot.Received(1).UpdateData(viewModel.AccountStartSize, viewModel.TradeManager.Trades);
        }
    }
}
