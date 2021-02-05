namespace TradeJournalCore.Interfaces
{
    public interface IMarket : ISelectable
    {
        AssetClass AssetClass { get; }

        PipDivisor PipDivisor { get; }
    }
}