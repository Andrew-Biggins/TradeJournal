using Common;
using System;
using Common.Optional;

namespace TradeJournalCore
{
    public sealed class TradeDetailsValidator : ViewModelBase
    {
        public bool MarketsHaveError
        {
            get => _marketsHaveError;
            set
            {
                SetProperty(ref _marketsHaveError, value, nameof(MarketsHaveError));
                VerifyInputs();
            }
        }

        public bool StrategiesHaveError
        {
            get => _strategiesHaveError;
            set
            {
                SetProperty(ref _strategiesHaveError, value, nameof(StrategiesHaveError));
                VerifyInputs();
            }
        }

        public bool EntryHasError
        {
            get => _entryHasError;
            set
            {
                _entryHasError = value;
                  VerifyInputs();
            }
        }

        public bool TargetHasError
        {
            get => _targetHasError;
            set
            {
                _targetHasError = value;
                VerifyInputs();
            }
        }

        public bool StopHasError
        {
            get => _stopHasError;
            set
            {
                _stopHasError = value;
                VerifyInputs();
            }
        }

        public bool OpenLevelHasError
        {
            get => _openLevelHasError;
            set
            {
                _openLevelHasError = value;
                VerifyInputs();
            }
        }

        public bool CloseLevelHasError
        {
            get => _closeLevelHasError;
            set
            {
                _closeLevelHasError = value;
                VerifyInputs();
            }
        }

        public bool SizeHasError
        {
            get => _sizeHasError;
            set
            {
                _sizeHasError = value;
                VerifyInputs();
            }
        }

        public bool DatesHaveError
        {
            get => _datesHaveError;
            private set
            {
                SetProperty(ref _datesHaveError, value, nameof(DatesHaveError));
                VerifyInputs();
            }
        }

        public bool HighHasError
        {
            get => _highHasError;
            set
            {
                SetProperty(ref _highHasError, value, nameof(HighHasError));
                VerifyInputs();
            }
        }

        public bool LowHasError
        {
            get => _lowHasError;
            set
            {
                SetProperty(ref _lowHasError, value, nameof(LowHasError));
                VerifyInputs();
            }
        }

        public double MaximumLow { get; private set; } = double.PositiveInfinity;

        public double MinimumHigh { get; private set; }

        public bool IsTradeValid
        {
            get => _isTradeValid;
            private set => SetProperty(ref _isTradeValid, value, nameof(IsTradeValid));
        }

        public void VerifyDates(DateTime open, DateTime close)
        {
            DatesHaveError = open > close;

            VerifyInputs();
        }

        internal void UpdateExcursionLimits(Direction tradeDirection, Optional<double> close, double open)
        {
            if (tradeDirection == Direction.Long)
            {
                CalculateLongMinimumHigh(close, open);
            }
            else
            {
                MinimumHigh = open;
            }

            MaximumLow = open;

            ValidateHigh(_high);
            ValidateLow(_low);
        }

        public void ValidateHigh(Optional<double> high)
        {
            _high = high;
            _high.IfExistsThen(x => { HighHasError = x < MinimumHigh; })
                 .IfEmpty(() => HighHasError = false);
        }

        public void ValidateLow(Optional<double> low)
        {
            _low = low;
            _low.IfExistsThen(x => { LowHasError = x > MaximumLow; })
                .IfEmpty(() => LowHasError = false);
        }

        private void CalculateLongMinimumHigh(Optional<double> close, double open)
        {
            close.IfExistsThen(x => MinimumHigh = x >= open ? x : open)
                 .IfEmpty(() => MinimumHigh = 0);
        }

        private void VerifyInputs()
        {
            if (MarketsHaveError || StrategiesHaveError || EntryHasError || 
                TargetHasError || StopHasError || OpenLevelHasError || 
                CloseLevelHasError || SizeHasError || DatesHaveError || 
                HighHasError || LowHasError)
            {
                IsTradeValid = false;
            }
            else
            {
                IsTradeValid = true;
            }
        }

        private bool _marketsHaveError;
        private bool _strategiesHaveError;
        private bool _entryHasError;
        private bool _targetHasError;
        private bool _stopHasError;
        private bool _openLevelHasError;
        private bool _closeLevelHasError;
        private bool _sizeHasError;
        private bool _lowHasError;
        private bool _highHasError;
        private bool _isTradeValid;
        private bool _datesHaveError;

        private Optional<double> _high = Option.None<double>();
        private Optional<double> _low = Option.None<double>();
    }
}
