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
            var market = new Market(testName, AssetClass.Commodities, PipDivisor.One);

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
            var market = new Market(string.Empty, AssetClass.Shares, PipDivisor.One);

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
            var market = new Market("test market", AssetClass.Indices, PipDivisor.One);

            // Act 
            var actual = market.IsSelected;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a market",
            "when the is selected flag is updated",
            "property changed is raised for is selected")]
        public void T3()
        {
            // Arrange
            var market = new Market(string.Empty, AssetClass.Shares, PipDivisor.One);
            var catcher = Catcher.For(market);

            // Act 
            market.IsSelected = true;

            // Assert
            catcher.CaughtPropertyChanged(market, nameof(market.IsSelected));
        }
    }
}
