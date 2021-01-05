using System;
using System.ComponentModel;

namespace TradeJournalCore.Interfaces
{
    public interface ISelectable : INotifyPropertyChanged
    {
        string Name { get; }

        bool IsSelected { get; set; }
    }
}