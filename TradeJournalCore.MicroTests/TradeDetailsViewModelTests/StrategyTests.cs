using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeDetailsViewModelTests
{
    public sealed class StrategyTests
    {
        [Gwt("Given a trade details view model",
            "when the strategies collection is read",
            "the markets collection is empty by default")]
        public void T0()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel());

            // Act 
            var actual = viewModel.Strategies;

            // Assert
            Assert.Empty(actual);
        }

        [Gwt("Given a trade details view model",
            "when the command to add a new strategy is executed",
            "the runner is told to get the new strategy name with the correct title string")]
        public void T1()
        {
            // Arrange
            var runner = SubRunner;
            var getNameVm = new GetNameViewModel();
            var viewModel = new TradeDetailsViewModel(runner, getNameVm);

            // Act 
            viewModel.AddNewStrategyCommand.Execute(null!);

            // Assert
            runner.Received(1).GetNewName(getNameVm, "Enter Strategy Name");
        }

        [Gwt("Given a trade details view model whose get name view model has a valid name string",
            "when the command to add a new strategy is executed",
            "the get name view model name is the empty string")]
        public void T2()
        {
            // Arrange
            var getNameVm = new GetNameViewModel();
            getNameVm.Name = "test name";
            var viewModel = new TradeDetailsViewModel(SubRunner, getNameVm);

            // Act 
            viewModel.AddNewStrategyCommand.Execute(null!);

            // Assert
            Assert.Equal(string.Empty, getNameVm.Name);
        }

        // Also tests event subscription
        [Gwt("Given a trade details view model has the add new market command executed",
            "when the get name view model confirms the new strategy name",
            "the new strategy is added to the strategies collection with the correct name")]
        public void T3()
        {
            // Arrange
            var getNameVm = new GetNameViewModel();
            const string testName = "test name";
            var viewModel = new TradeDetailsViewModel(SubRunner, getNameVm);
            viewModel.AddNewStrategyCommand.Execute(null!);
            getNameVm.Name = testName;

            // Act 
            getNameVm.ConfirmNewNameCommand.Execute(null!);

            // Assert
            Assert.Equal(testName, viewModel.Strategies[^1].Name);
        }

        [Gwt("Given a trade details view model has the add new market command executed",
            "when the get name view model confirms the new market name",
            "the selected market is the new market")]
        public void T4()
        {
            // Arrange
            var getNameVm = new GetNameViewModel();
            const string testName = "test name";
            var viewModel = new TradeDetailsViewModel(SubRunner, getNameVm);
            viewModel.AddNewMarketCommand.Execute(null!);
            getNameVm.Name = testName;

            // Act 
            getNameVm.ConfirmNewNameCommand.Execute(null!);

            // Assert
            Assert.Equal(testName, viewModel.SelectedMarket.Name);
        }

        [Gwt("Given a trade details view model has the add new strategy command executed",
            "when the get name view model confirms the new strategy name",
            "property changed is raised for selected strategy")]
        public void T5()
        {
            // Arrange
            var getNameVm = new GetNameViewModel();
            var viewModel = new TradeDetailsViewModel(SubRunner, getNameVm);
            var catcher = Catcher.For(viewModel);
            viewModel.AddNewStrategyCommand.Execute(null!);

            // Act 
            getNameVm.ConfirmNewNameCommand.Execute(null!);

            // Assert
            catcher.CaughtPropertyChanged(viewModel, nameof(viewModel.SelectedStrategy));
        }

        // Tests event unsubscription
        [Gwt("Given a trade details view model has the add new strategy command executed",
            "when the get name view model confirms another new strategy name",
            "a new strategy is not added to the collection")]
        public void T6()
        {
            // Arrange
            var getNameVm = new GetNameViewModel();
            var viewModel = new TradeDetailsViewModel(SubRunner, getNameVm);
            viewModel.AddNewStrategyCommand.Execute(null!);
            getNameVm.ConfirmNewNameCommand.Execute(null!);

            // Act 
            getNameVm.ConfirmNewNameCommand.Execute(null!);

            // Assert
            Assert.Single(viewModel.Strategies);
        }
    }
}
