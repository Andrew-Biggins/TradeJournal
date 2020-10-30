using Common.Optional;

namespace TradeJournalCore
{
    public class Excursion
    {
        public Optional<double> Points { get; } = Option.None<double>();

        public Optional<double> Percentage { get; } = Option.None<double>();
    }
}