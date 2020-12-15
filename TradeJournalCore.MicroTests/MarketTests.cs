using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class MarketTests
    {
        [Gwt("Given a market",
            "when the name is read",
            "it is the same as the one given at construction")]
        public void T0()
        {
            // Arrange
            const string testName = "test name";
            var market = new Market(testName, AssetClass.Commodities);

            // Act 
            var actual = market.Name;

            // Assert
            Assert.Equal(testName, actual);
        }

        [Gwt("Given a market",
            "when the asset class is read",
            "it is the same as the one given at construction")]
        public void T1()
        {
            // Arrange
            var market = new Market(string.Empty, AssetClass.Shares);

            // Act 
            var actual = market.AssetClass;

            // Assert
            Assert.Equal(AssetClass.Shares, actual);
        }

        [Gwt("Given a market",
            "when the is selected flag is read",
            "it is false by default")]
        public void T2()
        {
            // Arrange
            var market = new Market(string.Empty, AssetClass.Shares);

            // Act 
            var actual = market.IsSelected;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a market with a false is selected flag",
            "when the is selected flag is set to false again",
            "the is selected flag is true")]
        public void T3()
        {
            // Arrange
            var market = new Market(string.Empty, AssetClass.Shares);

            // Act 
            market.IsSelected = false;

            // Assert
            Assert.True(market.IsSelected);
        }

        [Gwt("Given a market with a true is selected flag",
            "when the is selected flag is set to false ",
            "the is selected flag is false")]
        public void T4()
        {
            // Arrange
            var market = new Market(string.Empty, AssetClass.Shares);
            market.IsSelected = true;

            // Act 
            market.IsSelected = false;

            // Assert
            Assert.False(market.IsSelected);
        }
    }
}
