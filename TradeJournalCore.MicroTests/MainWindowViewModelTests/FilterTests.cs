using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class FilterTests
    {
        [Gwt("Given a main window view model",
            "when the trade filterer's apply filters command is executed",
            "the trade manager is told to filter trades and passed the trade filterer view model")]
        public void T0()
        {
            // Arrange
            var tradeManager = SubTradeManager;
            var viewModel = new MainWindowViewModel(SubRunner, tradeManager, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()));

            // Act 
            viewModel.TradeFiltererViewModel.ApplyTradeFiltersCommand.Execute(null!);

            // Assert
            tradeManager.Received(1).FilterTrades(viewModel.TradeFiltererViewModel);
        }
    }
}
