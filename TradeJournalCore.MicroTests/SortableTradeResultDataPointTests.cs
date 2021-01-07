using Common.MicroTests;
using System;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class SortableTradeResultDataPointTests
    {
        [Gwt("Given a sortable trade result data point",
            "when the date is read",
            "it is the same as the one given at construction")]
        public void T0()
        {
            // Arrange
            var dataPoint = new SortableTradeResultDataPoint(DateTime.Today, 50);

            // Act 
            var actual = dataPoint.CloseTime;

            // Assert
            Assert.Equal(DateTime.Today, actual);
        }

        [Gwt("Given a sortable trade result data point",
            "when the profit is read",
            "it is the same as the one given at construction")]
        public void T1()
        {
            // Arrange
            const double testProfit = 50;
            var dataPoint = new SortableTradeResultDataPoint(DateTime.Today, testProfit);

            // Act 
            var actual = dataPoint.Profit;

            // Assert
            Assert.Equal(testProfit, actual);
        }
    }
}
