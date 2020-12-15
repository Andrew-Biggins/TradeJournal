using Common.MicroTests;
using TradeJournalCore.ViewModels;

namespace TradeJournalCore.MicroTests
{
    public sealed class GetNameViewModelTests
    {
        [Gwt("Given a get name view model",
            "when the confirm new name command is executed",
            "the name confirmed event is raised")]
        public void T0()
        {
            // Arrange
            var viewModel = new GetNameViewModel();
            var catcher = Catcher.Simple;
            viewModel.NameConfirmed += catcher.Catch;

            // Act
            viewModel.ConfirmNewNameCommand.Execute(null!);

            // Assert
            catcher.CaughtEmpty(viewModel);
        }
    }
}
