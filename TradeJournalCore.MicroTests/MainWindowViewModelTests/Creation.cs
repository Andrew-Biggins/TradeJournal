using Common.MicroTests;
using System;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class Creation
    {
        [Gwt("Given a main window view model",
            "when the markets list is read",
            "the markets list empty by default")]
        public void T0()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()));

            // Act 
            var actual = viewModel.Markets;

            // Assert
            Assert.Empty(actual);
        }

        [Gwt("Given a main window view model",
            "when the strategies list is read",
            "the strategies list empty by default")]
        public void T1()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(SubRunner, SubTradeManager, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()));

            // Act 
            var actual = viewModel.Strategies;

            // Assert
            Assert.Empty(actual);
        }

        [Gwt("Given a null runner",
            "when created",
            "an exception is thrown")]
        public void T2()
        {
            Assert.Throws<ArgumentNullException>("runner",
                () => new MainWindowViewModel(null!, new TradeManager(), new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel())));
        }

        [Gwt("Given a null trade manager",
            "when created",
            "an exception is thrown")]
        public void T3()
        {
            Assert.Throws<ArgumentNullException>("tradeManager",
                () => new MainWindowViewModel(SubRunner, null!, new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel())));
        }

        [Gwt("Given a null trade details view model",
            "when created",
            "an exception is thrown")]
        public void T4()
        {
            Assert.Throws<ArgumentNullException>("tradeDetailsViewModel",
                () => new MainWindowViewModel(SubRunner, new TradeManager(), null!));
        }
    }
}
