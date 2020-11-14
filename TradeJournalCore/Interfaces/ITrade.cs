using Common.Optional;

namespace TradeJournalCore.Interfaces
{
    public interface ITrade
    {
        ISelectable Market { get; }

        ISelectable Strategy { get; }

        Levels Levels { get; }

        Execution Open { get; }

        Optional<Execution> Close { get; }

        Optional<Excursion> MaxAdverseExcursion { get; }

        Optional<Excursion> MaxFavourableExcursion { get; }
    }
}