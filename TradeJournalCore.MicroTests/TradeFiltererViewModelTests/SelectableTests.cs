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
            Assert.True(AreSelectableCollectionsEqual(defaultMarkets, actual));
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
            Assert.True(AreSelectableCollectionsEqual(defaultStrategies, actual));
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
            Assert.True(AreSelectableCollectionsEqual(assetTypes, actual));
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
            Assert.True(AreSelectableCollectionsEqual(days, actual));
        }

        [Gwt("Given a trade filterer view model",
            "when the trade statuses are read",
            "the trade statuses are correct")]
        public void T4()
        {
            // Arrange
            var statuses = SelectableFactory.GetEnumList<TradeStatus>();
            var viewModel = new TradeFiltererViewModel();

            // Act
            var actual = viewModel.TradeStatuses;

            // Assert
            Assert.True(AreEnumCollectionsEqual(statuses, actual));
        }

        [Gwt("Given a trade filterer view model",
            "when the trade directions are read",
            "the trade directions are correct")]
        public void T5()
        {
            // Arrange
            var directions = SelectableFactory.GetEnumList<TradeDirection>();
            var viewModel = new TradeFiltererViewModel();

            // Act
            var actual = viewModel.TradeDirections;

            // Assert
            Assert.True(AreEnumCollectionsEqual(directions, actual));
        }

        private static bool AreSelectableCollectionsEqual(IReadOnlyList<ISelectable> x, IReadOnlyList<ISelectable> y)
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

        private static bool AreEnumCollectionsEqual<T>(IReadOnlyList<T> x, IReadOnlyList<T> y)
        {
            var areEqual = true;

            for (var i = 0; i < y.Count; i++)
            {
                if (x[i].ToString() != y[i].ToString())
                {
                    areEqual = false;
                }
            }

            return areEqual;
        }
    }
}
