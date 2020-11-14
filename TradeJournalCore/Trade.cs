using Common.Optional;
using System;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Trade : ITrade
    {
        public ISelectable Market { get; }

        public ISelectable Strategy { get; }

        public Optional<Excursion> MaxFavourableExcursion { get; }

        public Optional<Excursion> MaxAdverseExcursion { get; }

        public Levels Levels { get; }

        public Execution Open { get; }

        public Optional<Execution> Close { get; }

        public Trade(ISelectable market, ISelectable strategy, Levels levels, Execution open, Optional<Execution> close,
            (Optional<Excursion>, Optional<Excursion>) excursions)
        {
            Market = market ?? throw new ArgumentNullException(nameof(market));
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            Levels = levels ?? throw new ArgumentNullException(nameof(levels)); 
            Open = open ?? throw new ArgumentNullException(nameof(open)); 
            Close = close ?? throw new ArgumentNullException(nameof(close));

            var (adverse, favourable) = excursions; 
            MaxAdverseExcursion = adverse ?? throw new ArgumentNullException(nameof(adverse));
            MaxFavourableExcursion = favourable ?? throw new ArgumentNullException(nameof(favourable));
        }
    }
}
