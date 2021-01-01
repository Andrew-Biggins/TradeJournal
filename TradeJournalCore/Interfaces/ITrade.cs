using Common.Optional;

namespace TradeJournalCore.Interfaces
{
    public interface ITrade
    {
        IMarket Market { get; }

        ISelectable Strategy { get; }

        Levels Levels { get; }

        Execution Open { get; }

        Optional<Execution> Close { get; }

        Optional<double> MaxAdverseExcursion { get; }

        Optional<double> MaxFavourableExcursion { get; }

        TradeDirection Direction { get; }

        double RiskRewardRatio { get; }

        Optional<double> ResultInR { get; }

        Optional<double> PointsResult { get; }

        Optional<double> CashResult { get; }

        Optional<double> DrawdownPercentage { get; }

        Optional<double> RealisedProfitPercentage { get; }

        Optional<double> UnrealisedPointsProfit { get; }

        Optional<double> UnrealisedCashProfit { get; }
    }
}