namespace TradeJournalCore
{
    public class Levels
    {
        public double Entry { get; set; }

        public double Stop { get; set; }

        public double Target { get; set; }

        public Levels(double entry, double stop, double target)
        {
            Entry = entry;
            Stop = stop;
            Target = target;
        }
    }
}
