using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public class AssetType : ISelectable
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

        public AssetType(AssetClass assetClass)
        {
            Name = assetClass.ToString();
        }

        private bool _isSelected;
    }
}
