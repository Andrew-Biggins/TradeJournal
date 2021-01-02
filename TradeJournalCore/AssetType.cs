using System;
using Common;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public class AssetType : ISelectable
    {
        public event EventHandler SelectedChanged;

        public string Name { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                value = !_isSelected;
                _isSelected = value;
                SelectedChanged.Raise(this);
            }
        }

        public AssetType(AssetClass assetClass)
        {
            Name = assetClass.ToString();
        }

        private bool _isSelected;
    }
}
