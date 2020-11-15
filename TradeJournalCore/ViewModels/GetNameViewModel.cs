using System;
using System.Windows.Input;
using Common;

namespace TradeJournalCore.ViewModels
{
    public class GetNameViewModel
    {
        public event EventHandler NameConfirmed;

        public ICommand ConfirmNewNameCommand => new BasicCommand(() => NameConfirmed.Raise(this));

        public string Name { get; set; }
    }
}