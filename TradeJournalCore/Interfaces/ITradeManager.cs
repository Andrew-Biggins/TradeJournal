using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TradeJournalCore.ViewModels;

namespace TradeJournalCore.Interfaces
{
    public interface ITradeManager
    {
        event PropertyChangedEventHandler PropertyChanged;

        event EventHandler DateRangeChanged;
        ObservableCollection<ITrade> Trades { get; }

        ITrade SelectedTrade { get; set; }

        IFilters Filters { get; set; }

        void AddNewTrade(TradeDetailsViewModel tradeDetails);

        void RemoveTrade();

        void FilterTrades(IFilters filters);

        (DateTime, DateTime) GetDateRange();

        void ReadInTrades();
    }
}