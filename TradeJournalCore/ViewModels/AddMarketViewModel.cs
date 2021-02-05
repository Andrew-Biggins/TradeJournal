using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace TradeJournalCore.ViewModels
{
    public sealed class AddMarketViewModel
    {
        public event EventHandler MarketConfirmed;

        public ICommand ConfirmMarketCommand => new BasicCommand(() => MarketConfirmed.Raise(this));

        public string Name { get; set; }

        public List<AssetClass> AssetClasses { get; }

        public List<PipDivisor> PipDivisors { get; }

        public AssetClass SelectedAssetClass { get; set; }

        public PipDivisor SelectedPipDivisor { get; set; }

        public AddMarketViewModel()
        {
            AssetClasses = GetAssetClasses();
            PipDivisors = GetPipDivisors();
            SelectedPipDivisor = PipDivisor.One;
        }

        private static List<AssetClass> GetAssetClasses()
        {
            var assetClasses = (AssetClass[])Enum.GetValues(typeof(AssetClass));

            return assetClasses.ToList();
        }

        private static List<PipDivisor> GetPipDivisors()
        {
            var pipDivisors = (PipDivisor[])Enum.GetValues(typeof(PipDivisor));

            return pipDivisors.ToList();
        }
    }
}
