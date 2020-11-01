using System;

namespace TradeJournalCore
{
    public sealed class Execution
    {
        public double Level { get; }

        public DateTime DateTime { get; }

        public double Size { get; }

        public Execution(double level, DateTime dateTime, double size)
        {
            Level = level;
            DateTime = dateTime;
            Size = size;
        }
    }
}
