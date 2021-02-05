using Common.MicroTests;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeManagerTests
{
    public sealed class RemoveTradeTests
    {
        [Gwt("Given a trade manager",
            "when a trade is removed",
            "the selected trade is removed from the trades collection")]
        public void T0()
        {
            // Arrange
            var tradeManager = new TradeManager();
            var testTrade = TestOpenTrade;
            tradeManager.Trades.Add(testTrade);
            tradeManager.SelectedTrade = testTrade;

            // Act 
            tradeManager.RemoveTrade();

            // Assert
            Assert.Empty(tradeManager.Trades);
        }

        [Gwt("Given a trade manager",
            "when a trade is removed",
            "property changed is raised for the trades collection")]
        public void T1()
        {
            // Arrange
            var tradeManager = new TradeManager();
            tradeManager.SelectedTrade = TestOpenTrade;
            var catcher = Catcher.For(tradeManager);

            // Act 
            tradeManager.RemoveTrade();

            // Assert
            catcher.CaughtPropertyChanged(tradeManager, nameof(tradeManager.Trades));
        }
    }
}
