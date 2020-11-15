using System.Collections.ObjectModel;
using TradeJournalCore.ViewModels;

namespace TradeJournalCore.Interfaces
{
    public interface ITradeManager
    {
        ObservableCollection<ITrade> Trades { get; }

        void AddNewTrade(TradeDetailsViewModel tradeDetails);
    }
}