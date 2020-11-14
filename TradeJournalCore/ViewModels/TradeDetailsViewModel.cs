using Common;
using Common.Optional;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModels
{
    public abstract class TradeDetailsViewModel : ViewModelBase
    {
        public EventHandler TradeAdded;

        public ICommand ConfirmNewTradeCommand => new BasicCommand(() => TradeAdded.Raise(this));

        public ISelectable SelectedMarket { get; set; } = new Market("USDJPY");

        public ISelectable SelectedStrategy { get; set; } = new Strategy("Triangle");

        public ObservableCollection<ISelectable> Strategies { get; }

        public ObservableCollection<ISelectable> Markets { get; }

        public Levels Levels { get; } = new Levels(6000, 5900, 6500);

        public Execution Open { get; } = new Execution(6000, DateTime.Today, 1);

        public Optional<double> CloseLevel { get; set; } = Option.None<double>();

        public DateTime CloseDateTime { get; set; } = DateTime.Today;

        public Optional<double> CloseSize { get; set; } = Option.None<double>();

        public Optional<Excursion> MaxAdverse { get; set; } = Option.None<Excursion>();

        public Optional<Excursion> MaxFavourable { get; set; } = Option.None<Excursion>();

        protected TradeDetailsViewModel(IRunner runner)
        {
            Runner = runner;
        }

        protected IRunner Runner;
    }
}
