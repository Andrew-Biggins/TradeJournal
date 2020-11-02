using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class AddTradeTests
    {
        [Gwt("Given a main window view model",
            "when the add trade command is executed",
            "the runner is told to get the trade details")]
        public void T0()
        {
            // Arrange
            var runner = SubRunner;
            var viewModel = new MainWindowViewModel(runner);

            // Act 
            viewModel.AddNewTradeCommand.Execute(null!);

            // Assert
            runner.ReceivedWithAnyArgs(1).GetTradeDetails(null!);
        }
    }
}
