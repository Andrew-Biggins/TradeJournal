﻿using System.Collections.Generic;
using OxyPlot;

namespace TradeJournalCore.Interfaces
{
    public interface ITradePlot : IPlotModel
    {
        List<DataPoint> Points { get; }

        void InvalidatePlot(bool updateData);

        void UpdateData(double balance, IEnumerable<ITrade> trades);
    }
}