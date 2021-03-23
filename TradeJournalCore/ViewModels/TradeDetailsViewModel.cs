using Common;
using Common.Optional;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TradeJournalCore.Interfaces;
using static TradeJournalCore.DateTimeHelper;

namespace TradeJournalCore.ViewModels
{
    public class TradeDetailsViewModel : ViewModelBase
    {
        public EventHandler TradeAdded;

        public ICommand ConfirmNewTradeCommand => new BasicCommand(() => TradeAdded.Raise(this));

        public ICommand AddNewMarketCommand => new BasicCommand(GetNewMarketDetails);

        public ICommand AddNewStrategyCommand => new BasicCommand(GetNewStrategyName);

        public IMarket SelectedMarket { get; set; } 

        public ISelectableTradeField SelectedStrategy { get; set; }

        public SelectableCollection<ISelectableTradeField> Strategies { get; private set; } =
            new SelectableCollection<ISelectableTradeField>();

        public SelectableCollection<IMarket> Markets { get; private set; } = new SelectableCollection<IMarket>();

        public Levels Levels { get; private set; } = new Levels(6000, 5900, 6500);

        public Execution Open { get; private set; } = new Execution(6000, DateTime.Today, 1);

        public EntryOrderType SelectedEntryOrderType { get; set; }

        public List<EntryOrderType> EntryOrderTypes { get; } = new List<EntryOrderType>
            {EntryOrderType.Limit, EntryOrderType.Market};

        public Optional<double> CloseLevel
        {
            get => _closeLevel;
            set
            {
                _closeLevel = value;
                UpdateExcursions();
            }
        }

        public DateTime CloseDate
        {
            get => _closeDateTime;
            set
            {
                _closeDateTime = value;
                VerifyDateTimes();
            }
        }

        public DateTime CloseTime
        {
            get => _closeDateTime;
            set
            {
                _closeDateTime = value;
                VerifyDateTimes();
            }
        }

        public Optional<double> CloseSize { get; set; } = Option.None<double>();

        public Optional<double> High
        {
            get => _high;
            set
            {
                SetProperty(ref _high, value, nameof(High));
                TradeDetailsValidator.ValidateHigh(_high);
            }
        }

        public Optional<double> Low
        {
            get => _low;
            set
            {
                SetProperty(ref _low, value, nameof(Low));
                TradeDetailsValidator.ValidateLow(_low);
            }
        }

        public bool IsHighFixed
        {
            get => _isHighFixed;
            private set => SetProperty(ref _isHighFixed, value, nameof(IsHighFixed));
        }

        public bool IsLowFixed
        {
            get => _isLowFixed;
            private set => SetProperty(ref _isLowFixed, value, nameof(IsLowFixed));
        }

        public bool IsEditing { get; set; }

        public TradeDetailsValidator TradeDetailsValidator { get; } = new TradeDetailsValidator();

        public TradeDetailsViewModel(IRunner runner, GetNameViewModel getNameViewModel, AddMarketViewModel addMarketViewModel)
        {
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
            _getNameViewModel = getNameViewModel ?? throw new ArgumentNullException(nameof(getNameViewModel));
            _addMarketViewModel = addMarketViewModel ?? throw new ArgumentNullException(nameof(addMarketViewModel));

            Open.PropertyChanged += OnOpenChanged;
            Levels.PropertyChanged += OnLevelsChanged;

            VerifyDateTimes();
        }

        public void EditTrade(ITrade trade)
        {
            IsEditing = true;
            SetSelectedMarket(trade.Market.Id);
            SetSelectedStrategy(trade.Strategy.Id);
            
            Levels = trade.Levels;
            Open = trade.Open;

            Levels.PropertyChanged += OnLevelsChanged;

            trade.Close.IfExistsThen(x =>
            {
                CloseLevel = Option.Some(x.Level);
                CloseDate = x.Date;
                CloseTime = x.Time;
            }).IfEmpty(() =>
            {
                CloseLevel = Option.None<double>();
            });

            High = trade.High;
            Low = trade.Low;
        }

        public void AddSelectables(SelectableCollection<IMarket> markets,
            SelectableCollection<ISelectableTradeField> strategies)
        {
            Markets = markets;
            Strategies = strategies;

            if (Strategies.Count > 0)
            {
                SelectedStrategy = Strategies[0];
            }
            else
            {
                TradeDetailsValidator.StrategiesHaveError = true;
            }

            if (Markets.Count > 0)
            {
                SelectedMarket = Markets[0];
            }
            else
            {
                TradeDetailsValidator.MarketsHaveError = true;
            }
        }

        private void OnLevelsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Levels.Entry))
            {
                Open.Level = Levels.Entry;
                RaisePropertyChanged(nameof(Open));
            }

            UpdateExcursions();
        }

        private void OnOpenChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Execution.Date) || e.PropertyName == nameof(Execution.Time))
            {
                VerifyDateTimes();
            }

            if (e.PropertyName == nameof(Execution.Level))
            {
                UpdateExcursions();
            }
        }

        private void UpdateExcursions()
        {
            TradeDetailsValidator.UpdateExcursionLimits(Levels.TradeDirection, CloseLevel, Open.Level);

            double closeLevel = 0;
            var isCloseLevelSet = false;

            CloseLevel.IfExistsThen(x => 
                {  
                    closeLevel = x;
                    isCloseLevelSet = true; 
                }).IfEmpty(() =>
                {
                    High = Option.None<double>();
                    Low = Option.None<double>();
                    IsHighFixed = true;
                    IsLowFixed = true;
                });

            if (isCloseLevelSet)
            {
                if (Levels.TradeDirection == Direction.Long)
                {
                    SetFixedLongLow(closeLevel);
                    High = Option.None<double>();
                    IsHighFixed = false;
                }
                else
                {
                    SetFixedShortHigh(closeLevel);
                    Low = Option.None<double>();
                    IsLowFixed = false;
                }
            }
        }

        private void SetFixedLongLow(double closeLevel)
        {
            if (Open.Level > closeLevel)
            {
                Low = Option.Some(closeLevel);
                IsLowFixed = true;
            }
            else
            {
                IsLowFixed = false;
            }
        }

        private void SetFixedShortHigh(double closeLevel)
        {
            if (Open.Level < closeLevel)
            {
                High = Option.Some(closeLevel);
                IsHighFixed = true;
            }
            else
            {
                IsHighFixed = false;
            }
        }

        private void GetNewMarketDetails()
        {
            _addMarketViewModel.Name = string.Empty;
            _addMarketViewModel.MarketConfirmed += AddMarket;
            _runner.GetNewMarket(_addMarketViewModel, "Enter Market Details");
        }

        private void GetNewStrategyName()
        {
            _getNameViewModel.Name = string.Empty;
            _getNameViewModel.NameConfirmed += AddStrategy;
            _runner.GetNewName(_getNameViewModel, "Enter Strategy Name");
        }

        private void AddMarket(object sender, EventArgs e)
        {
            var market = new Market(_addMarketViewModel.Name, _addMarketViewModel.SelectedAssetClass, _addMarketViewModel.SelectedPipDivisor)
            {
                IsSelected = true
            };

            DataConnection.AddMarket(market);
            Markets.AddSelectable(market);

            if (TradeDetailsValidator.MarketsHaveError)
            {
                TradeDetailsValidator.MarketsHaveError = false;
            }

            SelectedMarket = market;
            RaisePropertyChanged(nameof(SelectedMarket));
            _addMarketViewModel.MarketConfirmed -= AddMarket;
        }

        private void AddStrategy(object sender, EventArgs e)
        {
            var strategy = new Strategy(_getNameViewModel.Name)
            {
                IsSelected = true
            };

            DataConnection.AddStrategy(strategy);
            Strategies.AddSelectable(strategy);

            if (TradeDetailsValidator.StrategiesHaveError)
            {
                TradeDetailsValidator.StrategiesHaveError = false;
            }

            SelectedStrategy = strategy;
            RaisePropertyChanged(nameof(SelectedStrategy));
            _getNameViewModel.NameConfirmed -= AddStrategy;
        }

        private void SetSelectedMarket(int id)
        {
            foreach (var market in Markets)
            {
                if (market.Id == id)
                {
                    SelectedMarket = market;
                    break;
                }
            }
        }

        private void SetSelectedStrategy(int id)
        {
            foreach (var strategy in Strategies)
            {
                if (strategy.Id == id)
                {
                    SelectedStrategy = strategy;
                    break;
                }
            }
        }

        private void VerifyDateTimes() => TradeDetailsValidator.VerifyDates(CombineDateTime(Open.Date, Open.Time),
            CombineDateTime(CloseDate, CloseTime));

        private readonly IRunner _runner;
        private readonly GetNameViewModel _getNameViewModel;
        private readonly AddMarketViewModel _addMarketViewModel;

        private DateTime _closeDateTime = DateTime.Today.AddMinutes(1);
        private Optional<double> _high = Option.None<double>();
        private Optional<double> _low = Option.None<double>();
        private Optional<double> _closeLevel = Option.None<double>();
        private bool _isHighFixed = true;
        private bool _isLowFixed = true;
    }
}
