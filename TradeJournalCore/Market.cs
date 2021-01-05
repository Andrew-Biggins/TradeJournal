using System;
using System.ComponentModel;
using Common;
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

    public sealed class Market : IMarket
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

        public AssetClass AssetClass { get; }

        public Market(string name, AssetClass assetClass)
        {
            Name = name;
            AssetClass = assetClass;
        }

        private bool _isSelected;
    }
}
