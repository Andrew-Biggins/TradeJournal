using Common.MicroTests;
using System;
using NSubstitute;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class Creation
    {
        [Gwt("Given a null runner",
            "when created",
            "an exception is thrown")]
        public void T0()
        {
            Assert.Throws<ArgumentNullException>("runner",
                () => new MainWindowViewModel(null!, new TradeManager(), new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel())));
        }

        [Gwt("Given a null trade manager",
            "when created",
            "an exception is thrown")]
        public void T1()
        {
            Assert.Throws<ArgumentNullException>("tradeManager",
                () => new MainWindowViewModel(SubRunner, null!, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel())));
        }

        [Gwt("Given a null trade details view model",
            "when created",
            "an exception is thrown")]
        public void T2()
        {
            Assert.Throws<ArgumentNullException>("tradeDetailsViewModel",
                () => new MainWindowViewModel(SubRunner, new TradeManager(), null!));
        }

        [Gwt("Given a main window view model",
            "when its add trade view model's markets is read",
            "it is the same as the filterer's markets")]
        public void T3()
        {
            // Arrange
            var addTradeViewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager, addTradeViewModel);

            // Act 
            var actual = addTradeViewModel.Markets;

            // Assert
            Assert.Equal(viewModel.TradeFilterer.Markets, actual);
        }

        [Gwt("Given a main window view model",
            "when its add trade view model's strategies is read",
            "it is the same as the filterer's strategies")]
        public void T4()
        {
            // Arrange
            var addTradeViewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager, addTradeViewModel);

            // Act 
            var actual = addTradeViewModel.Strategies;

            // Assert
            Assert.Equal(viewModel.TradeFilterer.Strategies, actual);
        }
    }
}
