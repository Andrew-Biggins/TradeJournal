using System;
using Common;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Day : ISelectable
    {
        public event EventHandler SelectedChanged;

        public string Name { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                value = !_isSelected;
                _isSelected = value;
                SelectedChanged.Raise(this);
            }
        }

        public Day(DayOfWeek day)
        {
            Name = day.ToString();
        }

        private bool _isSelected;
    }
}
