using Common;
using System;
using System.Windows.Input;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModels
{
    public sealed class MainWindowViewModel
    {
        public ITradeManager TradeManager { get; }

        public ICommand AddNewTradeCommand => new BasicCommand(AddTrade);

        public ICommand RemoveTradeCommand => new BasicCommand(RemoveTrade);

        public ICommand EditTradeCommand => new BasicCommand(EditTrade);

        public double AccountStartSize
        {
            get => _accountStartSize;
            set
            {
                _accountStartSize = value;
            }
        }

        public TradeFiltererViewModel TradeFilterer { get; } = new TradeFiltererViewModel();

        public MainWindowViewModel(IRunner runner, ITradeManager tradeManager, TradeDetailsViewModel tradeDetailsViewModel)
        {
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
            TradeManager = tradeManager ?? throw new ArgumentNullException(nameof(tradeManager));
            _tradeDetailsViewModel = tradeDetailsViewModel ?? throw new ArgumentNullException(nameof(tradeDetailsViewModel));

            _tradeDetailsViewModel.TradeAdded += ConfirmTrade;

            _tradeDetailsViewModel.AddSelectables(TradeFilterer.Markets, TradeFilterer.Strategies);
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
        }

        private void RemoveTrade()
        {
            if (_runner.RunForResult(this, Messages.ConfirmRemoveTrade))
            {
                TradeManager.RemoveTrade();
            }
        }

        private void EditTrade()
        {
            _tradeDetailsViewModel.EditTrade(TradeManager.SelectedTrade);
            _runner.GetTradeDetails(_tradeDetailsViewModel);
        }

        private readonly IRunner _runner;
        private readonly TradeDetailsViewModel _tradeDetailsViewModel;
        private double _accountStartSize = 10000;
    }
}
