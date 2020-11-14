using System.Collections.ObjectModel;
using Common.Optional;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModels;

namespace TradeJournalCore
{
    public sealed class TradeManager : ITradeManager
    {
        public ObservableCollection<ITrade> Trades { get; } = new ObservableCollection<ITrade>();

        public void AddNewTrade(AddTradeViewModel tradeDetails)
        {
            var close = GetCloseExecution(tradeDetails);

            var trade = new Trade(tradeDetails.SelectedMarket, tradeDetails.SelectedStrategy,
                tradeDetails.Levels, tradeDetails.Open, close,
                (tradeDetails.MaxAdverse, tradeDetails.MaxFavourable));

            Trades.Add(trade);
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
