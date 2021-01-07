using Common;
using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.Shared;
using static TradeJournalCore.MicroTests.MainWindowViewModelTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class FilterTests
    {
        [Gwt("Given a main window view model",
            "when the markets selected changed event is raised",
            "the trade manager is told to filter trades")]
        public void T1()
        {
            // Arrange
            var tradeManager = SubTradeManager;
            var viewModel = new MainWindowViewModel(SubRunner, tradeManager, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()), SubPlot);

            // Act 
            viewModel.TradeFiltererViewModel.Markets.SelectedChanged.Raise(viewModel.TradeFiltererViewModel);

            // Assert
            tradeManager.ReceivedWithAnyArgs(1).FilterTrades(null!);
        }

        [Gwt("Given a main window view model",
            "when a new market is added",
            "the trade manager is told to filter trades")]
        public void T2()
        {
            // Arrange
            var tradeManager = SubTradeManager;
            var viewModel = new MainWindowViewModel(SubRunner, tradeManager, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()), SubPlot);

            // Act 
            viewModel.TradeFiltererViewModel.Markets.Add(TestMarket);

            // Assert
            tradeManager.ReceivedWithAnyArgs(1).FilterTrades(null!);
        }

        [Gwt("Given a main window view model",
            "when the strategies selected changed event is raised",
            "the trade manager is told to filter trades")]
        public void T3()
        {
            // Arrange
            var tradeManager = SubTradeManager;
            var viewModel = new MainWindowViewModel(SubRunner, tradeManager, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()), SubPlot);

            // Act 
            viewModel.TradeFiltererViewModel.Strategies.SelectedChanged.Raise(viewModel.TradeFiltererViewModel);

            // Assert
            tradeManager.ReceivedWithAnyArgs(1).FilterTrades(null!);
        }

        [Gwt("Given a main window view model",
            "when a new strategy is added",
            "the trade manager is told to filter trades")]
        public void T4()
        {
            // Arrange
            var tradeManager = SubTradeManager;
            var viewModel = new MainWindowViewModel(SubRunner, tradeManager, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()), SubPlot);

            // Act 
            viewModel.TradeFiltererViewModel.Strategies.Add(TestStrategy);

            // Assert
            tradeManager.ReceivedWithAnyArgs(1).FilterTrades(null!);
        }
    }
}
