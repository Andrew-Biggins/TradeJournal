using Common.Optional;

namespace TradeJournalCore.Interfaces
{
    public interface ITrade
    {
        Levels Levels { get; }

        Optional<Excursion> MaxAdverseExcursion { get; }

        Optional<Excursion> MaxFavourableExcursion { get; }
    }
}