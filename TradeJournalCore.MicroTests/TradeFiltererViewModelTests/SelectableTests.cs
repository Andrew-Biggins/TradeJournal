using Common.MicroTests;
using System.Collections.Generic;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModels;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeFiltererViewModelTests
{
    public sealed class SelectableTests
    {
        [Gwt("Given a trade filterer view model",
            "when the markets are read",
            "the markets are the default markets")]
        public void T0()
        {
            // Arrange
            var defaultMarkets = SelectableFactory.GetDefaultMarkets();
            var viewModel = new TradeFiltererViewModel();

            // Act
            var actual = viewModel.Markets;

            // Assert
            Assert.True(AreCollectionsEqual(defaultMarkets, actual));
        }

        [Gwt("Given a trade filterer view model",
            "when the strategies are read",
            "the strategies are the default strategies")]
        public void T1()
        {
            // Arrange
            var defaultStrategies = SelectableFactory.GetDefaultStrategies();
            var viewModel = new TradeFiltererViewModel();

            // Act
            var actual = viewModel.Strategies;

            // Assert
            Assert.True(AreCollectionsEqual(defaultStrategies, actual));
        }

        [Gwt("Given a trade filterer view model",
            "when the asset types are read",
            "the asset types are correct")]
        public void T2()
        {
            // Arrange
            var assetTypes = SelectableFactory.GetAssetTypes();
            var viewModel = new TradeFiltererViewModel();

            // Act
            var actual = viewModel.AssetTypes;

            // Assert
            Assert.True(AreCollectionsEqual(assetTypes, actual));
        }

        [Gwt("Given a trade filterer view model",
            "when the days of week are read",
            "the days of week are correct")]
        public void T3()
        {
            // Arrange
            var days = SelectableFactory.GetDays();
            var viewModel = new TradeFiltererViewModel();

            // Act
            var actual = viewModel.DaysOfWeek;

            // Assert
            Assert.True(AreCollectionsEqual(days, actual));
        }

        private static bool AreCollectionsEqual(IReadOnlyList<ISelectable> x, IReadOnlyList<ISelectable> y)
        {
            var areEqual = true;

            for (var i = 0; i < y.Count; i++)
            {
                if (x[i].Name != y[i].Name)
                {
                    areEqual = false;
                }
            }

            return areEqual;
        }
    }
}
