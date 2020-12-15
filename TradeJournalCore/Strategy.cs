﻿using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Strategy : ISelectable
    {
        public string Name { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                value = !_isSelected;
                _isSelected = value;
            }
        }

        public Strategy(string name)
        {
            Name = name;
        }

        private bool _isSelected;
    }
}
