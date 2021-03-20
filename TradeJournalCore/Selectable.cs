using System.ComponentModel;
using Common;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public abstract class Selectable : ISelectable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                PropertyChanged.Raise(this, nameof(IsSelected));
            }
        }

        protected Selectable(string name)
        {
            Name = name;
        }

        private bool _isSelected;
    }
}