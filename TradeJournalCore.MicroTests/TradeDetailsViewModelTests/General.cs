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
    }
}
