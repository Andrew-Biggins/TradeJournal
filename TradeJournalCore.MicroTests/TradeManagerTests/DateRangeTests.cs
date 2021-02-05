using System;
using Common.MicroTests;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeManagerTests
{
    public sealed class DateRangeTests
    {
        [Gwt("Given a trade manager with no trades",
            "when the trade date range is requested",
            "the max and min date times are returned")]
        public void T0()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };

            // Act 
            var (startDate, endDate) = tradeManager.GetDateRange();

            // Assert
            Assert.Equal(DateTime.MaxValue, startDate);
            Assert.Equal(DateTime.MinValue, endDate);
        }

        [Gwt("Given a trade manager that has a trade",
            "when another trade with an earlier open date is added",
            "the start of the trade date range is updated correctly")]
        public void T1()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            var tradeDetails = TestTradeDetailsViewModel;
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 2);
            tradeManager.AddNewTrade(tradeDetails);
            var testDate = new DateTime(2021, 1, 1);
            tradeDetails.Open.DateTime = testDate;

            // Act
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.Equal(testDate, tradeManager.GetDateRange().Item1);
        }

        [Gwt("Given a trade manager that has a trade",
            "when another trade with a later open date is added",
            "the start of the trade date range is not updated")]
        public void T2()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            var tradeDetails = TestTradeDetailsViewModel;
            var testDate = new DateTime(2021, 1, 2);
            tradeDetails.Open.DateTime = testDate;
            tradeManager.AddNewTrade(tradeDetails);
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 3);

            // Act
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.Equal(testDate, tradeManager.GetDateRange().Item1);
        }

        [Gwt("Given a trade manager that has a trade",
            "when another trade with an later open date is added",
            "the end of the trade date range is updated correctly")]
        public void T3()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            var tradeDetails = TestTradeDetailsViewModel;
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 2);
            tradeManager.AddNewTrade(tradeDetails);
            var testDate = new DateTime(2021, 1, 3);
            tradeDetails.Open.DateTime = testDate;

            // Act
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.Equal(testDate, tradeManager.GetDateRange().Item2);
        }

        [Gwt("Given a trade manager that has a trade",
            "when another trade with an earlier open date is added",
            "the end of the trade date range is not updated")]
        public void T4()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            var tradeDetails = TestTradeDetailsViewModel;
            var testDate = new DateTime(2021, 1, 2);
            tradeDetails.Open.DateTime = testDate;
            tradeManager.AddNewTrade(tradeDetails);
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 1);

            // Act
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            Assert.Equal(testDate, tradeManager.GetDateRange().Item2);
        }

        [Gwt("Given a trade manager",
            "when a trade is added to update the date range",
            "the date range changed event is raised")]
        public void T5()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            var tradeDetails = TestTradeDetailsViewModel;
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 2);
            var catcher = Catcher.Simple;
            tradeManager.DateRangeChanged += catcher.Catch;
            
            // Act
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            catcher.CaughtEmpty(tradeManager);
        }

        [Gwt("Given a trade manager",
            "when a trade is added to update the date range",
            "the date range changed event is not raised")]
        public void T6()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            var tradeDetails = TestTradeDetailsViewModel;
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 2);
            tradeManager.AddNewTrade(tradeDetails);
            var catcher = Catcher.Simple;
            tradeManager.DateRangeChanged += catcher.Catch;

            // Act
            tradeManager.AddNewTrade(tradeDetails);

            // Assert
            catcher.DidNotCatch();
        }

        [Gwt("Given a trade manager",
            "when a trade is removed",
            "the date range changed event is raised")]
        public void T7()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            tradeManager.SelectedTrade = TestOpenTrade;
            var catcher = Catcher.Simple;
            tradeManager.DateRangeChanged += catcher.Catch;

            // Act
            tradeManager.RemoveTrade();

            // Assert
            catcher.CaughtEmpty(tradeManager);
        }

        [Gwt("Given a trade manager with trades",
            "when a trade is removed from the start of the date range",
            "the start of the date range is updated correctly")]
        public void T8()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            var tradeDetails = TestTradeDetailsViewModel;
            var testDate = new DateTime(2021, 1, 3);
            tradeDetails.Open.DateTime = testDate;
            tradeManager.AddNewTrade(tradeDetails);
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 2);
            tradeManager.AddNewTrade(tradeDetails);
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 4);
            tradeManager.AddNewTrade(tradeDetails);
            tradeManager.SelectedTrade = tradeManager.Trades[1];

            // Act
            tradeManager.RemoveTrade();

            // Assert
            Assert.Equal(testDate, tradeManager.GetDateRange().Item1);
        }

        [Gwt("Given a trade manager with trades",
            "when a trade is removed from the start of the date range",
            "the start of the date range is updated correctly")]
        public void T9()
        {
            // Arrange
            var tradeManager = new TradeManager { Filters = TestFilters };
            var tradeDetails = TestTradeDetailsViewModel;
            var testDate = new DateTime(2021, 1, 3);
            tradeDetails.Open.DateTime = testDate;
            tradeManager.AddNewTrade(tradeDetails);
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 4);
            tradeManager.AddNewTrade(tradeDetails);
            tradeDetails.Open.DateTime = new DateTime(2021, 1, 2);
            tradeManager.AddNewTrade(tradeDetails);
            tradeManager.SelectedTrade = tradeManager.Trades[1];

            // Act
            tradeManager.RemoveTrade();

            // Assert
            Assert.Equal(testDate, tradeManager.GetDateRange().Item2);
        }
    }
}
