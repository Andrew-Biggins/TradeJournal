namespace TradeJournalCore
{
    public class AssetType : Selectable
    {
        public AssetType(AssetClass assetClass) : base(assetClass.ToString())
        {
        }
    }
}
