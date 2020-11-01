namespace TradeJournalCore
{
    public class Levels
    {
        public double Entry { get; }

        public double Stop { get; }

        public double Target { get; }

        public Levels(double entry, double stop, double target)
        {
            Entry = entry;
            Stop = stop;
            Target = target;
        }
    }
}
