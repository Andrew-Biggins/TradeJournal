using Common.MicroTests;
using System;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeDetailsViewModelTests
{
    public sealed class General
    {
        [Gwt("Given a null runner",
            "when created",
            "an exception is thrown")]
        public void T0()
        {
            Assert.Throws<ArgumentNullException>("runner",
                () => new TradeDetailsViewModel(null!, new GetNameViewModel()));
        }

        [Gwt("Given a null get name view model",
            "when created",
            "an exception is thrown")]
        public void T1()
        {
            Assert.Throws<ArgumentNullException>("getNameViewModel",
                () => new TradeDetailsViewModel(SubRunner, null!));
        }

        [Gwt("Given a trade details view model",
            "when the confirm new trade command is executed",
            "the trade added event is raised")]
        public void T2()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel());
            var catcher = Catcher.Simple;
            viewModel.TradeAdded += catcher.Catch;

            // Act 
            viewModel.ConfirmNewTradeCommand.Execute(null!);

            // Assert
            catcher.CaughtEmpty(viewModel);
        }

        // Test the constructor call to verify dates
        [Gwt("Given a trade details view model",
            "when created",
            "the trade details validator valid trade flag is true")]
        public void T3()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel());

            // Assert
            Assert.True(viewModel.TradeDetailsValidator.IsTradeValid);
        }

        [Gwt("Given a trade details view model",
            "when the open date time is read",
            "the open date time is today by default")]
        public void T4()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel());

            //
            var actual = viewModel.Open.DateTime;

            // Assert
            Assert.Equal(DateTime.Today, actual);
        }

        [Gwt("Given a trade details view model",
            "when the close date time is read",
            "the open date time is today plus one minute by default")]
        public void T5()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel());

            //
            var actual = viewModel.CloseDateTime;

            // Assert
            Assert.Equal(DateTime.Today.AddMinutes(1), actual);
        }
    }
}
