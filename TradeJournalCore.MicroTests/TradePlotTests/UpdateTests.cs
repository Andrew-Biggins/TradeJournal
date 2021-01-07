using System;
using System.Collections.Generic;
using System.Text;
using Common.MicroTests;
using Common.Optional;
using NSubstitute;
using OxyPlot.Axes;
using OxyPlot.Series;
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

        [Gwt("Given a trade plot",
            "when closed trades are added",
            "they are added to the points series in the correct order with the correct data points")]
        public void T1()
        {
            // Arrange
            var plot = new TradePlot();
            var dtOne = new DateTime(2021, 1, 3);
            var testTradeOne = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(new Execution(150, dtOne, 1)), TestEmptyExcursions);

            var dtTwo = new DateTime(2021, 1, 1);
            var testTradeTwo = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(new Execution(110, dtTwo, 1)), TestEmptyExcursions);

            var dtThree = new DateTime(2021, 1, 2);
            var testTradeThree = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(new Execution(120, dtThree, 1)), TestEmptyExcursions);

            // Act 
            plot.UpdateData(1, new List<ITrade> { testTradeOne, testTradeTwo, testTradeThree });

            // Assert
            Assert.Equal(11, plot.Points[0].Y);
            Assert.Equal(DateTimeAxis.ToDouble(dtTwo), plot.Points[0].X);
            Assert.Equal(31, plot.Points[1].Y);
            Assert.Equal(DateTimeAxis.ToDouble(dtThree), plot.Points[1].X);
            Assert.Equal(81, plot.Points[2].Y);
            Assert.Equal(DateTimeAxis.ToDouble(dtOne), plot.Points[2].X);
        }

        // Tests the points from the previous update are cleared 
        [Gwt("Given a trade plot has points",
            "when updated with no trades",
            "the points collection is empty")]
        public void T2()
        {
            // Arrange
            var plot = new TradePlot();
            var testTradeOne = new Trade(TestMarket, TestStrategy, TestLevels, TestOpen, Option.Some(new Execution(150, new DateTime(2021, 1, 3), 1)), TestEmptyExcursions);
            plot.UpdateData(1, new List<ITrade> { testTradeOne });

            // Act 
            plot.UpdateData(1, new List<ITrade>());

            // Assert
            Assert.Empty(plot.Points);
        }
    }
}
