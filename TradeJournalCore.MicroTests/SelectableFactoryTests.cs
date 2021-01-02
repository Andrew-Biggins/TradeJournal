using System;
using Common.MicroTests;
using System.Collections.Generic;
using NSubstitute;
using TradeJournalCore.Interfaces;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class SelectableFactoryTests
    {
        [Gwt("Given a selectable factory",
            "when an asset type collection is requested",
            "it contains 5 asset classes")]
        public void T0()
        {
            Assert.Equal(5, SelectableFactory.GetAssetTypes().Count);
        }

        [Gwt("Given a selectable factory",
            "when the asset type collection is retrieved",
            "all its elements are selected")]
        public void T1()
        {
            // Arrange
            var assetTypes = SelectableFactory.GetAssetTypes();

            // Act
            var actual = IsAllSelected(assetTypes);

            // Assert
            Assert.True(actual);
        }

        [Gwt("Given a selectable factory",
            "when a days collection is requested",
            "it contains 7 days")]
        public void T2()
        {
            Assert.Equal(7, SelectableFactory.GetDays().Count);
        }

        [Gwt("Given a selectable factory",
            "when a day collection is retrieved",
            "all its elements are selected")]
        public void T3()
        {
            // Arrange
            var days = SelectableFactory.GetDays();

            // Act
            var actual = IsAllSelected(days);

            // Assert
            Assert.True(actual);
        }

        [Gwt("Given a selectable factory",
            "when the default markets collection is retrieved",
            "all its elements are selected")]
        public void T4()
        {
            // Arrange
            var markets = SelectableFactory.GetDefaultMarkets();

            // Act
            var actual = IsAllSelected(markets);

            // Assert
            Assert.True(actual);
        }

        [Gwt("Given a selectable factory",
            "when the default strategies collection is retrieved",
            "all its elements are selected")]
        public void T5()
        {
            // Arrange
            var strategies = SelectableFactory.GetDefaultStrategies();

            // Act
            var actual = IsAllSelected(strategies);

            // Assert
            Assert.True(actual);
        }

        [Gwt("Given a selectable factory",
            "when the trade statuses collection is requested",
            "it contains 3 elements")]
        public void T6()
        {
            Assert.Equal(3, SelectableFactory.GetTradeStatuses().Count);
        }

        [Gwt("Given a selectable factory",
            "when the trade directions collection is requested",
            "it contains 3 elements")]
        public void T7()
        {
            Assert.Equal(3, SelectableFactory.GetTradeDirections().Count);
        }

        [Gwt("Given a selectable factory",
            "when the trade statuses collection is requested",
            "the collection contains the correct type")]
        public void T8()
        {
            // Arrange
            var statuses = SelectableFactory.GetTradeStatuses();

            // Act
            var actual = statuses[0];
            
            // Assert
            Assert.IsType<TradeStatus>(actual);
        }

        [Gwt("Given a selectable factory",
            "when the trade directions collection is requested",
            "the collection contains the correct type")]
        public void T9()
        {
            // Arrange
            var directions = SelectableFactory.GetTradeDirections();

            // Act
            var actual = directions[0];

            // Assert
            Assert.IsType<TradeDirection>(actual);
        }

        // Tests the markets are added to the collection as selectables
        [Gwt("Given a markets collection from the selectable factory",
            "when is selected is toggled on all items",
            "the collection's selected changed event is raised for all items")]
        public void T10()
        {
            // Arrange
            var markets = SelectableFactory.GetDefaultMarkets();
            var catcher = Catcher.Simple;
            markets.SelectedChanged += catcher.Catch;

            // Act
            ToggleAllSelectables(markets);

            // Assert
            catcher.Received(markets.Count).Catch(markets, EventArgs.Empty);
        }

        // Tests the strategies are added to the collection as selectables
        [Gwt("Given a strategies collection from the selectable factory",
            "when is selected is toggled on all items",
            "the collection's selected changed event is raised for all items")]
        public void T11()
        {
            // Arrange
            var strategies = SelectableFactory.GetDefaultStrategies();
            var catcher = Catcher.Simple;
            strategies.SelectedChanged += catcher.Catch;

            // Act
            ToggleAllSelectables(strategies);

            // Assert
            catcher.Received(strategies.Count).Catch(strategies, EventArgs.Empty);
        }

        // Tests the asset types are added to the collection as selectables
        [Gwt("Given a asset types collection from the selectable factory",
            "when is selected is toggled on all items",
            "the collection's selected changed event is raised for all items")]
        public void T12()
        {
            // Arrange
            var assetTypes = SelectableFactory.GetAssetTypes();
            var catcher = Catcher.Simple;
            assetTypes.SelectedChanged += catcher.Catch;

            // Act
            ToggleAllSelectables(assetTypes);

            // Assert
            catcher.Received(assetTypes.Count).Catch(assetTypes, EventArgs.Empty);
        }

        // Tests the days are added to the collection as selectables
        [Gwt("Given a days collection from the selectable factory",
            "when is selected is toggled on all items",
            "the collection's selected changed event is raised for all items")]
        public void T13()
        {
            // Arrange
            var days = SelectableFactory.GetDays();
            var catcher = Catcher.Simple;
            days.SelectedChanged += catcher.Catch;

            // Act
            ToggleAllSelectables(days);

            // Assert
            catcher.Received(days.Count).Catch(days, EventArgs.Empty);
        }

        private static bool IsAllSelected(IEnumerable<ISelectable> list)
        {
            var isAllSelected = true;

            foreach (var selectable in list)
            {
                if (!selectable.IsSelected)
                {
                    isAllSelected = false;
                }
            }

            return isAllSelected;
        }

        private static void ToggleAllSelectables(IEnumerable<ISelectable> list)
        {
            foreach (var item in list)
            {
                item.IsSelected = false;
            }
        }
    }
}
