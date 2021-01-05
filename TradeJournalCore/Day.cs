using System;
using System.ComponentModel;
using Common;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Day : ISelectable
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

        public Day(DayOfWeek day)
        {
            Name = day.ToString();
        }

        private bool _isSelected;
    }
}
