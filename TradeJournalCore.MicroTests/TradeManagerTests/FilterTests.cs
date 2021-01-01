using System;
using System.Collections.Generic;
using Common.MicroTests;
using TradeJournalCore.Interfaces;
using Xunit;
using static TradeJournalCore.MicroTests.TradeManagerTests.Shared;
using static TradeJournalCore.SelectableFactory;

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
            "when told to filter trades",
            "trades with unselected asset classes are removed from the trades collection")]
        public void T3()
        {
            // Arrange
            var tradeManager = new TradeManager {Filters = TestFilters};

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedMarket = new Market("Gold", AssetClass.Commodities);
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedMarket = new Market("USDJPY", AssetClass.Currencies);
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var assetClasses = new List<ISelectable> { new AssetType(AssetClass.Commodities)};
            var filters = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), assetClasses, GetDays(),
                DateTime.MaxValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999, TradeStatus.Both,
                TradeDirection.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Gold", tradeManager.Trades[0].Market.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades with unselected markets are removed from the trades collection")]
        public void T4()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedMarket = new Market("Gold", AssetClass.Commodities);
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedMarket = new Market("USDJPY", AssetClass.Currencies);
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var markets = new List<ISelectable> { new Market("USDJPY", AssetClass.Currencies) };
            var filters = new Filters(markets, GetDefaultStrategies(), GetAssetTypes(), GetDays(),
                DateTime.MaxValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999, TradeStatus.Both,
                TradeDirection.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("USDJPY", tradeManager.Trades[0].Market.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades with unselected markets are removed from the trades collection")]
        public void T5()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedStrategy = new Strategy("Triangle");
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedStrategy = new Strategy("Gap fill"); 
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var strategies = new List<ISelectable> { new Strategy("Triangle") };
            var filters = new Filters(GetDefaultMarkets(), strategies, GetAssetTypes(), GetDays(),
                DateTime.MaxValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999, TradeStatus.Both,
                TradeDirection.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Triangle", tradeManager.Trades[0].Strategy.Name);
        }
    }
}
