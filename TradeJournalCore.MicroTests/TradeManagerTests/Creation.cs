using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeManagerTests
{
    public class Creation
    {
        [Gwt("Given a trade manager",
            "when the trade list is read",
            "the trade list is empty by default")]
        public void T0()
        {
            // Arrange
            var tradeManager = new TradeManager();

            // Act 
            var actual = tradeManager.Trades;

            // Assert
            Assert.Empty(actual);
        }
    }
}