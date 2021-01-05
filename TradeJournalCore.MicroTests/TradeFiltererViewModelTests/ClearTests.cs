using System;
using Common.MicroTests;
using TradeJournalCore.ViewModels;
using Xunit;
using static TradeJournalCore.MicroTests.SelectableHelper;

namespace TradeJournalCore.MicroTests.TradeFiltererViewModelTests
{
    public sealed class ClearTests
    {
        [Gwt("Given a trade filterer view model with some markets unselected",
            "when the apply trade filters command is executed",
            "all markets are selected")]
        public void T0()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.Markets[0].IsSelected = false;
            viewModel.Markets[1].IsSelected = false;
            viewModel.Markets[2].IsSelected = false;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.True(IsAllSelected(viewModel.Markets));
        }

        [Gwt("Given a trade filterer view model with some strategies unselected",
            "when the apply trade filters command is executed",
            "all markets are selected")]
        public void T1()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.Strategies[0].IsSelected = false;
            viewModel.Strategies[1].IsSelected = false;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.True(IsAllSelected(viewModel.Strategies));
        }

        [Gwt("Given a trade filterer view model with some strategies unselected",
            "when the apply trade filters command is executed",
            "all strategies are selected")]
        public void T2()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.Strategies[0].IsSelected = false;
            viewModel.Strategies[1].IsSelected = false;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.True(IsAllSelected(viewModel.Strategies));
        }

        [Gwt("Given a trade filterer view model with some asset types unselected",
            "when the apply trade filters command is executed",
            "all asset types are selected")]
        public void T3()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.AssetTypes[3].IsSelected = false;
            viewModel.AssetTypes[4].IsSelected = false;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.True(IsAllSelected(viewModel.AssetTypes));
        }

        [Gwt("Given a trade filterer view model with some days unselected",
            "when the apply trade filters command is executed",
            "all days are selected")]
        public void T4()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.DaysOfWeek[3].IsSelected = false;
            viewModel.DaysOfWeek[6].IsSelected = false;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.True(IsAllSelected(viewModel.DaysOfWeek));
        }

        [Gwt("Given a trade filterer view model",
            "when the apply trade filters command is executed",
            "the filter dates are set to the trade range dates")]
        public void T5()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var startDate = new DateTime(2021, 1, 1);
            var endDate = new DateTime(2021, 1, 22);
            viewModel.TradesStartDate = startDate;
            viewModel.TradesEndDate = endDate;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.Equal(startDate, viewModel.FilterStartDate);
            Assert.Equal(endDate, viewModel.FilterEndDate);
        }

        [Gwt("Given a trade filterer view model",
            "when the apply trade filters command is executed",
            "the filter times are the default values")]
        public void T6()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var startTime = viewModel.FilterStartTime;
            var endTime = viewModel.FilterEndTime;
            viewModel.FilterStartTime = new DateTime(2021,1,1,11,11,00);
            viewModel.FilterEndTime = new DateTime(2021,1,1,11,16,00);

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.Equal(startTime, viewModel.FilterStartTime);
            Assert.Equal(endTime, viewModel.FilterEndTime);
        }

        [Gwt("Given a trade filterer view model",
            "when the apply trade filters command is executed",
            "the risk reward ratio values are the default values")]
        public void T7()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var min = viewModel.MinRiskRewardRatio;
            var max = viewModel.MaxRiskRewardRatio;
            viewModel.MinRiskRewardRatio = 5.00;
            viewModel.MaxRiskRewardRatio = 15.00;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.Equal(min, viewModel.MinRiskRewardRatio);
            Assert.Equal(max, viewModel.MaxRiskRewardRatio);
        }

        [Gwt("Given a trade filterer view model with open trade status selected",
            "when the apply trade filters command is executed",
            "the selected trade status is both")]
        public void T8()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.SelectedTradeStatus = TradeStatus.Open;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.Equal(TradeStatus.Both, viewModel.SelectedTradeStatus);
        }

        [Gwt("Given a trade filterer view model with long trade direction selected",
            "when the apply trade filters command is executed",
            "the selected trade direction is both")]
        public void T9()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            viewModel.SelectedTradeDirection = TradeDirection.Long;

            // Act
            viewModel.ClearTradeFiltersCommand.Execute(null!);

            // Assert
            Assert.Equal(TradeDirection.Both, viewModel.SelectedTradeDirection);
        }
    }
}
