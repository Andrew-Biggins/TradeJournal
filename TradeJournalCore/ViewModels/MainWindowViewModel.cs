﻿using Common;
using System;
using System.ComponentModel;
using System.Windows.Input;
using TradeJournalCore.Interfaces;
using static TradeJournalCore.StatisticsGenerator;

namespace TradeJournalCore.ViewModels
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        public ITradeManager TradeManager { get; }

        public ICommand AddNewTradeCommand => new BasicCommand(AddTrade);

        public ICommand RemoveTradeCommand => new BasicCommand(RemoveTrade);

        public ICommand EditTradeCommand => new BasicCommand(EditTrade);

        public ICommand ClearCommand => new BasicCommand(ClearAllTrades);

        public ICommand PopOutGraphCommand => new BasicCommand(PopOutGraph);

        public ICommand MoreDetailsCommand => new BasicCommand(() => _runner.ShowStatsWindow(Statistics));

        public double AccountStartSize
        {
            get => _accountStartSize;
            set
            {
                _accountStartSize = value;
                UpdateGraph();
            }
        }

        public TradeFiltererViewModel TradeFiltererViewModel { get; } = new TradeFiltererViewModel();

        public TradeCollectionStatistics Statistics
        {
            get => _statistics;
            private set => SetProperty(ref _statistics, value, nameof(Statistics));
        }

        public ITradePlot Plot { get; }

        public RiskManager RiskManager { get; } = new RiskManager();

        public MainWindowViewModel(IRunner runner, ITradeManager tradeManager, TradeDetailsViewModel tradeDetailsViewModel, ITradePlot plot)
        {
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
            TradeManager = tradeManager ?? throw new ArgumentNullException(nameof(tradeManager));
            _tradeDetailsViewModel = tradeDetailsViewModel ?? throw new ArgumentNullException(nameof(tradeDetailsViewModel));
            Plot = plot ?? throw new ArgumentNullException(nameof(plot));

            TradeManager.Filters = TradeFiltererViewModel.GetFilters();
            _tradeDetailsViewModel.AddSelectables(TradeFiltererViewModel.Markets, TradeFiltererViewModel.Strategies);

            _tradeDetailsViewModel.TradeAdded += ConfirmTrade;
            TradeFiltererViewModel.FiltersChanged += SelectedChanged;
            TradeFiltererViewModel.Markets.SelectedChanged += SelectedChanged;
            TradeFiltererViewModel.Markets.CollectionChanged += SelectedChanged;
            TradeFiltererViewModel.Strategies.SelectedChanged += SelectedChanged;
            TradeFiltererViewModel.Strategies.CollectionChanged += SelectedChanged;
            TradeFiltererViewModel.AssetTypes.SelectedChanged += SelectedChanged;
            TradeFiltererViewModel.DaysOfWeek.SelectedChanged += SelectedChanged;

            TradeFiltererViewModel.PropertyChanged += FiltersChanged;

            TradeManager.DateRangeChanged += TradeManager_DateRangeChanged;
            TradeManager.PropertyChanged += TradeManager_PropertyChanged;

            TradeManager.ReadInTrades();
            UpdateGraph();
            Statistics = GetStatistics(TradeManager.Trades, AccountStartSize);
        }

        private void TradeManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TradeManager.Trades))
            {
                UpdateGraph();
                Statistics = GetStatistics(TradeManager.Trades, AccountStartSize);
            }
        }

        private void TradeManager_DateRangeChanged(object sender, EventArgs e)
        {
            TradeFiltererViewModel.UpdateDates(TradeManager.GetDateRange());
        }

        private void FiltersChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(e.PropertyName == nameof(TradeFiltererViewModel.TradesStartDate) ||
                  e.PropertyName == nameof(TradeFiltererViewModel.TradesEndDate)))
            {
                TradeManager.FilterTrades(TradeFiltererViewModel.GetFilters());
            }
        }

        private void SelectedChanged(object sender, EventArgs e)
        {
            TradeManager.FilterTrades(TradeFiltererViewModel.GetFilters());
        }

        private void AddTrade()
        {
            _tradeDetailsViewModel.IsEditing = false;
            _runner.GetTradeDetails(_tradeDetailsViewModel);
        }

        private void ConfirmTrade(object sender, EventArgs e)
        {
            if (_tradeDetailsViewModel.IsEditing)
            {
                TradeManager.RemoveTrade();
            }

            TradeManager.AddNewTrade(_tradeDetailsViewModel);
            RiskManager.UpdateRisks();
        }

        private void RemoveTrade()
        {
            if (_runner.RunForResult(this, Messages.ConfirmRemoveTrade))
            {
                TradeManager.RemoveTrade();
                RiskManager.UpdateRisks();
            }
        }

        private void EditTrade()
        {
            _tradeDetailsViewModel.EditTrade(TradeManager.SelectedTrade);
            _runner.GetTradeDetails(_tradeDetailsViewModel);
        }

        private void UpdateGraph()
        {
            Plot.UpdateData(AccountStartSize, TradeManager.Trades);
        }

        private void PopOutGraph()
        {
            var vm = new GraphWindowViewModel(AccountStartSize, TradeManager.Trades);

            _runner.ShowGraphWindow(vm);
        }

        private void ClearAllTrades()
        {
            if (_runner.RunForResult(this, Messages.ConfirmClearAllTrades))
            {
                TradeManager.ClearAll();
                RiskManager.UpdateRisks();
            }
        }

        private readonly IRunner _runner;
        private readonly TradeDetailsViewModel _tradeDetailsViewModel;
        private double _accountStartSize = 10000;
        private TradeCollectionStatistics _statistics;
    }
}
