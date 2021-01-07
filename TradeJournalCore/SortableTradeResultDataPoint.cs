using System;

namespace TradeJournalCore
{
    public sealed class SortableTradeResultDataPoint
    {
        public DateTime CloseTime { get; }

        public double Profit { get; }

        public SortableTradeResultDataPoint(DateTime closeTime, double profit)
        {
            CloseTime = closeTime;
            Profit = profit;
        }
    }
}
