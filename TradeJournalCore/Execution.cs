using System;
using System.ComponentModel;
using Common;

namespace TradeJournalCore
{
    public sealed class Execution
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public double Level
        {
            get => _level;
            set
            {
                if (value != _level)
                {
                    _level = value;
                    PropertyChanged.Raise(this, nameof(Level));
                }
            }
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                if (value != _dateTime)
                {
                    _dateTime = value;
                    PropertyChanged.Raise(this, nameof(DateTime));
                }
            }
        }

        public double Size { get; set; }

        public Execution(double level, DateTime dateTime, double size)
        {
            Level = level;
            DateTime = dateTime;
            Size = size;
        }

        private DateTime _dateTime;
        private double _level;
    }
}
