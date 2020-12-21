using System;
using System.Collections.Generic;
using System.Text;
using Common.MicroTests;
using TradeJournalCore.ViewModels;

namespace TradeJournalCore.MicroTests.TradeFiltererViewModelTests
{
    public sealed class General
    {
        [Gwt("Given a trade filterer view model",
            "when the apply trade filters command is executed",
            "the filters changed event is raised")]
        public void T1()
        {
            // Arrange
            var viewModel = new TradeFiltererViewModel();
            var catcher = Catcher.Simple;
            viewModel.FiltersChanged += catcher.Catch;

            // Act
            viewModel.ApplyTradeFiltersCommand.Execute(null!);

            // Assert
            catcher.CaughtEmpty(viewModel);
        }
    }
}
