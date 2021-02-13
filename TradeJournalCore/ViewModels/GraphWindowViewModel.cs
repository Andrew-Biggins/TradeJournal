using System.Collections.Generic;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModelAdapters;

namespace TradeJournalCore.ViewModels
{
    public class GraphWindowViewModel
    {
        public ITradePlot Plot { get; }

        public GraphWindowViewModel(double accountStartSize, IEnumerable<ITrade> trades)
        {
            Plot = new TradePlot();
            Plot.UpdateData(accountStartSize, trades);
        }
    }
}
