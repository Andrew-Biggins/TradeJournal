using Common;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModels
{
    public sealed class MainWindowViewModel
    {
        public ITradeManager TradeManager { get; }

        public ObservableCollection<ISelectable> Markets { get; } = new ObservableCollection<ISelectable>();

        public ObservableCollection<ISelectable> Strategies { get; } = new ObservableCollection<ISelectable>();

        public ICommand AddNewTradeCommand => new BasicCommand(AddNewTrade);

        public double AccountStartSize
        {
            get => _accountStartSize;
            set
            {
                _accountStartSize = value;
            }
        }

        public MainWindowViewModel(IRunner runner, ITradeManager tradeManager, TradeDetailsViewModel tradeDetailsViewModel)
        {
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
            TradeManager = tradeManager ?? throw new ArgumentNullException(nameof(tradeManager));
            _tradeDetailsViewModel = tradeDetailsViewModel ?? throw new ArgumentNullException(nameof(tradeDetailsViewModel));

            _tradeDetailsViewModel.TradeAdded += AddTrade;
        }

        private void AddNewTrade()
        {
            _runner.GetTradeDetails(_tradeDetailsViewModel);
        }

        private void AddTrade(object sender, EventArgs e)
        {
            TradeManager.AddNewTrade(_tradeDetailsViewModel);
        }


        private readonly IRunner _runner;
        private readonly TradeDetailsViewModel _tradeDetailsViewModel;
        private double _accountStartSize = 10000;
    }
}
