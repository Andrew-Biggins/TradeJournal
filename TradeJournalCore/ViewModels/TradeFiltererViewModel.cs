using System;
using System.Collections.ObjectModel;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModels
{
    public class TradeFiltererViewModel
    {
        public ObservableCollection<ISelectable> Markets { get; } = new ObservableCollection<ISelectable>();

        public ObservableCollection<ISelectable> Strategies { get; } = new ObservableCollection<ISelectable>();

        public ObservableCollection<ISelectable> AssetTypes { get; } = new ObservableCollection<ISelectable>();

        public ObservableCollection<ISelectable> DaysOfWeek { get; } = new ObservableCollection<ISelectable>();

        public TradeFiltererViewModel()
        {
            GetAssetTypes();
        }

        private void GetAssetTypes()
        {
            var assetClasses = (AssetClass[])Enum.GetValues(typeof(AssetClass));

            foreach (var assetClass in assetClasses)
            {
                AssetTypes.Add(new AssetType(assetClass));
            }
        }
    }
}