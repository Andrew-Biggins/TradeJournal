namespace TradeJournalCore.Interfaces
{
    public interface IMarket : ISelectableTradeField
    {
        AssetClass AssetClass { get; }

        PipDivisor PipDivisor { get; }
    }
}