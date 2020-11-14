using Common.MicroTests;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class AccountStartSizeTests
    {
        [Gwt("Given a main window view model",
            "when the account start size is read",
            "the account start size is 10000 by default")]
        public void T0()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager, new AddTradeViewModel(SubRunner));

            // Act 
            var actual = viewModel.AccountStartSize;

            // Assert
            Assert.Equal(10000, actual);
        }

        [Gwt("Given a main window view model",
            "when the account start size is set",
            "the account start size is updated with the new value")]
        public void T1()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager, new AddTradeViewModel(SubRunner));

            // Act 
            viewModel.AccountStartSize = 500;

            // Assert
            Assert.Equal(500, viewModel.AccountStartSize);
        }
    }
}
