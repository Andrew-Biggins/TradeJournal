using System;
using System.Windows.Input;
using Common;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModels
{
    public sealed class MainWindowViewModel
    {
        public ICommand AddNewTradeCommand => new BasicCommand(AddNewTrade);

        public double AccountStartSize
        {
            get => _accountStartSize;
            set
            {
                _accountStartSize = value;
            }
        }

        public MainWindowViewModel(IRunner runner)
        {
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
        }

        private void AddNewTrade()
        { 
            _runner.GetTradeDetails(new AddTradeViewModel());
        }

        private readonly IRunner _runner;
        private double _accountStartSize = 10000;
    }
}
