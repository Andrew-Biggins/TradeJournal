using Common;
using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class RemoveTradeTests
    {
        [Gwt("Given a main window view model",
            "when the remove trade command is executed",
            "the runner asks for confirmation")]
        public void T0()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel);

            // Act 
            viewModel.RemoveTradeCommand.Execute(null!);

            // Assert
            runner.Received(1).RunForResult(viewModel, Arg.Any<Message>());
        }

        [Gwt("Given a main window view model",
            "when the remove trade command is executed and confirmed",
            "the trade manager is told to remove a trade")]
        public void T1()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel);
            runner.RunForResult(viewModel, Arg.Is<Message>(m => m.Is(Messages.ConfirmRemoveTrade))).Returns(true);

            // Act 
            viewModel.RemoveTradeCommand.Execute(null!);

            // Assert
            viewModel.TradeManager.Received(1).RemoveTrade();
        }

        [Gwt("Given a main window view model",
            "when the remove trade command is executed and not confirmed",
            "the trade manager is not told to remove a trade")]
        public void T2()
        {
            // Arrange
            var runner = SubRunner;
            var addTradeViewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(runner, SubTradeManager, addTradeViewModel);
            runner.RunForResult(viewModel, Arg.Is<Message>(m => m.Is(Messages.ConfirmRemoveTrade))).Returns(false);

            // Act 
            viewModel.RemoveTradeCommand.Execute(null!);

            // Assert
            viewModel.TradeManager.DidNotReceive().RemoveTrade();
        }
    }
}
