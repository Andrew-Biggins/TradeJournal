using Common.MicroTests;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;
using static TradeJournalCore.MicroTests.MainWindowViewModelTests.Shared;
using NSubstitute;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class AccountStartSizeTests
    {
        [Gwt("Given a main window view model",
            "when the account start size is read",
            "then the account start size is 10000 by default")]
        public void T0()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager,
                new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()), SubPlot);

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
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()), SubPlot);

            // Act 
            viewModel.AccountStartSize = 500;

            // Assert
            Assert.Equal(500, viewModel.AccountStartSize);
        }

        [Gwt("Given a main window view model",
            "when the account start size is set",
            "the plot is told to update data")]
        public void T2()
        {
            // Arrange
            var subPlot = Substitute.For<ITradePlot>();
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager,
                new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), 
                    new AddMarketViewModel()), subPlot);

            // Act 
            viewModel.AccountStartSize = 500;

            // Assert
            subPlot.Received(1).UpdateData(viewModel.AccountStartSize, viewModel.TradeManager.Trades);
        }
    }
}
