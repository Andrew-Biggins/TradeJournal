using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModelAdapters
{
    public sealed class RiskPiePlot : PlotModel, IPiePlot
    {
        public void UpdateData(IList<IRisk> assetClassRisks)
        {
            Series.Clear();

            var seriesP1 = new PieSeries
            {
                StrokeThickness = 2.0, AngleSpan = 360, StartAngle = 0, OutsideLabelFormat = "",
                TickHorizontalLength = 0.00, TickRadialLength = 0.00, InsideLabelFormat = ""
            };

            for (var i = 0; i < assetClassRisks.Count - 1; i++)
            {
                seriesP1.Slices.Add(new PieSlice(assetClassRisks[i].Name, assetClassRisks[i].Percentage)
                    {Fill = SetSliceColour(assetClassRisks[i].Name)});
            }

            Series.Add(seriesP1);
            InvalidatePlot(true);
        }

        private OxyColor SetSliceColour(string assetClass)
        {
            return assetClass switch
            {
                "Commodities" => OxyColors.PaleVioletRed,
                "Currencies" => OxyColors.Green,
                "Crypto" => OxyColors.Yellow,
                "Shares" => OxyColors.MediumVioletRed,
                "Indices" => OxyColors.LightBlue,
                _ => throw new ArgumentOutOfRangeException(nameof(assetClass), assetClass, null)
            };
        }
    }
}
