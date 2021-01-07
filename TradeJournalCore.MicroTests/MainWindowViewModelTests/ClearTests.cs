using Common;
using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.Shared;
using static TradeJournalCore.MicroTests.MainWindowViewModelTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class ClearTests
    {
        [Gwt("Given a main window view model",
            "when the clear trades command is executed",
            "the runner asks for confirmation")]
        public void T0()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel, SubPlot);

            // Act 
            viewModel.ClearCommand.Execute(null!);

            // Assert
            runner.Received(1).RunForResult(viewModel, Arg.Any<Message>());
        }

        [Gwt("Given a main window view model",
            "when the clear trades command is executed and confirmed",
            "the trade manager is told to clear all trades")]
        public void T1()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel, SubPlot);
            runner.RunForResult(viewModel, Arg.Is<Message>(m => m.Is(Messages.ConfirmClearAllTrades))).Returns(true);

            // Act 
            viewModel.ClearCommand.Execute(null!);

            // Assert
            viewModel.TradeManager.Received(1).ClearAll();
        }
    }
}
