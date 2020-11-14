using System.Collections.ObjectModel;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.ViewModels
{
    public sealed class AddTradeViewModel : TradeDetailsViewModel
    {
        public AddTradeViewModel(IRunner runner) : base(runner)
        {
            
        }

    }
}
