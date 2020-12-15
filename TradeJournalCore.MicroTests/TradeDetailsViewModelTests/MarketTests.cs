using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeDetailsViewModelTests
{
    public sealed class MarketTests
    {
        [Gwt("Given a trade details view model",
            "when the markets collection is read",
            "the markets collection is empty by default")]
        public void T0()
        {
            // Arrange
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), new AddMarketViewModel());

            // Act 
            var actual = viewModel.Markets;

            // Assert
            Assert.Empty(actual);
        }

        [Gwt("Given a trade details view model",
            "when the command to add a new market is executed",
            "the runner is told to get the new market with the correct title string")]
        public void T1()
        {
            // Arrange
            var runner = SubRunner;
            var addMarketViewModel = new AddMarketViewModel();
            var viewModel = new TradeDetailsViewModel(runner, new GetNameViewModel(), addMarketViewModel);

            // Act 
            viewModel.AddNewMarketCommand.Execute(null!);

            // Assert
            runner.Received(1).GetNewMarket(addMarketViewModel, "Enter Market Details");
        }

        [Gwt("Given a trade details view model whose get name view model has a valid name string",
            "when the command to add a new market is executed",
            "the add market view model name is the empty string")]
        public void T2()
        {
            // Arrange
            var addMarketViewModel = new AddMarketViewModel();
            addMarketViewModel.Name = "test name";
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), addMarketViewModel);

            // Act 
            viewModel.AddNewMarketCommand.Execute(null!);

            // Assert
            Assert.Equal(string.Empty, addMarketViewModel.Name);
        }

        // Also tests event subscription
        [Gwt("Given a trade details view model has the add new market command executed",
            "when the add market view model confirms the new market details",
            "the new market is added to the markets collection with the correct name")]
        public void T3()
        {
            // Arrange
            var addMarketViewModel = new AddMarketViewModel();
            const string testName = "test name";
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), addMarketViewModel);
            viewModel.AddNewMarketCommand.Execute(null!);
            addMarketViewModel.Name = testName;

            // Act 
            addMarketViewModel.ConfirmMarketCommand.Execute(null!);

            // Assert
            Assert.Equal(testName, viewModel.Markets[^1].Name);
        }

        [Gwt("Given a trade details view model has the add new market command executed",
            "when the add market view model confirms the new market name",
            "the selected market is the new market")]
        public void T4()
        {
            // Arrange
            var addMarketViewModel = new AddMarketViewModel();
            const string testName = "test name";
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), addMarketViewModel);
            viewModel.AddNewMarketCommand.Execute(null!);
            addMarketViewModel.Name = testName;

            // Act 
            addMarketViewModel.ConfirmMarketCommand.Execute(null!);

            // Assert
            Assert.Equal(testName, viewModel.SelectedMarket.Name);
        }

        [Gwt("Given a trade details view model has the add new market command executed",
            "when the add market view model confirms the new market name",
            "property changed is raised for selected market")]
        public void T5()
        {
            // Arrange
            var addMarketViewModel = new AddMarketViewModel();
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), addMarketViewModel);
            var catcher = Catcher.For(viewModel);
            viewModel.AddNewMarketCommand.Execute(null!);

            // Act 
            addMarketViewModel.ConfirmMarketCommand.Execute(null!);

            // Assert
            catcher.CaughtPropertyChanged(viewModel, nameof(viewModel.SelectedMarket));
        }

        // Tests event unsubscription
        [Gwt("Given a trade details view model has the add new market command executed",
            "when the add market view model confirms another new market name",
            "a new market is not added to the collection")]
        public void T6()
        {
            // Arrange
            var addMarketViewModel = new AddMarketViewModel();
            var viewModel = new TradeDetailsViewModel(SubRunner, new GetNameViewModel(), addMarketViewModel);
            viewModel.AddNewMarketCommand.Execute(null!);
            addMarketViewModel.ConfirmMarketCommand.Execute(null!);

            // Act 
            addMarketViewModel.ConfirmMarketCommand.Execute(null!);

            // Assert
            Assert.Single(viewModel.Markets);
        }
    }
}
