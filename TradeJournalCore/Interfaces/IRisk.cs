namespace TradeJournalCore.Interfaces
{
    public interface IRisk
    {
        string Name { get; }

        double Amount { get; }

        double Percentage { get; set; }
    }
}