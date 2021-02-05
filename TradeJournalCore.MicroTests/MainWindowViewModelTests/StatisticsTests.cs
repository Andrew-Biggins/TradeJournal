using System;
using System.Collections.Generic;
using Common;
using Common.MicroTests;
using NSubstitute;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.MicroTests.MainWindowViewModelTests.Shared;
using static TradeJournalCore.MicroTests.Shared;
using static TradeJournalCore.StatisticsGenerator;
using Xunit;

namespace TradeJournalCore.MicroTests.MainWindowViewModelTests
{
    public sealed class StatisticsTests
    {
        [Gwt("Given a main window view model",
            "when the trade manager filters trades",
            "property changed is raised for statistics")]
        public void T1()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(SubRunner, new TradeManager(), TestTradeDetailsViewModel, SubPlot);
            var catcher = Catcher.For(viewModel);

            // Act 
            viewModel.TradeManager.FilterTrades(TestFilters);

            // Assert
            catcher.CaughtPropertyChanged(viewModel, nameof(viewModel.Statistics));
        }

        [Gwt("Given a main window view model",
            "when the trade manager filters trades",
            "the statistics are updated")]
        public void T2()
        {
            // Arrange
            var runner = SubRunner;
            var expected = GetStatistics(new List<ITrade> {TestClosedTrade}, 10000);
            var viewModel = new MainWindowViewModel(runner, new TradeManager(), TestTradeDetailsViewModel, SubPlot);
            runner.RunForResult(viewModel, Arg.Is<Message>(m => m.Is(Messages.ConfirmClearAllTrades))).Returns(true);
            viewModel.ClearCommand.Execute(null!);
            viewModel.TradeManager.AddNewTrade(TestTradeDetailsViewModel);

            // Act
            viewModel.TradeManager.FilterTrades(TestFilters);

            // Assert
            Assert.Equal(expected.Wins, viewModel.Statistics.Wins);
            Assert.Equal(expected.Loses, viewModel.Statistics.Loses);
            Assert.Equal(expected.ProfitFactor, viewModel.Statistics.ProfitFactor);
            Assert.Equal(expected.PointsTotal, viewModel.Statistics.PointsTotal);
        }
    }
}
