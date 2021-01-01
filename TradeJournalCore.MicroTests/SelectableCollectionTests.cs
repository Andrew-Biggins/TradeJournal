using System;
using System.Collections.Generic;
using System.Text;
using Common.MicroTests;
using TradeJournalCore.Interfaces;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests
{
    public sealed class SelectableCollectionTests
    {
        [Gwt("Given a selectable collection",
            "when a selectable is added",
            "it is added to the collection")]
        public void T0()
        {
            // Arrange
            var collection = new SelectableCollection<ISelectable>();

            // Act 
            collection.AddSelectable(TestMarket);

            // Assert
            Assert.Single(collection);
        }

        [Gwt("Given a selectable collection containing an item",
            "when the item's selected changed event is raised",
            "the collection's selected changed event is raised")]
        public void T1()
        {
            // Arrange
            var collection = new SelectableCollection<ISelectable>();
            var testMarket = TestMarket;
            collection.AddSelectable(testMarket);
            var catcher = Catcher.Simple;
            collection.SelectedChanged += catcher.Catch;

            // Act 
            testMarket.IsSelected = false;

            // Assert
            catcher.CaughtEmpty(collection);
        }

        [Gwt("Given a selectable collection has a wrong type",
            "when an item is added",
            "it is not added to the collection")]
        public void T2()
        {
            // Arrange
            var collection = new SelectableCollection<double>();

            // Act 
            collection.AddSelectable(134.32);

            // Assert
            Assert.Empty(collection);
        }
    }
}
