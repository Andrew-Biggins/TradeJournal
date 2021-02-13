using Common;
using Common.Optional;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModels
{
    public class TradeDetailsViewModel : ViewModelBase
    {
        public EventHandler TradeAdded;

        public ICommand ConfirmNewTradeCommand => new BasicCommand(() => TradeAdded.Raise(this));

        public ICommand AddNewMarketCommand => new BasicCommand(GetNewMarketDetails);

        public ICommand AddNewStrategyCommand => new BasicCommand(GetNewStrategyName);

        public IMarket SelectedMarket { get; set; } 

        public ISelectable SelectedStrategy { get; set; }

        public SelectableCollection<ISelectable> Strategies { get; private set; } = new SelectableCollection<ISelectable>();

        public SelectableCollection<IMarket> Markets { get; private set; } = new SelectableCollection<IMarket>();

        public Levels Levels { get; private set; } = new Levels(6000, 5900, 6500);

        public Execution Open { get; private set; } = new Execution(6000, DateTime.Today, 1);

        public EntryOrderType SelectedEntryOrderType { get; set; }

        public List<EntryOrderType> EntryOrderTypes { get; } = ((EntryOrderType[])Enum.GetValues(typeof(EntryOrderType))).ToList();

        public Optional<double> CloseLevel
        {
            get => _closeLevel;
            set
            {
                _closeLevel = value;
                UpdateExcursions();
            }
        }

        public DateTime CloseDateTime
        {
            get => _closeDateTime;
            set
            {
                _closeDateTime = value;
                TradeDetailsValidator.VerifyDates(Open.DateTime, CloseDateTime);
            }
        }

        public Optional<double> CloseSize { get; set; } = Option.None<double>();

        public Optional<double> MaxAdverse
        {
            get => _maxAdverse;
            set
            {
                SetProperty(ref _maxAdverse, value, nameof(MaxAdverse));
                TradeDetailsValidator.ValidateMae(_maxAdverse);
            }
        }

        public Optional<double> MaxFavourable
        {
            get => _maxFavourable;
            set
            {
                SetProperty(ref _maxFavourable, value, nameof(MaxFavourable));
                TradeDetailsValidator.ValidateMfe(_maxFavourable);
            }
        }

        public bool IsMaeFixed
        {
            get => _isMaeFixed;
            private set => SetProperty(ref _isMaeFixed, value, nameof(IsMaeFixed));
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

            TradeDetailsValidator.VerifyDates(Open.DateTime, CloseDateTime);
        }

        public void EditTrade(ITrade trade)
        {
            IsEditing = true;

            SelectedMarket = trade.Market;
            SelectedStrategy = trade.Strategy;
            Levels = trade.Levels;
            Open = trade.Open;

            trade.Close.IfExistsThen(x =>
            {
                CloseLevel = Option.Some(x.Level);
                CloseDateTime = x.DateTime;
            }).IfEmpty(() => CloseLevel = Option.None<double>());
        }

        public void AddSelectables(SelectableCollection<IMarket> markets, SelectableCollection<ISelectable> strategies)
        {
            Markets = markets;
            Strategies = strategies;

            if (Strategies.Count > 0)
            {
                SelectedStrategy = Strategies[0];
            }

            if (Markets.Count > 0)
            {
                SelectedMarket = Markets[0];
            }
        }

        private void OnLevelsChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateExcursions();
        }

        private void OnOpenChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Execution.DateTime))
            {
                TradeDetailsValidator.VerifyDates(Open.DateTime, CloseDateTime);
            }

            if (e.PropertyName == nameof(Execution.Level))
            {
                UpdateExcursions();
            }
        }

        private void UpdateExcursions()
        {
            TradeDetailsValidator.UpdateExcursionLimits(Levels.TradeDirection, CloseLevel, Open.Level,
                SelectedMarket.PipDivisor);

            double closeLevel = 0;
            var isCloseLevelSet = false;

            CloseLevel.IfExistsThen(x => 
                {  
                    closeLevel = x;
                    isCloseLevelSet = true; 
                }).IfEmpty(() =>
                {
                    MaxAdverse = Option.None<double>();
                    IsMaeFixed = false;
                });

            if (isCloseLevelSet)
            {
                if (Levels.TradeDirection == Direction.Long)
                {
                    SetFixedLongMae(closeLevel);
                }
                else
                {
                    SetFixedShortMae(closeLevel);
                }
            }
        }

        private void SetFixedLongMae(double closeLevel)
        {
            if (Open.Level > closeLevel)
            {
                MaxAdverse = Option.Some(Open.Level - closeLevel);
                IsMaeFixed = true;
            }
            else
            {
                IsMaeFixed = false;
            }
        }

        private void SetFixedShortMae(double closeLevel)
        {
            if (Open.Level < closeLevel)
            {
                MaxAdverse = Option.Some(closeLevel - Open.Level);
                IsMaeFixed = true;
            }
            else
            {
                IsMaeFixed = false;
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
            SelectedStrategy = strategy;
            RaisePropertyChanged(nameof(SelectedStrategy));
            _getNameViewModel.NameConfirmed -= AddStrategy;
        }

        private readonly IRunner _runner;
        private readonly GetNameViewModel _getNameViewModel;
        private readonly AddMarketViewModel _addMarketViewModel;

        private DateTime _closeDateTime = DateTime.Today.AddMinutes(1);
        private Optional<double> _maxAdverse = Option.None<double>();
        private Optional<double> _maxFavourable = Option.None<double>();
        private Optional<double> _closeLevel = Option.None<double>();
        private bool _isMaeFixed;
    }
}
