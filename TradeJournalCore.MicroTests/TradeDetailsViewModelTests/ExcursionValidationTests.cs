using System;
using Common.MicroTests;
using Common.Optional;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeDetailsViewModelTests
{
    public sealed class ExcursionValidationTests
    {
        [Gwt("Given a trade details view model",
            "when the is MAE fixed property is read",
            "it is false by default")]
        public void T0()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());

            // Act 
            var actual = viewModel.IsMaeFixed;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details view model with open long trade details",
            "when the close level is set to make the trade a loser",
            "the maximum adverse excursion is set to the difference between the open and close")]
        public void T1()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) {SelectedMarket = TestMarket};
            const double testOpen = 150;
            const double testClose = 100;
            viewModel.Levels.Stop = 100;
            viewModel.Levels.Target = 200; 
            viewModel.Open.Level = testOpen;
            var actual = 0.00;

            // Act 
            viewModel.CloseLevel = Option.Some(testClose);

            // Assert
            viewModel.MaxAdverse.IfExistsThen(x => { actual = x; });
            Assert.Equal(testOpen - testClose, actual);
        }

        [Gwt("Given a trade details view model open long trade details",
            "when the close level is set to make the trade a loser",
            "the mae is fixed property is true")]
        public void T2()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Levels.Stop = 100;
            viewModel.Levels.Target = 200;
            viewModel.Open.Level = 150;

            // Act 
            viewModel.CloseLevel = Option.Some(100.00);

            // Assert
            Assert.True(viewModel.IsMaeFixed);
        }

        [Gwt("Given a trade details view model has losing long details and a fixed MAE",
            "when the close level is updated to make the trade a winner",
            "the mae is fixed property is false")]
        public void T3()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Levels.Stop = 100;
            viewModel.Levels.Target = 200;
            viewModel.Open.Level = 150;
            viewModel.CloseLevel = Option.Some(100.0);

            // Act 
            viewModel.CloseLevel = Option.Some(300.0);

            // Assert
            Assert.False(viewModel.IsMaeFixed);
        }

        [Gwt("Given a trade details view model with open short trade details",
            "when the close level is set to make the trade a loser",
            "the maximum adverse excursion is set to the difference between the open and close")]
        public void T4()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            const double testOpen = 150;
            const double testClose = 200;
            viewModel.Levels.Stop = 200;
            viewModel.Levels.Target = 100;
            viewModel.Open.Level = testOpen;
            var actual = 0.00;

            // Act 
            viewModel.CloseLevel = Option.Some(testClose);

            // Assert
            viewModel.MaxAdverse.IfExistsThen(x => {actual = x; });
            Assert.Equal(testClose - testOpen, actual);
        }

        [Gwt("Given a trade details view model open short trade details",
            "when the close level is set to make the trade a loser",
            "the mae is fixed property is true")]
        public void T5()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Levels.Stop = 200;
            viewModel.Levels.Target = 100;
            viewModel.Open.Level = 150;

            // Act 
            viewModel.CloseLevel = Option.Some(200.00);

            // Assert
            Assert.True(viewModel.IsMaeFixed);
        }

        [Gwt("Given a trade details view model has losing short details and a fixed MAE",
            "when the close level is updated to make the trade a winner",
            "the mae is fixed property is false")]
        public void T6()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Levels.Stop = 200;
            viewModel.Levels.Target = 100;
            viewModel.Open.Level = 150;
            viewModel.CloseLevel = Option.Some(200.0);

            // Act 
            viewModel.CloseLevel = Option.Some(100.0);

            // Assert
            Assert.False(viewModel.IsMaeFixed);
        }

        [Gwt("Given a trade details view model has losing long details and a fixed MAE",
            "when the close level is updated to option none",
            "the mae is fixed property is false")]
        public void T7()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Levels.Stop = 100;
            viewModel.Levels.Target = 200;
            viewModel.Open.Level = 150;
            viewModel.CloseLevel = Option.Some(100.0);

            // Act 
            viewModel.CloseLevel = Option.None<double>();

            // Assert
            Assert.False(viewModel.IsMaeFixed);
        }

        [Gwt("Given a trade details view model has losing long details and a fixed MAE",
            "when the close level is updated to option none",
            "the mae is option none")]
        public void T8()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Levels.Stop = 100;
            viewModel.Levels.Target = 200;
            viewModel.Open.Level = 150;
            viewModel.CloseLevel = Option.Some(100.0);

            // Act 
            viewModel.CloseLevel = Option.None<double>();

            // Assert
            Assert.IsType<Option.OptionNone<double>>(viewModel.MaxAdverse);
        }

        // Also tests subscription to open changed event
        [Gwt("Given a trade details view model has details",
            "when the open level is updated",
            "the trade details validator's excursion limits are correct")]
        public void T9()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Levels.Stop = 200;
            viewModel.Levels.Target = 100;
            viewModel.Open.Level = 150;
            viewModel.CloseLevel = Option.Some(200.0);

            // Act 
            viewModel.Open.Level = 100;

            // Assert
            Assert.Equal(100, viewModel.TradeDetailsValidator.MaximumMae);
        }

        // Also tests subscription to open date changed event
        [Gwt("Given a trade details view model has details",
            "when the open date time is updated to an invalid date",
            "the trade details validator's has a dates error")]
        public void T10()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Open.DateTime = new DateTime(2020, 11, 11, 12, 00, 00);
            viewModel.CloseDateTime = new DateTime(2020, 11, 12, 12, 00, 00);

            // Act 
            viewModel.Open.DateTime = new DateTime(2020, 11,13, 12,00,00);

            // Assert
            Assert.True(viewModel.TradeDetailsValidator.DatesHaveError);
        }

        // Tests subscription to levels changed event
        [Gwt("Given a trade details view model has details",
            "when the levels are updated",
            "the trade details validator's excursion limits are correct")]
        public void T11()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Levels.Stop = 200;
            viewModel.Levels.Target = 100;
            viewModel.Open.Level = 150;
            viewModel.CloseLevel = Option.Some(200.0);

            // Act 
            viewModel.Levels.Target = 300;

            // Assert
            Assert.Equal(150, viewModel.TradeDetailsValidator.MaximumMae);
        }

        [Gwt("Given a trade details view model has details",
            "when the close date time is updated to an invalid date",
            "the trade details validator's has a dates error")]
        public void T12()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());
            viewModel.Open.DateTime = new DateTime(2020, 11, 11, 12, 00, 00);
            viewModel.CloseDateTime = new DateTime(2020, 11, 12, 12, 00, 00);

            // Act 
            viewModel.CloseDateTime = new DateTime(2020, 11, 10, 12, 00, 00);

            // Assert
            Assert.True(viewModel.TradeDetailsValidator.DatesHaveError);
        }

        [Gwt("Given a trade details view model validator has a maximum MAE",
            "when MAE is updated to a value above the maximum",
            "the trade details validator's has an MAE error")]
        public void T13()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Open.Level = 150;
            viewModel.CloseLevel = Option.Some(100.0);

            // Act 
            viewModel.MaxAdverse = Option.Some(100.0);

            // Assert
            Assert.True(viewModel.TradeDetailsValidator.MaeHasError);
        }

        [Gwt("Given a trade details view model validator has a minimum MFE",
            "when MFE is updated to a value below the minimum",
            "the trade details validator's has an MFE error")]
        public void T14()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel()) { SelectedMarket = TestMarket };
            viewModel.Open.Level = 150;
            viewModel.CloseLevel = Option.Some(200.0);

            // Act 
            viewModel.MaxFavourable = Option.Some(5.0);

            // Assert
            Assert.True(viewModel.TradeDetailsValidator.MfeHasError);
        }
    }
}
