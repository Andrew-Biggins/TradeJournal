using System;
using System.Collections.Generic;
using Common.MicroTests;
using Common.Optional;
using TradeJournalCore.Interfaces;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;
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
            testTradeOneDetails.SelectedMarket = new Market("Gold", AssetClass.Commodities, PipDivisor.One);
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedMarket = new Market("USDJPY", AssetClass.Currencies, PipDivisor.One);
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var assetClasses = new List<ISelectable> { new AssetType(AssetClass.Commodities)};
            var filters = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), assetClasses, GetDays(),
                DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999, TradeStatus.Both,
                TradeDirection.Both, EntryOrderType.Both);

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
            testTradeOneDetails.SelectedMarket = new Market("Gold", AssetClass.Commodities, PipDivisor.One);
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedMarket = new Market("USDJPY", AssetClass.Currencies, PipDivisor.One);
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var markets = new List<ISelectable> { new Market("USDJPY", AssetClass.Currencies, PipDivisor.One) };
            var filters = new Filters(markets, GetDefaultStrategies(), GetAssetTypes(), GetDays(),
                DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999, TradeStatus.Both,
                TradeDirection.Both, EntryOrderType.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("USDJPY", tradeManager.Trades[0].Market.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades with unselected strategies are removed from the trades collection")]
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
                DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999, TradeStatus.Both,
                TradeDirection.Both, EntryOrderType.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Triangle", tradeManager.Trades[0].Strategy.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades with unselected days are removed from the trades collection")]
        public void T6()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedStrategy = new Strategy("Triangle");
            testTradeOneDetails.Open.DateTime = new DateTime(2021,1, 2);
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedStrategy = new Strategy("Gap fill");
            testTradeTwoDetails.Open.DateTime = new DateTime(2021, 1, 3);
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var days = new List<ISelectable> { new Day(DayOfWeek.Saturday) };
            var filters = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), GetAssetTypes(), days,
                DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 999, TradeStatus.Both,
                TradeDirection.Both, EntryOrderType.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Triangle", tradeManager.Trades[0].Strategy.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades outside the date range are removed from the trades collection")]
        public void T7()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedStrategy = new Strategy("Triangle");
            testTradeOneDetails.Open.DateTime = new DateTime(2021, 1, 2);
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedStrategy = new Strategy("Gap fill");
            testTradeTwoDetails.Open.DateTime = new DateTime(2021, 1, 3);
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var days = new List<ISelectable> { new Day(DayOfWeek.Saturday) };
            var filters = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), GetAssetTypes(), days,
                new DateTime(2021, 1, 2), new DateTime(2021, 1, 2), DateTime.MinValue, DateTime.MaxValue, 
                0, 999, TradeStatus.Both, TradeDirection.Both, EntryOrderType.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Triangle", tradeManager.Trades[0].Strategy.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades outside the time range are removed from the trades collection")]
        public void T8()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedStrategy = new Strategy("Triangle");
            testTradeOneDetails.Open.DateTime = new DateTime(2021, 1, 2, 11, 00, 00);
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedStrategy = new Strategy("Gap fill");
            testTradeTwoDetails.Open.DateTime = new DateTime(2021, 1, 3, 12, 00,00);
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var days = new List<ISelectable> { new Day(DayOfWeek.Saturday) };
            var filters = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), GetAssetTypes(), days,
                DateTime.MinValue, DateTime.MaxValue, new DateTime(2021, 1, 2, 10, 00, 00),
                new DateTime(2021, 1, 2, 11, 30, 00),
                0, 999, TradeStatus.Both, TradeDirection.Both, EntryOrderType.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Triangle", tradeManager.Trades[0].Strategy.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades outside the risk reward ratio range are removed from the trades collection")]
        public void T9()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedStrategy = new Strategy("Triangle");
            testTradeOneDetails.Levels.Entry = 100;
            testTradeOneDetails.Levels.Stop = 50;
            testTradeOneDetails.Levels.Target = 200;
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedStrategy = new Strategy("Gap fill");
            testTradeTwoDetails.Levels.Entry = 100;
            testTradeTwoDetails.Levels.Stop = 50;
            testTradeTwoDetails.Levels.Target = 300;
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var filters = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), GetAssetTypes(), GetDays(),
                DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, 
                DateTime.MaxValue, 2, 2, TradeStatus.Both, TradeDirection.Both, EntryOrderType.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Triangle", tradeManager.Trades[0].Strategy.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades with unselected status are removed from the trades collection")]
        public void T10()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedStrategy = new Strategy("Triangle");
            testTradeOneDetails.CloseLevel = Option.Some(100.0);
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedStrategy = new Strategy("Gap fill");
            testTradeTwoDetails.CloseLevel = Option.None<double>();
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var filters = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), GetAssetTypes(), GetDays(),
                DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue,
                DateTime.MaxValue, 0, 999, TradeStatus.Closed, TradeDirection.Both, EntryOrderType.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Triangle", tradeManager.Trades[0].Strategy.Name);
        }

        [Gwt("Given a trade manager",
            "when told to filter trades",
            "trades with unselected directions are removed from the trades collection")]
        public void T11()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            var testTradeOneDetails = TestTradeDetailsViewModel;
            testTradeOneDetails.SelectedStrategy = new Strategy("Triangle");
            testTradeOneDetails.Levels.Entry = 100;
            testTradeOneDetails.Levels.Stop = 50;
            testTradeOneDetails.Levels.Target = 200;
            tradeManager.AddNewTrade(testTradeOneDetails);

            var testTradeTwoDetails = TestTradeDetailsViewModel;
            testTradeTwoDetails.SelectedStrategy = new Strategy("Gap fill");
            testTradeTwoDetails.Levels.Entry = 100;
            testTradeTwoDetails.Levels.Stop = 530;
            testTradeTwoDetails.Levels.Target = 50;
            tradeManager.AddNewTrade(testTradeTwoDetails);

            var filters = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), GetAssetTypes(), GetDays(),
                DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue,
                DateTime.MaxValue, 0, 999, TradeStatus.Both, TradeDirection.Long, EntryOrderType.Both);

            // Act 
            tradeManager.FilterTrades(filters);

            // Assert
            Assert.Single(tradeManager.Trades);
            Assert.Equal("Triangle", tradeManager.Trades[0].Strategy.Name);
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
