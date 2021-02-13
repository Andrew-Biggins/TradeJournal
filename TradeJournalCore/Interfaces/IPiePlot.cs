using System.Collections.Generic;
using OxyPlot;

namespace TradeJournalCore.Interfaces
{
    public interface IPiePlot : IPlotModel
    {
        void UpdateData(IList<IRisk> risks);
    }
}