﻿using Common;
using Common.Optional;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModels
{
    public class TradeDetailsViewModel : ViewModelBase
    {
        public EventHandler TradeAdded;

        public ICommand ConfirmNewTradeCommand => new BasicCommand(() => TradeAdded.Raise(this));

        public ICommand AddNewMarketCommand => new BasicCommand(GetNewMarketName);

        public ICommand AddNewStrategyCommand => new BasicCommand(GetNewStrategyName);

        public ISelectable SelectedMarket { get; set; } = new Market("USDJPY");

        public ISelectable SelectedStrategy { get; set; } = new Strategy("Triangle");

        public ObservableCollection<ISelectable> Strategies { get; } = new ObservableCollection<ISelectable>();

        public ObservableCollection<ISelectable> Markets { get; } = new ObservableCollection<ISelectable>();

        public Levels Levels { get; } = new Levels(6000, 5900, 6500);

        public Execution Open { get; } = new Execution(6000, DateTime.Today, 1);

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

        public Optional<Excursion> MaxAdverse
        {
            get => _maxAdverse;
            set
            {
                SetProperty(ref _maxAdverse, value, nameof(MaxAdverse));
                TradeDetailsValidator.ValidateMae(_maxAdverse);
            }
        }

        public Optional<Excursion> MaxFavourable
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

        public TradeDetailsValidator TradeDetailsValidator { get; } = new TradeDetailsValidator();

        public TradeDetailsViewModel(IRunner runner, GetNameViewModel getNameViewModel)
        {
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
            _getNameViewModel = getNameViewModel ?? throw new ArgumentNullException(nameof(getNameViewModel));

            Open.PropertyChanged += OnOpenChanged;
            Levels.PropertyChanged += OnLevelsChanged;
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
            TradeDetailsValidator.UpdateExcursionLimits(Levels.TradeDirection, CloseLevel, Open.Level);

            double closeLevel = 0;
            var isCloseLevelSet = false;

            CloseLevel.IfExistsThen(x => 
                {  
                    closeLevel = x;
                    isCloseLevelSet = true; 
                }).IfEmpty(() =>
                {
                    MaxAdverse = Option.None<Excursion>();
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
                MaxAdverse = Option.Some(new Excursion(Open.Level - closeLevel, 0));
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
                MaxAdverse = Option.Some(new Excursion(closeLevel - Open.Level, 0));
                IsMaeFixed = true;
            }
            else
            {
                IsMaeFixed = false;
            }
        }

        private void GetNewMarketName()
        {
            _getNameViewModel.Name = string.Empty;
            _getNameViewModel.NameConfirmed += AddMarket;
            _runner.GetNewName(_getNameViewModel, "Enter Market Name");
        }

        private void GetNewStrategyName()
        {
            _getNameViewModel.Name = string.Empty;
            _getNameViewModel.NameConfirmed += AddStrategy;
            _runner.GetNewName(_getNameViewModel, "Enter Strategy Name");
        }

        private void AddMarket(object sender, EventArgs e)
        {
            var market = new Market(_getNameViewModel.Name);
            Markets.Add(market);
            SelectedMarket = market;
            RaisePropertyChanged(nameof(SelectedMarket));
            _getNameViewModel.NameConfirmed -= AddMarket;
        }

        private void AddStrategy(object sender, EventArgs e)
        {
            var strategy = new Strategy(_getNameViewModel.Name);
            Strategies.Add(strategy);
            SelectedStrategy = strategy;
            RaisePropertyChanged(nameof(SelectedStrategy));
            _getNameViewModel.NameConfirmed -= AddStrategy;
        }

        private readonly IRunner _runner;
        private readonly GetNameViewModel _getNameViewModel;

        private DateTime _closeDateTime = DateTime.Today;
        private Optional<Excursion> _maxAdverse = Option.None<Excursion>();
        private Optional<Excursion> _maxFavourable = Option.None<Excursion>();
        private Optional<double> _closeLevel = Option.None<double>();
        private bool _isMaeFixed;
    }
}
