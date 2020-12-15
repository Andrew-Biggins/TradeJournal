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

    public enum CommoditiesSubType
    {
        Agriculture,
        Energy,
        IndustrialMetals,
        Livestock,
        PreciousMetals
    }

    public sealed class Market : ISelectable
    {
        public string Name { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                value = !_isSelected;
                _isSelected = value;
            }
        }

        public AssetClass AssetClass { get; }

        public Market(string name, AssetClass assetClass)
        {
            Name = name;
            AssetClass = assetClass;
        }

        private bool _isSelected;
    }
}
