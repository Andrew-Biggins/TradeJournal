using Common.MicroTests;
using Common.Optional;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModelAdapters;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradePlotTests
{
    public sealed class UpdateTests
    {
        [Gwt("Given a trade plot",
            "when open trades are added",
            "they are not added to the points series")]
        public void T0()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            plot.UpdateData(0, new List<ITrade> { TestOpenTrade, TestOpenTrade });

            // Assert
            Assert.Empty(plot.Points);
        }

        // Tests the points from the previous update are cleared 
        [Gwt("Given a trade plot has points",
            "when updated with no trades",
            "the points collection is empty")]
        public void T2()
        {
            // Arrange
            var plot = new TradePlot();
            var testTradeOne = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(new Execution(150, new DateTime(2021, 1, 3), 1)), TestEmptyExcursions, EntryOrderType.Limit);
            plot.UpdateData(1, new List<ITrade> { testTradeOne });

            // Act 
            plot.UpdateData(1, new List<ITrade>());

            // Assert
            Assert.Empty(plot.Points);
        }
    }
}
