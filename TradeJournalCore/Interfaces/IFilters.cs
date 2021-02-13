using System;
using System.Collections.Generic;

namespace TradeJournalCore.Interfaces
{
    public interface IFilters
    {
        IReadOnlyList<ISelectable> Markets { get; }

        IReadOnlyList<ISelectable> Strategies { get; }

        IReadOnlyList<ISelectable> AssetClasses { get; }

        IReadOnlyList<ISelectable> Days { get; }

        DateTime StartDate { get; }

        DateTime EndDate { get; }

        DateTime StartTime { get; }

        DateTime EndTime { get; }

        double MinRiskRewardRatio { get; }

        double MaxRiskRewardRatio { get; }

        TradeStatus Status { get; }

        TradeDirection Direction { get; }

        EntryOrderType OrderType { get; }
    }
}