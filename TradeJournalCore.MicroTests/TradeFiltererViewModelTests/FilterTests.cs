using System;
using Common.MicroTests;
using TradeJournalCore.ViewModels;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeFiltererViewModelTests
{
    public sealed class FilterTests
    {
        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain only the selected markets")]
        public void T0()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var expected = viewModel.Markets[0].Name;

            for (var i = 1; i < viewModel.Markets.Count; i++)
            {
                viewModel.Markets[i].IsSelected = false;
            }

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Single(actual.Markets);
            Assert.Equal(expected, actual.Markets[0].Name);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain only the selected strategies")]
        public void T1()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var expected = viewModel.Strategies[0].Name;

            for (var i = 1; i < viewModel.Strategies.Count; i++)
            {
                viewModel.Strategies[i].IsSelected = false;
            }

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Single(actual.Strategies);
            Assert.Equal(expected, actual.Strategies[0].Name);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain only the selected asset types")]
        public void T2()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var expected = viewModel.AssetTypes[0].Name;

            for (var i = 1; i < viewModel.AssetTypes.Count; i++)
            {
                viewModel.AssetTypes[i].IsSelected = false;
            }

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Single(actual.AssetClasses);
            Assert.Equal(expected, actual.AssetClasses[0].Name);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain only the selected days")]
        public void T3()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var expected = viewModel.DaysOfWeek[0].Name;

            for (var i = 1; i < viewModel.DaysOfWeek.Count; i++)
            {
                viewModel.DaysOfWeek[i].IsSelected = false;
            }

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Single(actual.Days);
            Assert.Equal(expected, actual.Days[0].Name);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain the start date from the view model")]
        public void T4()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var testDate = new DateTime(2011,11,11);
            viewModel.FilterStartDate = testDate;

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Equal(testDate, actual.StartDate);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain the end date from the view model")]
        public void T5()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var testDate = new DateTime(2011, 11, 11);
            viewModel.FilterEndDate = testDate;

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Equal(testDate, actual.EndDate);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain the start time from the view model")]
        public void T6()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var testTime = new DateTime(2011, 11, 11, 12, 34, 56);
            viewModel.FilterStartTime = testTime;

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Equal(testTime, actual.StartTime);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain the end time from the view model")]
        public void T7()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var testTime = new DateTime(2011, 11, 11, 12, 34, 56);
            viewModel.FilterEndTime = testTime;

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Equal(testTime, actual.EndTime);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain the minimum risk reward ratio from the view model")]
        public void T8()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            const double testRrr = 3.4;
            viewModel.MinRiskRewardRatio = testRrr;

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Equal(testRrr, actual.MinRiskRewardRatio);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain the maximum risk reward ratio from the view model")]
        public void T9()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            const double testRrr = 3.4;
            viewModel.MaxRiskRewardRatio = testRrr;

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Equal(testRrr, actual.MaxRiskRewardRatio);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain the trade direction from the view model")]
        public void T10()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.SelectedTradeDirection = TradeDirection.Short;

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Equal(TradeDirection.Short, actual.Direction);
        }

        [Gwt("Given a trade filterer view model",
            "when the filters are requested",
            "the filters contain the trade status from the view model")]
        public void T11()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.SelectedTradeStatus = TradeStatus.Open;

            // Act
            var actual = viewModel.GetFilters();

            // Assert
            Assert.Equal(TradeStatus.Open, actual.Status);
        }
    }
}
