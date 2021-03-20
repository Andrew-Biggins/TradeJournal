using Common.Optional;

namespace TradeJournalCore.Interfaces
{
    public interface ITrade 
    {
        int Id { get; set; }

        IMarket Market { get; }

        ISelectableTradeField Strategy { get; }

        Levels Levels { get; }

        Execution Open { get; }

        TradeDirection Direction { get; }

        double RiskRewardRatio { get; }

        Optional<Execution> Close { get; }

        Optional<double> MaxAdverseExcursion { get; }

        Optional<double> MaxFavourableExcursion { get; }

        Optional<double> High { get; }

        Optional<double> Low { get; }

        Optional<double> ResultInR { get; }

        Optional<double> PointsResult { get; }

        Optional<double> CashResult { get; }

        Optional<double> DrawdownPercentage { get; }

        Optional<double> RealisedProfitPercentage { get; }

        Optional<double> UnrealisedPointsProfit { get; }

        Optional<double> UnrealisedCashProfit { get; }

        EntryOrderType EntryOrderType { get; }
    }
}