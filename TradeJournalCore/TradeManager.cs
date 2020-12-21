using System.Collections.ObjectModel;
using Common.Optional;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModels;

namespace TradeJournalCore
{
    public sealed class TradeManager : ITradeManager
    {
        public ObservableCollection<ITrade> Trades { get; } = new ObservableCollection<ITrade>();

        public ITrade SelectedTrade { get; set; }

        public void AddNewTrade(TradeDetailsViewModel tradeDetails)
        {
            var close = GetCloseExecution(tradeDetails);

            var trade = new Trade(tradeDetails.SelectedMarket, tradeDetails.SelectedStrategy,
                new Levels(tradeDetails.Levels.Entry, tradeDetails.Levels.Stop, tradeDetails.Levels.Target),
                new Execution(tradeDetails.Open.Level, tradeDetails.Open.DateTime, tradeDetails.Open.Size), close,
                (tradeDetails.MaxAdverse, tradeDetails.MaxFavourable));

            Trades.Add(trade);
        }

        public void RemoveTrade()
        {
            Trades.Remove(SelectedTrade);
        }

        public void FilterTrades(TradeFiltererViewModel filters)
        {

        }

        private static Optional<Execution> GetCloseExecution(TradeDetailsViewModel tradeDetails)
        {
            var close = new Execution(0, tradeDetails.CloseDateTime, 0);
            var fieldIsEmpty = false;

            tradeDetails.CloseLevel.IfExistsThen(x => { close.Level = x; }).IfEmpty(() => fieldIsEmpty = true);

            return fieldIsEmpty ? Option.None<Execution>() : Option.Some(close);
        }
    }
}
