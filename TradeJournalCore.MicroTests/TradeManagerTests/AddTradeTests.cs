﻿using Common.MicroTests;
using Common.Optional;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeManagerTests
{
    public sealed class AddTradeTests
    {
        [Gwt("Given a trade manager",
            "when a trade is added",
            "the trade is added to the trades collection")]
        public void T0()
        {
            // Arrange
            var tradeManager = new TradeManager {Filters = TestFilters};

            // Act 
            tradeManager.AddNewTrade(TestTradeDetailsViewModel);

            // Assert
            Assert.NotEmpty(tradeManager.Trades);
        }

        [Gwt("Given a trade manager",
            "when a trade is added",
            "the trade is added to the trades collection with the correct market")]
        public void T1()
        {
            // Arrange
            var tradeDetails = TestTradeDetailsViewModel;
            var testMarket = new Market("USDJPY", AssetClass.Currencies, PipDivisor.One);
            tradeDetails.SelectedMarket = testMarket;
            var tradeManager = new TradeManager { Filters = TestFilters };

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.Equal(testMarket, tradeManager.Trades[0].Market);
        }

        [Gwt("Given a trade manager",
            "when a trade is added",
            "the trade is added to the trades collection with the correct strategy")]
        public void T2()
        {
            // Arrange
            var tradeDetails = TestTradeDetailsViewModel;
            var testStrategy = new Strategy("Triangle");
            tradeDetails.SelectedStrategy = testStrategy;
            var tradeManager = new TradeManager{ Filters = TestFilters };

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.Equal(testStrategy, tradeManager.Trades[0].Strategy);
        }

        [Gwt("Given a trade manager",
            "when a trade is added",
            "the trade is added to the trades collection with the correct levels")]
        public void T3()
        {
            // Arrange
            var tradeDetails = TestTradeDetailsViewModel;
            const double testLevelEntry = 123.00;
            tradeDetails.Levels.Entry = testLevelEntry;
            var tradeManager = new TradeManager{ Filters = TestFilters };

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.Equal(testLevelEntry, tradeManager.Trades[0].Levels.Entry);
        }

        [Gwt("Given a trade manager",
            "when a trade is added",
            "the trade is added to the trades collection with the correct open")]
        public void T4()
        {
            // Arrange
            var tradeDetails = TestTradeDetailsViewModel;
            const double testOpenSize = 19.6;
            tradeDetails.Open.Size = testOpenSize;
            var tradeManager = new TradeManager{ Filters = TestFilters };

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.Equal(testOpenSize, tradeManager.Trades[0].Open.Size);
        }

        [Gwt("Given a trade manager",
            "when a trade is added with an option none close level",
            "the trade is added to the trades collection with an option none close execution")]
        public void T5()
        {
            // Arrange
            var tradeDetails = TestTradeDetailsViewModel;
            tradeDetails.CloseLevel = Option.None<double>();
            var tradeManager = new TradeManager{ Filters = TestFilters };

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.IsType<Option.OptionNone<Execution>>(tradeManager.Trades[0].Close);
        }

        [Gwt("Given a trade manager",
            "when a trade is added with an option some close level",
            "the trade is added to the trades collection with the correct option some execution level")]
        public void T6()
        {
            // Arrange
            var tradeDetails = TestTradeDetailsViewModel;
            const double testCloseLevel = 456.78;
            tradeDetails.CloseLevel = Option.Some(testCloseLevel);
            var tradeManager = new TradeManager{ Filters = TestFilters };
            var outValue = 1.00;

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            tradeManager.Trades[0].Close.IfExistsThen(x => { outValue = x.Level; });
            Assert.Equal(testCloseLevel, outValue);
        }

        [Gwt("Given a trade manager",
            "when a trade is added with an option some high",
            "the trade is added to the trades collection with the correct option some high")]
        public void T7()
        {
            // Arrange
            var tradeDetails = TestTradeDetailsViewModel;
            const double testHigh = 69.8;
            tradeDetails.High = Option.Some(testHigh);
            var tradeManager = new TradeManager{ Filters = TestFilters };
            var outValue = 1.00;

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            tradeManager.Trades[0].High.IfExistsThen(x => { outValue = x; } );
            Assert.Equal(testHigh, outValue);
        }

        [Gwt("Given a trade manager",
            "when a trade is added with an option some low",
            "the trade is added to the trades collection with the correct option some low")]
        public void T8()
        {
            // Arrange
            var tradeDetails = TestTradeDetailsViewModel;
            const double testLow = 93.8;
            tradeDetails.Low = Option.Some(testLow);
            var tradeManager = new TradeManager{ Filters = TestFilters };
            var outValue = 1.00;

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            tradeManager.Trades[0].Low.IfExistsThen(x => { outValue = x; });
            Assert.Equal(testLow, outValue);
        }
    }
}
