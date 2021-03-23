using System;

namespace TradeJournalCore
{
    public static class DateTimeHelper
    {
        public static DateTime CombineDateTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }
    }
}