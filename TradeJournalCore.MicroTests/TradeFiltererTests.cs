using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.MicroTests;
using Common.Optional;
using TradeJournalCore.Interfaces;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;
using static TradeJournalCore.TradeFilterer;

namespace TradeJournalCore.MicroTests
{
    public sealed class TradeFiltererTests
    {
        [Gwt("Given collection of trades and a collection of asset classes",
            "when unselected asset class trades are removed",
            "only trades with selected asset classes remain")]
        public void T0()
        {
            // Arrange
            var testTradeOne = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.None<Execution>(),
                TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.None<Execution>(),
                TestEmptyExcursions);

            const string testMarketName = "USDJPY";
            var testTradeThree = new Trade(new Market(testMarketName, AssetClass.Currencies), TestStrategy, TestLevels,
                TestOpen, Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> {testTradeOne, testTradeTwo, testTradeThree};
            var assetClasses = new List<ISelectable> {new AssetType(AssetClass.Currencies)};

            // Act 
            var actual = RemoveUnselectedAssetClasses(trades, assetClasses).ToList();

            // Assert
            Assert.Single((IEnumerable) actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given collection of trades and a collection of markets",
            "when unselected market trades are removed",
            "only trades with selected markets remain")]
        public void T1()
        {
            // Arrange
            var testTradeOne = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.None<Execution>(),
                TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.None<Execution>(),
                TestEmptyExcursions);

            const string testMarketName = "USDJPY";
            var testTradeThree = new Trade(new Market(testMarketName, AssetClass.Currencies), TestStrategy, TestLevels,
                TestOpen, Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };
            var markets = new List<ISelectable> { new Market("USDJPY", AssetClass.Currencies) };

            // Act 
            var actual = RemoveUnselectedMarkets(trades, markets).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given collection of trades and a collection of strategies",
            "when unselected strategy trades are removed",
            "only trades with selected strategies remain")]
        public void T2()
        {
            // Arrange
            var testTradeOne = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.None<Execution>(),
                TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.None<Execution>(),
                TestEmptyExcursions);

            const string testStrategyName = "Gap fill";
            var testTradeThree = new Trade(TestMarket, new Strategy(testStrategyName), TestLevels,
                TestOpen, Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };
            var strategies = new List<ISelectable> { new Strategy("Gap fill") };

            // Act 
            var actual = RemoveUnselectedStrategies(trades, strategies).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testStrategyName, actual[0].Strategy.Name);
        }
    }
}
