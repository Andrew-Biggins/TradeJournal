using Common.Optional;

namespace TradeJournalCore.Interfaces
{
    public interface ITrade
    {
        Optional<Excursion> MaxAdverseExcursion { get; }

        Optional<Excursion> MaxFavourableExcursion { get; }
    }
}