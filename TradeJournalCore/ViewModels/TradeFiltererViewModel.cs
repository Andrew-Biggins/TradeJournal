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
            set => SetProperty(ref _tradesStartDate, value, nameof(TradesStartDate));
        }

        public DateTime TradesEndDate
        {
            get => _tradesEndDate;
            set => SetProperty(ref _tradesEndDate, value, nameof(TradesEndDate));
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

        public double MinRiskRewardRatio
        {
            get => _minRiskRewardRatio;
            set => SetProperty(ref _minRiskRewardRatio, value);
        }

        public double MaxRiskRewardRatio
        {
            get => _maxRiskRewardRatio;
            set => SetProperty(ref _maxRiskRewardRatio, value);
        }

        public IReadOnlyList<TradeStatus> TradeStatuses { get; } = GetEnumList<TradeStatus>();

        public TradeStatus SelectedTradeStatus
        {
            get => _selectedTradeStatus;
            set => SetProperty(ref _selectedTradeStatus, value);
        }

        public IReadOnlyList<TradeDirection> TradeDirections { get; } = GetEnumList<TradeDirection>();

        public TradeDirection SelectedTradeDirection
        {
            get => _selectedTradeDirection;
            set => SetProperty(ref _selectedTradeDirection, value);
        }

        public IReadOnlyList<EntryOrderType> OrderTypes { get; } = GetEnumList<EntryOrderType>();

        public EntryOrderType SelectedOrderType
        {
            get => _selectedOrderType;
            set => SetProperty(ref _selectedOrderType, value);
        }

        public ICommand ClearTradeFiltersCommand => new BasicCommand(ClearFilters);

        public IFilters GetFilters()
        {
            return new Filters(RemoveUnselected(Markets), RemoveUnselected(Strategies), RemoveUnselected(AssetTypes),
                RemoveUnselected(DaysOfWeek), FilterStartDate, FilterEndDate, FilterStartTime, FilterEndTime,
                MinRiskRewardRatio, MaxRiskRewardRatio, SelectedTradeStatus, SelectedTradeDirection, SelectedOrderType);
        }

        public void UpdateDates((DateTime, DateTime) dateRange)
        {
            var (startDate, endDate) = dateRange;
            TradesStartDate = startDate;
            TradesEndDate = endDate;
            FilterStartDate = startDate;
            FilterEndDate = endDate;
        }

        private void ClearFilters()
        {
            SelectAll(Markets);
            SelectAll(Strategies);
            SelectAll(AssetTypes);
            SelectAll(DaysOfWeek);

            FilterStartDate = TradesStartDate;
            FilterEndDate = TradesEndDate;
            FilterStartTime = DateTime.MinValue;
            FilterEndTime = DateTime.MaxValue;
            MinRiskRewardRatio = DefaultMinRiskRewardRatio;
            MaxRiskRewardRatio = DefaultMaxRiskRewardRatio;
            SelectedTradeStatus = TradeStatus.Both;
            SelectedTradeDirection = TradeDirection.Both;
            SelectedOrderType = EntryOrderType.Both;
        }

        private static void SelectAll(IEnumerable<ISelectable> selectables)
        {
            foreach (var selectable in selectables)
            {
                if (!selectable.IsSelected)
                {
                    selectable.IsSelected = true;
                }   
            }
        }

        private static IReadOnlyList<ISelectable> RemoveUnselected(IEnumerable<ISelectable> selectables)
        {
            return selectables.Where(x => x.IsSelected).ToList();
        }

        private const double DefaultMinRiskRewardRatio = 0;
        private const double DefaultMaxRiskRewardRatio = 9999;

        private DateTime _tradesStartDate = DateTime.MinValue;
        private DateTime _tradesEndDate = DateTime.MaxValue;
        private DateTime _filterStartDate = DateTime.MinValue;
        private DateTime _filterEndDate = DateTime.MaxValue;
        private DateTime _filterStartTime = DateTime.MinValue;
        private DateTime _filterEndTime = DateTime.MaxValue;
        private double _minRiskRewardRatio = DefaultMinRiskRewardRatio;
        private double _maxRiskRewardRatio = DefaultMaxRiskRewardRatio;
        private TradeStatus _selectedTradeStatus = TradeStatus.Both;
        private TradeDirection _selectedTradeDirection = TradeDirection.Both;
        private EntryOrderType _selectedOrderType = EntryOrderType.Both;
    }
}