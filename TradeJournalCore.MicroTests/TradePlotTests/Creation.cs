using Common.MicroTests;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using TradeJournalCore.ViewModelAdapters;
using Xunit;

namespace TradeJournalCore.MicroTests.TradePlotTests
{
    public sealed class Creation
    {
        [Gwt("Given a trade plot",
            "when the plot area border colour is read",
            "it is white by default")]
        public void T0()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.PlotAreaBorderColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the background is read",
            "it is black by default")]
        public void T1()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Background;

            // Assert
            Assert.Equal(OxyColors.Black, actual);
        }

        [Gwt("Given a trade plot",
            "when the axes are read",
            "there are two axes")]
        public void T2()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes.Count;

            // Assert
            Assert.Equal(2, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis position is read",
            "it is left by default")]
        public void T4()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].Position;

            // Assert
            Assert.Equal(AxisPosition.Left, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis title is read",
            "it is the correct title")]
        public void T5()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].Title;

            // Assert
            Assert.Equal("Balance", actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis absolute minimum is read",
            "it is zero by default")]
        public void T6()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].AbsoluteMinimum;

            // Assert
            Assert.Equal(0, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis axis title distance is read",
            "it is 10 by default")]
        public void T7()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].AxisTitleDistance;

            // Assert
            Assert.Equal(10, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis maximum padding is read",
            "it is 0.1 by default")]
        public void T8()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].MaximumPadding;

            // Assert
            Assert.Equal(0.1, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis maximum padding is read",
            "it is 0.1 by default")]
        public void T9()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].MaximumPadding;

            // Assert
            Assert.Equal(0.1, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis text colour is read",
            "it is white by default")]
        public void T10()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].TextColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis tick line colour is read",
            "it is zero by default")]
        public void T11()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].TicklineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis major grid line colour is read",
            "it is zero by default")]
        public void T12()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].MajorGridlineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis extra grid line colour is read",
            "it is zero by default")]
        public void T13()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].ExtraGridlineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis extra grid line colour is read",
            "it is zero by default")]
        public void T14()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].ExtraGridlineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis axis line colour is read",
            "it is zero by default")]
        public void T15()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].AxislineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the y axis title colour is read",
            "it is zero by default")]
        public void T16()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[0].TitleColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis position is read",
            "it is bottom by default")]
        public void T17()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].Position;

            // Assert
            Assert.Equal(AxisPosition.Bottom, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis string format is read",
            "it is the correct format")]
        public void T18()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].StringFormat;

            // Assert
            Assert.Equal("dd/MM", actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis title is read",
            "it is the correct format")]
        public void T19()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].Title;

            // Assert
            Assert.Equal("Date", actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis axis title distance is read",
            "it is 10 by default")]
        public void T20()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].AxisTitleDistance;

            // Assert
            Assert.Equal(10, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis axis title distance is read",
            "it is 10 by default")]
        public void T21()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].AxisTitleDistance;

            // Assert
            Assert.Equal(10, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis title colour is read",
            "it is white by default")]
        public void T22()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].TextColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis tick line colour is read",
            "it is white by default")]
        public void T23()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].TicklineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis major grid line colour is read",
            "it is white by default")]
        public void T24()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].MajorGridlineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis extra grid line colour is read",
            "it is white by default")]
        public void T25()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].ExtraGridlineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis line colour is read",
            "it is white by default")]
        public void T26()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].AxislineColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the x axis title colour is read",
            "it is white by default")]
        public void T27()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Axes[1].TitleColor;

            // Assert
            Assert.Equal(OxyColors.White, actual);
        }

        [Gwt("Given a trade plot",
            "when the series are read",
            "there is one series")]
        public void T28()
        {
            // Arrange
            var plot = new TradePlot();

            // Act 
            var actual = plot.Series.Count;

            // Assert
            Assert.Equal(1, actual);
        }

        [Gwt("Given a trade plot",
            "when the points are read",
            "there are the same points as the line series")]
        public void T29()
        {
            // Arrange
            var plot = new TradePlot();
            var series = (LineSeries)plot.Series[0];

            // Act 
            var actual = series.Points;

            // Assert
            Assert.Same(plot.Points, actual);
        }
    }
}
