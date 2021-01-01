using Common;
using Common.Optional;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.TradeFilterer;

namespace TradeJournalCore
{
    public sealed class TradeManager : ITradeManager, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ITrade> Trades { get; private set; } = new ObservableCollection<ITrade>();

        public ITrade SelectedTrade { get; set; }

        public IFilters Filters { get; set; } 

        public void AddNewTrade(TradeDetailsViewModel tradeDetails)
        {
            var close = GetCloseExecution(tradeDetails);

            var trade = new Trade(tradeDetails.SelectedMarket, tradeDetails.SelectedStrategy,
                new Levels(tradeDetails.Levels.Entry, tradeDetails.Levels.Stop, tradeDetails.Levels.Target),
                new Execution(tradeDetails.Open.Level, tradeDetails.Open.DateTime, tradeDetails.Open.Size), close,
                (tradeDetails.MaxAdverse, tradeDetails.MaxFavourable));

            _unfilteredTrades.Add(trade);
            FilterTrades(Filters);
        }

        public void RemoveTrade()
        {
            Trades.Remove(SelectedTrade);
        }

        public void FilterTrades(IFilters filters)
        {
            Filters = filters;
            var assetClassesRemoved = RemoveUnselectedAssetClasses(_unfilteredTrades, filters.AssetClasses);
            var marketsRemoved = RemoveUnselectedMarkets(assetClassesRemoved, filters.Markets);
            var strategiesRemoved = RemoveUnselectedStrategies(marketsRemoved, filters.Strategies);
            Trades = strategiesRemoved;
            PropertyChanged.Raise(this, nameof(Trades));
        }

        private static Optional<Execution> GetCloseExecution(TradeDetailsViewModel tradeDetails)
        {
            var close = new Execution(0, tradeDetails.CloseDateTime, 0);
            var fieldIsEmpty = false;

            tradeDetails.CloseLevel.IfExistsThen(x => { close.Level = x; }).IfEmpty(() => fieldIsEmpty = true);

            return fieldIsEmpty ? Option.None<Execution>() : Option.Some(close);
        }

        private readonly IList<ITrade> _unfilteredTrades = new List<ITrade>();
    }
}
