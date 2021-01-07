using Common.MicroTests;
using Xunit;
using NSubstitute;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.Shared;
using static TradeJournalCore.MicroTests.MainWindowViewModelTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class AddTradeTests
    {
        [Gwt("Given a main window view model",
            "when the add trade command is executed",
            "the runner is passed the add trade view model and told to get the trade details")]
        public void T0()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel, SubPlot);

            // Act 
            viewModel.AddNewTradeCommand.Execute(null!);

            // Assert
            runner.Received(1).GetTradeDetails(addTradeViewModel);
        }

        // Also tests add trade event subscription
        [Gwt("Given a main window view model",
            "when the add trade view model confirms a new trade",
            "the trade manager is passed the add trade view model and told to add a new trade")]
        public void T1()
        {
            // Arrange
            var addTradeViewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager, addTradeViewModel, SubPlot);

            // Act 
            addTradeViewModel.ConfirmNewTradeCommand.Execute(null!);

            // Assert
            viewModel.TradeManager.Received(1).AddNewTrade(addTradeViewModel);
        }

        [Gwt("Given a main window view model's trade details view model has been editing",
            "when the add trade command is executed",
            "the trade details view model editing flag is false")]
        public void T2()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            addTradeViewModel.IsEditing = true;
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel, SubPlot);

            // Act 
            viewModel.AddNewTradeCommand.Execute(null!);

            // Assert
            Assert.False(addTradeViewModel.IsEditing);
        }
    }
}
