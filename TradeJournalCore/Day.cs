using System;

namespace TradeJournalCore
{
    public sealed class Day : Selectable 
    {
        public Day(DayOfWeek day) : base(day.ToString())
        {
        }
    }
}
