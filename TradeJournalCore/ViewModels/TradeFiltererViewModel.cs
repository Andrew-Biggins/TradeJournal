using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Common;
using TradeJournalCore.Interfaces;
using static TradeJournalCore.SelectableFactory;

namespace TradeJournalCore.ViewModels
{
    public class TradeFiltererViewModel : ViewModelBase
    {
        public EventHandler FiltersChanged;

        public ObservableCollection<ISelectable> Markets { get; } = GetDefaultMarkets();

        public ObservableCollection<ISelectable> Strategies { get; } = GetDefaultStrategies();

        public IReadOnlyList<ISelectable> AssetTypes { get; } = GetAssetTypes();

        public IReadOnlyList<ISelectable> DaysOfWeek { get; } = GetDays();

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

        public TradeFiltererViewModel()
        {
         
        }

        

        private DateTime _tradesStartDate;
        private DateTime _tradesEndDate;
        private DateTime _filterStartDate;
        private DateTime _filterEndDate;
        private DateTime _filterStartTime = new DateTime(1, 1, 1, 0, 0, 0);
        private DateTime _filterEndTime = new DateTime(1, 1, 1, 23, 59, 59);
    }
}