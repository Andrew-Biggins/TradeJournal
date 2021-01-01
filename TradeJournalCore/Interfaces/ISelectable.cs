using System;

namespace TradeJournalCore.Interfaces
{
    public interface ISelectable
    {
        event EventHandler SelectedChanged;

        string Name { get; }

        bool IsSelected { get; set; }
    }
}