﻿using System;
using Common.MicroTests;
using Common.Optional;
using TradeJournalCore.ViewModels;
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
            var tradeManager = new TradeManager();

            // Act 
            tradeManager.AddNewTrade(new AddTradeViewModel(SubRunner));

            // Assert
            Assert.NotEmpty(tradeManager.Trades);
        }

        [Gwt("Given a trade manager",
            "when a trade is added",
            "the trade is added to the trades collection with the correct market")]
        public void T1()
        {
            // Arrange
            var tradeDetails = new AddTradeViewModel(SubRunner);
            var testMarket = new Market("test market");
            tradeDetails.SelectedMarket = testMarket;
            var tradeManager = new TradeManager();

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
            var tradeDetails = new AddTradeViewModel(SubRunner);
            var testStrategy = new Strategy("test strategy");
            tradeDetails.SelectedStrategy = testStrategy;
            var tradeManager = new TradeManager();

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
            var tradeDetails = new AddTradeViewModel(SubRunner);
            const double testLevelEntry = 123.00;
            tradeDetails.Levels.Entry = testLevelEntry;
            var tradeManager = new TradeManager();

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
            var tradeDetails = new AddTradeViewModel(SubRunner);
            const double testOpenSize = 19.6;
            tradeDetails.Open.Size = testOpenSize;
            var tradeManager = new TradeManager();

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
            var tradeDetails = new AddTradeViewModel(SubRunner);
            tradeDetails.CloseLevel = Option.None<double>();
            var tradeManager = new TradeManager();

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
            var tradeDetails = new AddTradeViewModel(SubRunner);
            const double testCloseLevel = 456.78;
            tradeDetails.CloseLevel = Option.Some(testCloseLevel);
            var tradeManager = new TradeManager();
            var outValue = 1.00;

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            tradeManager.Trades[0].Close.IfExistsThen(x => { outValue = x.Level; });
            Assert.Equal(testCloseLevel, outValue);
        }

        [Gwt("Given a trade manager",
            "when a trade is added with an option some max adverse excursion",
            "the trade is added to the trades collection with the correct option some max adverse excursion")]
        public void T7()
        {
            // Arrange
            var tradeDetails = new AddTradeViewModel(SubRunner);
            const double testMaxAdverseExcursion = 69.8;
            tradeDetails.MaxAdverse = Option.Some(new Excursion(testMaxAdverseExcursion, 50));
            var tradeManager = new TradeManager();
            var outValue = 1.00;

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            tradeManager.Trades[0].MaxAdverseExcursion.IfExistsThen(x => 
                { x.Points.IfExistsThen(y =>
                    { outValue = y;}
                    );
                });
            Assert.Equal(testMaxAdverseExcursion, outValue);
        }

        [Gwt("Given a trade manager",
            "when a trade is added with an option some max favourable excursion",
            "the trade is added to the trades collection with the correct option some max favourable excursion")]
        public void T8()
        {
            // Arrange
            var tradeDetails = new AddTradeViewModel(SubRunner);
            const double testMaxFavourableExcursion = 93.8;
            tradeDetails.MaxFavourable = Option.Some(new Excursion(testMaxFavourableExcursion, 50));
            var tradeManager = new TradeManager();
            var outValue = 1.00;

            // Act 
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            tradeManager.Trades[0].MaxFavourableExcursion.IfExistsThen(x =>
            {
                x.Points.IfExistsThen(y =>
                    { outValue = y; }
                );
            });
            Assert.Equal(testMaxFavourableExcursion, outValue);
        }
    }
}