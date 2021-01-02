using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [Gwt("Given a collection of trades and a collection of asset classes",
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

        [Gwt("Given a collection of trades and a collection of markets",
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

        [Gwt("Given a collection of trades and a collection of strategies",
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

        [Gwt("Given a collection of trades and a collection of days",
            "when unselected day trades are removed",
            "only trades with selected days remain")]
        public void T3()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 2), 1), Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 3), 1), Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 4), 1), Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };
            var days = new List<ISelectable> { new Day(DayOfWeek.Saturday) };

            // Act 
            var actual = RemoveUnselectedDays(trades, days).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when trades outside of a date range are removed",
            "only trades within the date range remain")]
        public void T4()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 3), 1), Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 2), 1), Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 4), 1), Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveTradesOutsideDateRange(trades, new DateTime(2021, 1, 3), new DateTime(2021, 1, 3))
                .ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when trades outside of a time range are removed",
            "only trades within the time range remain")]
        public void T5()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 3, 11,01,00), 1), Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 2, 10, 59, 00), 1), Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, TestLevels,
                new Execution(1, new DateTime(2021, 1, 4, 12, 01,00), 1), Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveTradesOutsideTimeRange(trades, new DateTime(2021, 1, 3, 11, 00, 00),
                new DateTime(2021, 1, 3, 12, 00, 00)).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when trades outside of a risk reward ratio range are removed",
            "only trades within the risk reward ratio range remain")]
        public void T6()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy,
                new Levels(100, 50, 200), TestOpen, Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, new Levels(100, 50, 201), TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, new Levels(100, 50, 199), TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveTradesOutsideRiskRewardRatioRange(trades, 2, 2).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when closed trades are removed",
            "only open trades remain")]
        public void T7()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy,
                TestLevels, TestOpen, Option.Some(new Execution(1, DateTime.Today, 1)), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveUnselectedTradeStatuses(trades, TradeStatus.Closed).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when open trades are removed",
            "only closed trades remain")]
        public void T8()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy,
                TestLevels, TestOpen, Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
                Option.Some(new Execution(1, DateTime.Today, 1)), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
                Option.Some(new Execution(1, DateTime.Today, 1)), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveUnselectedTradeStatuses(trades, TradeStatus.Open).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when filtered with both trade status",
            "all trades remain")]
        public void T9()
        {
            // Arrange
            var testTradeOne = new Trade(TestMarket, TestStrategy,
                TestLevels, TestOpen, Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
                Option.Some(new Execution(1, DateTime.Today, 1)), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
                Option.Some(new Execution(1, DateTime.Today, 1)), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveUnselectedTradeStatuses(trades, TradeStatus.Both).ToList();

            // Assert
            Assert.Equal(3, actual.Count);
        }

        [Gwt("Given a collection of trades",
            "when closed trades are removed",
            "only open trades remain")]
        public void T10()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy,
                TestLevels, TestOpen, Option.Some(new Execution(1, DateTime.Today, 1)), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveUnselectedTradeStatuses(trades, TradeStatus.Closed).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when long trades are removed",
            "only short trades remain")]
        public void T11()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy, new Levels(100, 200, 50),
                TestOpen, Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, new Levels(100, 50, 201), TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, new Levels(100, 50, 201), TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveUnselectedTradeDirections(trades, TradeDirection.Short).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when short trades are removed",
            "only long trades remain")]
        public void T12()
        {
            // Arrange
            const string testMarketName = "Silver";
            var testTradeOne = new Trade(new Market(testMarketName, AssetClass.Commodities), TestStrategy, new Levels(100, 50, 100),
                TestOpen, Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, new Levels(100, 200, 50), TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, new Levels(100, 200, 50), TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveUnselectedTradeDirections(trades, TradeDirection.Long).ToList();

            // Assert
            Assert.Single((IEnumerable)actual);
            Assert.Equal(testMarketName, actual[0].Market.Name);
        }

        [Gwt("Given a collection of trades",
            "when filtered with both trade directions",
            "all trades remain")]
        public void T14()
        {
            // Arrange
            var testTradeOne = new Trade(TestMarket, TestStrategy, new Levels(100, 50, 100),
                TestOpen, Option.None<Execution>(), TestEmptyExcursions);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, new Levels(100, 200, 50), TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);
            var testTradeThree = new Trade(TestMarket, TestStrategy, new Levels(100, 200, 50), TestOpen,
                Option.None<Execution>(), TestEmptyExcursions);

            var trades = new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree };

            // Act 
            var actual = RemoveUnselectedTradeDirections(trades, TradeDirection.Both).ToList();

            // Assert
            Assert.Equal(3, actual.Count);
        }

        [Gwt("Given a trade filterer",
            "when filtered for direction",
            "an observable collection is returned")]
        public void T15()
        {
            // Act 
            var actual = RemoveUnselectedTradeDirections(new List<ITrade>(), TradeDirection.Both);

            // Assert
            Assert.IsType<ObservableCollection<ITrade>>(actual);
        }
    }
}
