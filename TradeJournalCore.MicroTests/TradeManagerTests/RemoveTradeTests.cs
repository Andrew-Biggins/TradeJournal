using Common.MicroTests;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeManagerTests
{
    public sealed class RemoveTradeTests
    {
        [Gwt("Given a trade manager",
            "when a trade is remove",
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
    }
}
