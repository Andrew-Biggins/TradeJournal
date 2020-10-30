using Common.Optional;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Trade : ITrade
    {
        public Optional<Excursion> MaxFavourableExcursion { get; } = Option.None<Excursion>();

        public Optional<Excursion> MaxAdverseExcursion { get; } = Option.None<Excursion>();


    }
}
