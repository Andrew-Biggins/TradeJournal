using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Risk : IRisk
    {
        public string Name { get; }

        public double Amount { get; }

        public double Percentage { get; set; }

        public Risk(string name, double risk)
        {
            Name = name;
            Amount = risk;
        }
    }
}
