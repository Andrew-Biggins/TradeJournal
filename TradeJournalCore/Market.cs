using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public enum AssetClass
    {
        Commodities,
        Crypto,
        Currencies,
        Indices,
        Shares
    }

    public enum PipDivisor
    {
        One = 1,
        Ten = 10,
        OneHundred = 100,
        OneThousand = 1000,
        TenThousand = 10000
    }

    public sealed class Market : DatabaseSelectable, IMarket
    {
        public AssetClass AssetClass { get; }

        public PipDivisor PipDivisor { get; }

        public Market(string name, AssetClass assetClass, PipDivisor pipDivisor) : base(name)
        {
            AssetClass = assetClass;
            PipDivisor = pipDivisor;
        }
    }
}
