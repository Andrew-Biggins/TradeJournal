using System;
using System.Collections.ObjectModel;
using TradeJournalCore.Interfaces;
using static TradeJournalCore.SelectableFactory;

namespace TradeJournalCore.ViewModels
{
    public class TradeFiltererViewModel
    {
        public ObservableCollection<ISelectable> Markets { get; } = GetDefaultMarkets();

        public ObservableCollection<ISelectable> Strategies { get; } = GetDefaultStrategies();

        public ObservableCollection<ISelectable> AssetTypes { get; } = GetAssetTypes();

        public ObservableCollection<ISelectable> DaysOfWeek { get; } = GetDays();

        public TradeFiltererViewModel()
        {
        }
    }
}