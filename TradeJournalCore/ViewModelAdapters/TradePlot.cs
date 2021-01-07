using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModelAdapters
{
    public class TradePlot : PlotModel, ITradePlot
    {
        public List<DataPoint> Points { get; }

        public TradePlot()
        {
            PlotAreaBorderColor = OxyColors.White;
            Background = OxyColors.Black;

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Balance",
                AbsoluteMinimum = 0,
                AxisTitleDistance = 10,
                MaximumPadding = 0.1,
                MinimumPadding = 0.1,
                TextColor = OxyColors.White,
                TicklineColor = OxyColors.White,
                MajorGridlineColor = OxyColors.White,
                ExtraGridlineColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TitleColor = OxyColors.White
            };
            Axes.Add(yAxis);

            LinearAxis xAxis = new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                StringFormat = "dd/MM HH:mm",
                Title = "Date/Time",
                AxisTitleDistance = 10,
                TextColor = OxyColors.White,
                TicklineColor = OxyColors.White,
                MajorGridlineColor = OxyColors.White,
                ExtraGridlineColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TitleColor = OxyColors.White,
            };
            Axes.Add(xAxis);

            var lineSeries = new LineSeries();
            Points = lineSeries.Points;
            Series.Add(lineSeries);
        }

        public void UpdateData(double balance, IEnumerable<ITrade> trades)
        {
            var tradeList = new List<SortableTradeResultDataPoint>();

            foreach (var trade in trades)
            {
                trade.Close.IfExistsThen(x =>
                {
                    trade.CashResult.IfExistsThen(y =>
                    {
                        tradeList.Add(new SortableTradeResultDataPoint(x.DateTime, y));
                    });
                });
            }

            tradeList.Sort((x, y) => DateTime.Compare(x.CloseTime, y.CloseTime));

            Points.Clear();

            foreach (var trade in tradeList)
            {
                balance += trade.Profit;

                if (balance <= 0)
                {
                    Points.Add(new DataPoint(DateTimeAxis.ToDouble(trade.CloseTime), 0));
                    break;
                }

                Points.Add(new DataPoint(DateTimeAxis.ToDouble(trade.CloseTime), balance));
            }

            InvalidatePlot(true);
        }
    }
}
