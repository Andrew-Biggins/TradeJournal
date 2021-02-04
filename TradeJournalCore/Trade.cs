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

    public sealed class Trade : ITrade
    {
        public int Id { get; set; }

        public IMarket Market { get; }

        public ISelectable Strategy { get; }

        public Optional<double> MaxFavourableExcursion { get; }

        public Optional<double> MaxAdverseExcursion { get; }

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

        public Trade(IMarket market, ISelectable strategy, Levels levels, Execution open, Optional<Execution> close,
            (Optional<double>, Optional<double>) excursions)
        {
            Market = market ?? throw new ArgumentNullException(nameof(market));
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            Levels = levels ?? throw new ArgumentNullException(nameof(levels));
            Open = open ?? throw new ArgumentNullException(nameof(open));
            Close = close ?? throw new ArgumentNullException(nameof(close));

            var (adverse, favourable) = excursions;
            MaxAdverseExcursion = adverse ?? throw new ArgumentNullException(nameof(adverse));
            MaxFavourableExcursion = favourable ?? throw new ArgumentNullException(nameof(favourable));

            SetDirection();
            
            CalculateRiskRewardRatio();
            CalculateResult();
            CalculateDrawdown();
            CalculateExcursionStats();
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
                    ? Option.Some(x.Level - Open.Level)
                    : Option.Some(Open.Level - x.Level);

                PointsResult.IfExistsThen(y =>
                {
                    ResultInR = Option.Some(y / _stopSize);
                    CashResult = Option.Some(Open.Size * y);
                });
            });
        }

        private void CalculateDrawdown()
        {
            MaxAdverseExcursion.IfExistsThen(x =>
            {
                DrawdownPercentage = Option.Some(x / _stopSize);
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
