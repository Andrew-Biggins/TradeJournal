using System.Collections.ObjectModel;
using TradeJournalCore.ViewModels;

namespace TradeJournalCore.Interfaces
{
    public interface ITradeManager
    {
        ObservableCollection<ITrade> Trades { get; }

        ITrade SelectedTrade { get; set; }

        void AddNewTrade(TradeDetailsViewModel tradeDetails);

        void RemoveTrade();

        void FilterTrades(TradeFiltererViewModel filters);
    }
}