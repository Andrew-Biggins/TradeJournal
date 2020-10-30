namespace TradeJournalCore.ViewModels
{
    public sealed class MainWindowViewModel
    {

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

        private double _accountStartSize = 10000;
    }
}
