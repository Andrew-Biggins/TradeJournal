using System;
using Common.MicroTests;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeManagerTests
{
    public sealed class ClearTests
    {
        [Gwt("Given a trade manager has trades",
            "when the trades are cleared",
            "all trades are removed from the trades collection")]
        public void T0()
        {
            // Arrange
            var tradeManager = new TradeManager();
            tradeManager.Trades.Add(TestOpenTrade);
            tradeManager.Trades.Add(TestOpenTrade);
            tradeManager.Trades.Add(TestOpenTrade);

            // Act 
            tradeManager.ClearAll();

            // Assert
            Assert.Empty(tradeManager.Trades);
        }

        [Gwt("Given a trade manager",
            "when a trades are cleared",
            "property changed is raised for the trades collection")]
        public void T1()
        {
            // Arrange
            var tradeManager = new TradeManager();
            var catcher = Catcher.For(tradeManager);

            // Act 
            tradeManager.ClearAll();

            // Assert
            catcher.CaughtPropertyChanged(tradeManager, nameof(tradeManager.Trades));
        }

        // Tests the unfiltered list is also cleared
        [Gwt("Given a trade manager has trades which are cleared",
            "when the trades are filtered",
            "all trades are removed from the trades collection")]
        public void T2()
        {
            // Arrange
            var tradeManager = new TradeManager();
            tradeManager.AddNewTrade(TestTradeDetailsViewModel);
            tradeManager.AddNewTrade(TestTradeDetailsViewModel);
            tradeManager.AddNewTrade(TestTradeDetailsViewModel);
            tradeManager.ClearAll();

            // Act 
            tradeManager.FilterTrades(TestFilters);

            // Assert
            Assert.Empty(tradeManager.Trades);
        }

        [Gwt("Given a trade manager has trades which are cleared",
            "when the trades are filtered",
            "all trades are removed from the trades collection")]
        public void T3()
        {
            // Arrange
            var tradeManager = new TradeManager();
            tradeManager.AddNewTrade(TestTradeDetailsViewModel);
            tradeManager.AddNewTrade(TestTradeDetailsViewModel);
            tradeManager.AddNewTrade(TestTradeDetailsViewModel);
            tradeManager.ClearAll();

            // Act 
            var actual = tradeManager.GetDateRange();

            // Assert
            Assert.Equal((DateTime.MaxValue, DateTime.MinValue), actual);
        }
    }
}
