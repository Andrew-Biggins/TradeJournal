using Common;
using System;
using Common.Optional;

namespace TradeJournalCore
{
    public sealed class TradeDetailsValidator : ViewModelBase
    {
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

        public bool MaeHasError
        {
            get => _maeHasError;
            set
            {
                SetProperty(ref _maeHasError, value, nameof(MaeHasError));
                VerifyInputs();
            }
        }

        public bool MfeHasError
        {
            get => _mfeHasError;
            set
            {
                SetProperty(ref _mfeHasError, value, nameof(MfeHasError));
                VerifyInputs();
            }
        }

        public double MaximumMae { get; private set; } = double.PositiveInfinity;

        public double MinimumMfe { get; private set; }

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
                CalculateLongMaximumMae(close, open);
                CalculateLongMinimumMfe(close, open);
            }
            else
            {
                CalculateShortMaximumMae(close, open);
                CalculateShortMinimumMfe(close, open);
            }

            ValidateMae(_mae);
            ValidateMfe(_mfe);
        }

        private void CalculateLongMaximumMae(Optional<double> close, double open)
        {
            close.IfExistsThen(x =>
            {
                if (x > open)
                {
                    MaximumMae = open;
                }
                else
                {
                    MaximumMae = open - x;
                }

            }).IfEmpty(() => { MaximumMae = open; });
        }

        private void CalculateLongMinimumMfe(Optional<double> close, double open)
        {
            close.IfExistsThen(x =>
            {
                if (x > open)
                {
                    MinimumMfe = x - open;
                }
                else
                {
                    MinimumMfe = 0;
                }
            }).IfEmpty(() => { MinimumMfe = 0; });
        }

        private void CalculateShortMaximumMae(Optional<double> close, double open)
        {
            close.IfExistsThen(x =>
            {
                if (x < open)
                {
                    MaximumMae = double.PositiveInfinity;
                }
                else
                {
                    MaximumMae = x - open;
                }

            }).IfEmpty(() => { MaximumMae = double.PositiveInfinity; });
        }

        private void CalculateShortMinimumMfe(Optional<double> close, double open)
        {
            close.IfExistsThen(x =>
            {
                if (x < open)
                {
                    MinimumMfe = open - x;
                }
                else
                {
                    MinimumMfe = 0;
                }
            }).IfEmpty(() => { MinimumMfe = 0; });
        }

        public void ValidateMae(Optional<Excursion> mae)
        {
            _mae = mae;
            _mae.IfExistsThen(x => { x.Points.IfExistsThen(y => { MaeHasError = y > MaximumMae; }); })
                .IfEmpty(() => MaeHasError = false); 
        }

        public void ValidateMfe(Optional<Excursion> mfe)
        {
            _mfe = mfe;
            _mfe.IfExistsThen(x => { x.Points.IfExistsThen(y => { MfeHasError = y < MinimumMfe; }); })
                .IfEmpty(() => MfeHasError = false);
        }

        private void VerifyInputs()
        {
            if (EntryHasError || TargetHasError || StopHasError ||
                OpenLevelHasError || CloseLevelHasError || SizeHasError
                || DatesHaveError || MaeHasError || MfeHasError)
            {
                IsTradeValid = false;
            }
            else
            {
                IsTradeValid = true;
            }
        }

        private bool _entryHasError;
        private bool _targetHasError;
        private bool _stopHasError;
        private bool _openLevelHasError;
        private bool _closeLevelHasError;
        private bool _sizeHasError;
        private bool _mfeHasError;
        private bool _maeHasError;
        private bool _isTradeValid;
        private bool _datesHaveError;

        private Optional<Excursion> _mae = Option.None<Excursion>();
        private Optional<Excursion> _mfe = Option.None<Excursion>();
    }
}
