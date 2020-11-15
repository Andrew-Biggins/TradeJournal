using Common;
using Common.Optional;
using System;
using System.Collections.ObjectModel;
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

        public Optional<double> CloseLevel { get; set; } = Option.None<double>();

        public DateTime CloseDateTime { get; set; } = DateTime.Today;

        public Optional<double> CloseSize { get; set; } = Option.None<double>();

        public Optional<Excursion> MaxAdverse { get; set; } = Option.None<Excursion>();

        public Optional<Excursion> MaxFavourable { get; set; } = Option.None<Excursion>();

        public TradeDetailsViewModel(IRunner runner, GetNameViewModel getNameViewModel)
        {
            _runner = runner;
            _getNameViewModel = getNameViewModel;
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
    }
}
