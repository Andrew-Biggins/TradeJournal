using System.ComponentModel;
using Common;

namespace TradeJournalCore
{
    public enum Direction
    {
        Long,
        Short
    }

    public class Levels
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public double Entry
        {
            get => _entry;
            set
            {
                _entry = value;
                VerifyLevels();
            } 
        }

        public double Stop
        {
            get => _stop;
            set
            {
                if (value != _stop)
                {
                    _stop = value;
                    UpdateDirection();
                    PropertyChanged.Raise(this, nameof(Stop));
                }

                VerifyLevels();
            }
        }

        public double Target
        {
            get => _target;
            set
            {
                if (value != _target)
                {
                    _target = value;
                    UpdateDirection();
                    PropertyChanged.Raise(this, nameof(Target));
                }

                VerifyLevels();
            }
        }

        public Direction TradeDirection { get; private set; }

        public bool IsValidLevels
        {
            get => _isValidLevels;
            private set
            {
                if (value != _isValidLevels)
                {
                    _isValidLevels = value;
                    PropertyChanged.Raise(this, nameof(IsValidLevels));
                }
            }
        }

        public Levels(double entry, double stop, double target)
        {
            Entry = entry;
            Stop = stop;
            Target = target;
        }

        private void VerifyLevels()
        {
            if (Stop < Entry && Target > Entry || Stop > Entry && Target < Entry)
            {
                IsValidLevels = true;
            }
            else
            {
                IsValidLevels = false;
            }
        }

        private void UpdateDirection() => TradeDirection = Stop > Target ? Direction.Short : Direction.Long;

        private double _entry;
        private double _stop;
        private double _target;
        private bool _isValidLevels;
    }
}
