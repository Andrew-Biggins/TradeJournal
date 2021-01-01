using Common;
using System;
using System.Collections.ObjectModel;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class SelectableCollection<T> : ObservableCollection<T>
    {
        public EventHandler SelectedChanged;

        public void AddSelectable(T item)
        {
            if (item is ISelectable selectable)
            {
                selectable.SelectedChanged += ItemSelectedChanged;
                Add(item);
            }
        }

        private void ItemSelectedChanged(object sender, EventArgs e)
        {
            SelectedChanged.Raise(this);
        }
    }
}
