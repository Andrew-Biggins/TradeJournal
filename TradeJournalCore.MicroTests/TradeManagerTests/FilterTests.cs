using Common.MicroTests;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeManagerTests
{
    public sealed class FilterTests
    {
        [Gwt("Given a trade manager",
            "when told to filter trades",
            "the filters are the ones passed in")]
        public void T0()
        {
            // Arrange
            var tradeManager = new TradeManager();
            var filters = TestFilters;

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Equal(filters, tradeManager.Filters);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "property changed is raised for the trades collection")]
        public void T2()
        {
            // Arrange
            var tradeManager = new TradeManager();
            var catcher = Catcher.For(tradeManager);

            // Act 
            tradeManager.FilterTrades(TestFilters);

            // Assert
            catcher.CaughtPropertyChanged(tradeManager, nameof(tradeManager.Trades));
        }

        

        [Gwt("Given a trade manager",
            "when a trades are removed",
            "property changed is raised for the trades collection")]
        public void T12()
        {
            // Arrange
            var tradeManager = new TradeManager();
            var catcher = Catcher.For(tradeManager);

            // Act 
            tradeManager.FilterTrades(TestFilters);

            // Assert
            catcher.CaughtPropertyChanged(tradeManager, nameof(tradeManager.Trades));
        }
    }
}
