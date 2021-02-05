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

    public enum PipDivisor
    {
        One = 1,
        Ten = 10,
        OneHundred = 100,
        OneThousand = 1000,
        TenThousand = 10000
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

        public PipDivisor PipDivisor { get; }

        public Market(string name, AssetClass assetClass, PipDivisor pipDivisor)
        {
            Name = name;
            AssetClass = assetClass;
            PipDivisor = pipDivisor;
        }

        private bool _isSelected;
    }
}
