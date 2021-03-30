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

        public DateTime Time
        {
            get => _time;
            set
            {
                if (value != _time)
                {
                    _time = value;
                    PropertyChanged.Raise(this, nameof(Time));
                }
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (value != _date)
                {
                    _date = value;
                    PropertyChanged.Raise(this, nameof(Date));
                }
            }
        }

        public double Size { get; set; }

        public Execution(double level, DateTime dateTime, double size)
        {
            Level = level;
            Date = dateTime;
            Time = dateTime;
            Size = size;
        }

        private DateTime _date;
        private DateTime _time;
        private double _level;
    }
}
