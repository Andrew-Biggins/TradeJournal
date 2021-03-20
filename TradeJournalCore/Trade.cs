using Common.Optional;
using System;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public enum TradeDirection
    {
        Long,
        Short,
        Both
    }

    public enum EntryOrderType 
    {
        Limit,
        Market,
        Both
    }

    public sealed class Trade : ITrade
    {
        public int Id { get; set; }

        public IMarket Market { get; }

        public ISelectableTradeField Strategy { get; }

        public Optional<double> MaxFavourableExcursion { get; private set; } = Option.None<double>();

        public Optional<double> MaxAdverseExcursion { get; private set; } = Option.None<double>();

        public Optional<double> High { get; }

        public Optional<double> Low { get; }

        public Levels Levels { get; }

        public Execution Open { get; }

        public Optional<Execution> Close { get; }

        public TradeDirection Direction { private set; get; }

        public double RiskRewardRatio { private set; get; }

        public Optional<double> ResultInR { get; private set; } = Option.None<double>();

        public Optional<double> PointsResult { get; private set; } = Option.None<double>();

        public Optional<double> CashResult { get; private set; } = Option.None<double>();

        public Optional<double> DrawdownPercentage { get; private set; } = Option.None<double>();

        public Optional<double> RealisedProfitPercentage { get; private set; } = Option.None<double>();

        public Optional<double> UnrealisedPointsProfit { get; private set; } = Option.None<double>();

        public Optional<double> UnrealisedCashProfit { get; private set; } = Option.None<double>();

        public EntryOrderType EntryOrderType { get; }

        public Trade(IMarket market, ISelectableTradeField strategy, Levels levels, Execution open,
            Optional<Execution> close, (Optional<double>, Optional<double>) excursions, EntryOrderType entryOrderType)
        {
            Market = market ?? throw new ArgumentNullException(nameof(market));
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            Levels = levels ?? throw new ArgumentNullException(nameof(levels));
            Open = open ?? throw new ArgumentNullException(nameof(open));
            Close = close ?? throw new ArgumentNullException(nameof(close));

            var (high, low) = excursions;
            High = high ?? throw new ArgumentNullException(nameof(high));
            Low = low ?? throw new ArgumentNullException(nameof(low));
            EntryOrderType = entryOrderType;

            SetDirection();
            
            CalculateRiskRewardRatio();
            CalculateResult();

            Close.IfExistsThen(x => CalculateExcursions());

            CalculateDrawdown();
            CalculateExcursionStats();
        }

        private void CalculateExcursions()
        {
            if (Direction == TradeDirection.Long)
            {
                Low.IfExistsThen(x => MaxAdverseExcursion = Option.Some((Open.Level - x) * (double)Market.PipDivisor));
                High.IfExistsThen(x => MaxFavourableExcursion = Option.Some((x - Open.Level) * (double)Market.PipDivisor));
            }

            if (Direction == TradeDirection.Short)
            {
                Low.IfExistsThen(x => MaxFavourableExcursion = Option.Some((Open.Level - x) * (double)Market.PipDivisor));
                High.IfExistsThen(x => MaxAdverseExcursion = Option.Some((x - Open.Level) * (double)Market.PipDivisor));
            }
        }

        private void SetDirection()
        {
            Direction = Levels.Target > Levels.Stop ? TradeDirection.Long : TradeDirection.Short;
        }

        private void CalculateRiskRewardRatio()
        {
            _stopSize = Math.Abs(Levels.Entry - Levels.Stop);
            RiskRewardRatio = Math.Abs(Levels.Entry - Levels.Target) / _stopSize;
        }

        private void CalculateResult()
        {
            Close.IfExistsThen(x =>
            {
                PointsResult = Direction == TradeDirection.Long
                    ? Option.Some((x.Level - Open.Level) * (double) Market.PipDivisor)
                    : Option.Some((Open.Level - x.Level) * (double) Market.PipDivisor);

                PointsResult.IfExistsThen(y =>
                {
                    ResultInR = Option.Some((y / (double) Market.PipDivisor) / _stopSize);
                    CashResult = Option.Some(Open.Size * y);
                });
            });
        }

        private void CalculateDrawdown()
        {
            MaxAdverseExcursion.IfExistsThen(x =>
            {
                DrawdownPercentage = Option.Some(x / _stopSize / (double) Market.PipDivisor);
            });
        }

        private void CalculateExcursionStats()
        {
            MaxFavourableExcursion.IfExistsThen(x =>
            {
                PointsResult.IfExistsThen(z =>
                {
                    if (z > 0)
                    {
                        RealisedProfitPercentage = Option.Some(z / x);
                        UnrealisedPointsProfit = Option.Some(x - z);
                    }
                    else
                    {
                        UnrealisedPointsProfit = Option.Some(x);
                        RealisedProfitPercentage = Option.Some(0.00);
                    }

                    UnrealisedPointsProfit.IfExistsThen(d =>
                    {
                        UnrealisedCashProfit = Option.Some(d * Open.Size);
                    });
                });
            });
        }

        private double _stopSize;
    }
}
