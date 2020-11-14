using System;

namespace TradeJournalCore
{
    public sealed class Execution
    {
        public double Level { get; set; }

        public DateTime DateTime { get; set; }

        public double Size { get; set; }

        public Execution(double level, DateTime dateTime, double size)
        {
            Level = level;
            DateTime = dateTime;
            Size = size;
        }
    }
}
