using Common;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Common.Optional;
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

        public TradeStatistics Statistics
        {
            get => _statistics;
            private set => SetProperty(ref _statistics, value, nameof(Statistics));
        }

        public ITradePlot Plot { get; }

        public RiskManager RiskManager { get; } = new RiskManager();

        public MainWindowViewModel(IRunner runner, ITradeManager tradeManager,
            TradeDetailsViewModel tradeDetailsViewModel, ITradePlot plot)
        {
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
            TradeManager = tradeManager ?? throw new ArgumentNullException(nameof(tradeManager));
            _tradeDetailsViewModel =
                tradeDetailsViewModel ?? throw new ArgumentNullException(nameof(tradeDetailsViewModel));
            Plot = plot ?? throw new ArgumentNullException(nameof(plot));

            TradeManager.Filters = TradeFiltererViewModel.GetFilters();
            _tradeDetailsViewModel.AddSelectables(TradeFiltererViewModel.Markets, TradeFiltererViewModel.Strategies);

            _tradeDetailsViewModel.TradeAdded += ConfirmTrade;
            TradeFiltererViewModel.FiltersChanged += OnSelectedChanged;
            TradeFiltererViewModel.Markets.SelectedChanged += OnSelectedChanged;
            TradeFiltererViewModel.Markets.CollectionChanged += OnSelectedChanged;
            TradeFiltererViewModel.Strategies.SelectedChanged += OnSelectedChanged;
            TradeFiltererViewModel.Strategies.CollectionChanged += OnSelectedChanged;
            TradeFiltererViewModel.AssetTypes.SelectedChanged += OnSelectedChanged;
            TradeFiltererViewModel.DaysOfWeek.SelectedChanged += OnSelectedChanged;

            TradeFiltererViewModel.PropertyChanged += OnFiltersChanged;

            TradeManager.DateRangeChanged += OnTradeManagerDateRangeChanged;
            TradeManager.PropertyChanged += OnTradeManagerPropertyChanged;

            TradeManager.ReadInTrades();
            UpdateGraph();
            Statistics = GetStatistics(TradeManager.Trades, AccountStartSize);

            //for (var i = 0; i < 16000; i++)
            //{
            //    if (i % 3 == 0)
            //    {
            //        _tradeDetailsViewModel.CloseLevel = Option.Some(5900.00);
            //        _tradeDetailsViewModel.High = Option.Some(6100.00);
            //        _tradeDetailsViewModel.Low = Option.Some(5900.00);
            //    }
            //    else
            //    {
            //        _tradeDetailsViewModel.CloseLevel = Option.Some(6500.00);
            //        _tradeDetailsViewModel.High = Option.Some(6750.00);
            //        _tradeDetailsViewModel.Low = Option.Some(5950.00);
            //    }

            //    TradeManager.AddNewTrade(_tradeDetailsViewModel);

            //    _tradeDetailsViewModel.CloseDate = _tradeDetailsViewModel.CloseDate.AddDays(1);
            //}
        }

        private void OnTradeManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TradeManager.Trades))
            {
                UpdateGraph();
                Statistics = GetStatistics(TradeManager.Trades, AccountStartSize);
            }
        }

        private void OnTradeManagerDateRangeChanged(object sender, EventArgs e)
        {
            TradeFiltererViewModel.UpdateDates(TradeManager.GetDateRange());
        }

        private void OnFiltersChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(e.PropertyName == nameof(TradeFiltererViewModel.TradesStartDate) ||
                  e.PropertyName == nameof(TradeFiltererViewModel.TradesEndDate)))
            {
                TradeManager.FilterTrades(TradeFiltererViewModel.GetFilters());
            }
        }

        private void OnSelectedChanged(object sender, EventArgs e)
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
        private TradeStatistics _statistics;
    }
}
