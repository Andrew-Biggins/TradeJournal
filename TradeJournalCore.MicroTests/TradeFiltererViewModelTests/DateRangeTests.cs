using System;
using System.Collections.Generic;
using System.Text;
using Common.MicroTests;
using TradeJournalCore.ViewModels;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeFiltererViewModelTests
{
    public sealed class DateRangeTests
    {
        [Gwt("Given a trade filterer view model",
            "when the dates are updated",
            "the date properties are updated correctly")]
        public void T0()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var startDate = new DateTime(2021,1,1);
            var endDate = new DateTime(2021,1,22);

            // Act
            viewModel.UpdateDates((startDate, endDate));

            // Assert
            Assert.Equal(startDate, viewModel.TradesStartDate);
            Assert.Equal(startDate, viewModel.FilterStartDate);
            Assert.Equal(endDate, viewModel.TradesEndDate);
            Assert.Equal(endDate, viewModel.FilterEndDate);
        }
    }
}
