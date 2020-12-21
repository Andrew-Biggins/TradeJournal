using Common.MicroTests;
using System.Collections.Generic;
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
    }
}
