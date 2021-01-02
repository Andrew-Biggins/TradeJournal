using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Common;
using TradeJournalCore.Interfaces;
using static TradeJournalCore.SelectableFactory;

namespace TradeJournalCore.ViewModels
{
    public class TradeFiltererViewModel : ViewModelBase
    {
        public EventHandler FiltersChanged;

        public SelectableCollection<IMarket> Markets { get; } = GetDefaultMarkets();

        public SelectableCollection<ISelectable> Strategies { get; } = GetDefaultStrategies();

        public SelectableCollection<ISelectable> AssetTypes { get; } = GetAssetTypes();

        public SelectableCollection<ISelectable> DaysOfWeek { get; } = GetDays();

        public DateTime TradesStartDate
        {
            get => _tradesStartDate;
            set => SetProperty(ref _tradesStartDate, value);
        }

        public DateTime TradesEndDate
        {
            get => _tradesEndDate;
            set => SetProperty(ref _tradesEndDate, value);
        }

        public DateTime FilterStartDate
        {
            get => _filterStartDate;
            set => SetProperty(ref _filterStartDate, value);
        }

        public DateTime FilterEndDate
        {
            get => _filterEndDate;
            set => SetProperty(ref _filterEndDate, value);
        }

        public DateTime FilterStartTime
        {
            get => _filterStartTime;
            set => SetProperty(ref _filterStartTime, value);
        }

        public DateTime FilterEndTime
        {
            get => _filterEndTime;
            set => SetProperty(ref _filterEndTime, value);
        }

        public double MinRiskRewardRatio { get; set; } = 0;

        public double MaxRiskRewardRatio { get; set; } = 9999;

        public IReadOnlyList<TradeStatus> TradeStatuses { get; } = GetTradeStatuses();

        public TradeStatus SelectedTradeStatus { get; set; } = TradeStatus.Both;

        public IReadOnlyList<TradeDirection> TradeDirections { get; } = GetTradeDirections();

        public TradeDirection SelectedTradeDirection { get; set; } = TradeDirection.Both;

        public ICommand ApplyTradeFiltersCommand => new BasicCommand(() => FiltersChanged.Raise(this));

        public IFilters GetFilters()
        {
            return new Filters(RemoveUnselected(Markets), RemoveUnselected(Strategies), RemoveUnselected(AssetTypes),
                RemoveUnselected(DaysOfWeek), FilterStartDate, FilterEndDate, FilterStartTime, FilterEndTime,
                MinRiskRewardRatio, MaxRiskRewardRatio, SelectedTradeStatus, SelectedTradeDirection);
        }

        private static IReadOnlyList<ISelectable> RemoveUnselected(IEnumerable<ISelectable> selectables)
        {
            return selectables.Where(x => x.IsSelected).ToList();
        }
    
        private DateTime _tradesStartDate = DateTime.MinValue;
        private DateTime _tradesEndDate = DateTime.MaxValue;
        private DateTime _filterStartDate = DateTime.MinValue;
        private DateTime _filterEndDate = DateTime.MaxValue;
        private DateTime _filterStartTime = DateTime.MinValue;
        private DateTime _filterEndTime = DateTime.MaxValue;
    }
}