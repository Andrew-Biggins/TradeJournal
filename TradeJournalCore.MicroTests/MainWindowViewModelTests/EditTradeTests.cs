using Common;
using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;
using static TradeJournalCore.MicroTests.MainWindowViewModelTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class EditTradeTests
    {
        [Gwt("Given a main window view model",
            "when the edit trade command is executed",
            "the runner is passed the add trade view model and told to get the trade detail")]
        public void T0()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel, SubPlot);

            // Act 
            viewModel.EditTradeCommand.Execute(null!);

            // Assert
            runner.Received(1).GetTradeDetails(addTradeViewModel);
        }

        // Tests edit trade is called on the trade details view model with the selected trade being passed in
        [Gwt("Given a main window view model's trade manager has a selected trade",
            "when the edit trade command is executed",
            "the add trade view model's level entry is the same as the trade manager's selected trade's")]
        public void T1()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel, SubPlot);
            const double testEntry = 666.66;
            var testTrade = TestOpenTrade;
            testTrade.Levels.Entry = testEntry;
            viewModel.TradeManager.SelectedTrade = testTrade;

            // Act 
            viewModel.EditTradeCommand.Execute(null!);

            // Assert
            Assert.Equal(testEntry, addTradeViewModel.Levels.Entry);
        }
    }
}
