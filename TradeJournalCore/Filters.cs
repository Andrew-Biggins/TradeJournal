using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Filters : IFilters
    {
        public IReadOnlyList<ISelectable> Markets { get; }

        public IReadOnlyList<ISelectable> Strategies { get; } 

        public IReadOnlyList<ISelectable> AssetClasses { get; }

        public IReadOnlyList<ISelectable> Days { get; } 

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public double MinRiskRewardRatio { get; }

        public double MaxRiskRewardRatio { get; }

        public TradeStatus Status { get; }

        public TradeDirection Direction { get; }

        public EntryOrderType OrderType { get; }

        public Filters(IReadOnlyList<ISelectable> markets, IReadOnlyList<ISelectable> strategies,
            IReadOnlyList<ISelectable> assetClasses, IReadOnlyList<ISelectable> days, DateTime startDate,
            DateTime endDate, DateTime startTime, DateTime endTime, double minRiskRewardRatio,
            double maxRiskRewardRatio, TradeStatus status, TradeDirection direction, EntryOrderType orderType)
        {
            Markets = markets;
            Strategies = strategies;
            AssetClasses = assetClasses;
            Days = days;
            StartDate = startDate;
            EndDate = endDate;
            StartTime = startTime;
            EndTime = endTime;
            MinRiskRewardRatio = minRiskRewardRatio;
            MaxRiskRewardRatio = maxRiskRewardRatio;
            Status = status;
            Direction = direction;
            OrderType = orderType;
        }
    }
}
