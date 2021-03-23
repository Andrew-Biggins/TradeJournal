using System;
using Common.MicroTests;
using Common.Optional;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeDetailsViewModelTests
{
    public sealed class EditTradeTests
    {
        [Gwt("Given a trade details view model",
            "when a trade is edited",
            "the is editing flag is true")]
        public void T0()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());

            // Act
            viewModel.EditTrade(TestOpenTrade);

            // Assert
            Assert.True(viewModel.IsEditing);
        }

        [Gwt("Given a trade details view model",
            "when a trade is edited",
            "the selected market name is the name of the editing trade")]
        public void T1()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            var markets = SelectableFactory.GetDefaultMarkets();
            var expected = markets[1];
            viewModel.AddSelectables(markets, SelectableFactory.GetDefaultStrategies());
            var testTrade = new Trade(expected, TestStrategy, TestLevels, TestOpen, new Option.OptionNone<Execution>(),
                TestEmptyExcursions, EntryOrderType.Limit);

            // Act
            viewModel.EditTrade(testTrade);

            // Assert
            Assert.Equal(expected.Id, viewModel.SelectedMarket.Id);
        }

        [Gwt("Given a trade details view model",
            "when a trade is edited",
            "the selected strategy name is the name of the editing trade")]
        public void T2()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            var strategies = SelectableFactory.GetDefaultStrategies();
            var expected = strategies[1];
            viewModel.AddSelectables(SelectableFactory.GetDefaultMarkets(), strategies);
            var testTrade = new Trade(TestMarket, expected, TestLevels, TestOpen, new Option.OptionNone<Execution>(), 
                TestEmptyExcursions, EntryOrderType.Limit);

            // Act
            viewModel.EditTrade(testTrade);

            // Assert
            Assert.Equal(expected.Id, viewModel.SelectedStrategy.Id);
        }

        [Gwt("Given a trade details view model",
            "when a trade is edited",
            "the levels is the same as the editing trade")]
        public void T3()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            const double testEntry = 1234.56;
            var testTrade = TestOpenTrade;
            testTrade.Levels.Entry = testEntry;

            // Act
            viewModel.EditTrade(testTrade);

            // Assert
            Assert.Equal(testEntry, viewModel.Levels.Entry);
        }

        [Gwt("Given a trade details view model",
            "when a trade is edited",
            "the open is the same as the editing trade")]
        public void T4()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            const double testOpen = 1234.56;
            var testTrade = TestOpenTrade;
            testTrade.Open.Level = testOpen;

            // Act
            viewModel.EditTrade(testTrade);

            // Assert
            Assert.Equal(testOpen, viewModel.Open.Level);
        }

        [Gwt("Given a trade details view model",
            "when a trade is edited",
            "the close level is the same as the editing trade")]
        public void T5()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            var actual = 0.00;

            // Act
            viewModel.EditTrade(TestClosedTrade);

            // Assert
            viewModel.CloseLevel.IfExistsThen(x => { actual = x; });
            Assert.Equal(TestClose.Level, actual);
        }

        [Gwt("Given a trade details view model",
            "when a trade is edited",
            "the close date is the same as the editing trade")]
        public void T6()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());

            // Act
            viewModel.EditTrade(TestClosedTrade);

            // Assert
            Assert.Equal(TestClose.Date, viewModel.CloseDate);
        }

        [Gwt("Given a trade details view model with a close level",
            "when an open trade is edited",
            "the close level is option none")]
        public void T7()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            viewModel.SelectedMarket = TestMarket;
            viewModel.CloseLevel = Option.Some(666.66);

            // Act
            viewModel.EditTrade(TestOpenTrade);

            // Assert
            Assert.IsType<Option.OptionNone<double>>(viewModel.CloseLevel);
        }
    }
}
