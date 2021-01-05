using System;
using System.ComponentModel;
using Common;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public class AssetType : ISelectable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                PropertyChanged.Raise(this, nameof(IsSelected));
            }
        }

        public AssetType(AssetClass assetClass)
        {
            Name = assetClass.ToString();
        }

        private bool _isSelected;
    }
}
