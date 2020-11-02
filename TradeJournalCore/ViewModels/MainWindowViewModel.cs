using System.Windows.Input;
using Common;

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

        public MainWindowViewModel()
        {
            
        }

        private void AddNewTrade()
        {
            throw new System.NotImplementedException();
        }

        private double _accountStartSize = 10000;
    }
}
