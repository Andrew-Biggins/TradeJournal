using Common.MicroTests;
using System;
using System.Linq;
using TradeJournalCore.ViewModels;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class AddMarketViewModelTests
    {
        [Gwt("Given an add trade view model",
            "when the asset class collection is read",
            "it contains all the asset classes")]
        public void T0()
        {
            // Arrange
            var viewModel = new AddMarketViewModel();
            var assetClasses = (AssetClass[])Enum.GetValues(typeof(AssetClass));
            var expected = assetClasses.ToList();

            // Act
            var actual = viewModel.AssetClasses;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Gwt("Given an add trade view model",
            "when the confirm market command is executed",
            "the market confirmed event is raised")]
        public void T1()
        {
            // Arrange
            var viewModel = new AddMarketViewModel();
            var catcher = Catcher.Simple;
            viewModel.MarketConfirmed += catcher.Catch;

            // Act
            viewModel.ConfirmMarketCommand.Execute(null!);

            // Assert
            catcher.CaughtEmpty(viewModel);
        }
    }
}
