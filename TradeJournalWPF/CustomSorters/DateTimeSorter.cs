using System.Collections;
using System.ComponentModel;
using TradeJournalCore.Interfaces;

namespace TradeJournalWPF.CustomSorters
{
    public class DateTimeSorter : IComparer
    {
        public DateTimeSorter(ListSortDirection direction)
        {
            _direction = direction;
        }

        public int Compare(object x, object y)
        {
            var result = 0;

            var tradeX = (ITrade)x;
            var tradeY = (ITrade)y;

            if (tradeX.Open.Date.CompareTo(tradeY.Open.Date) < 0 )
            {
                result = -1;
            }

            if (tradeX.Open.Date.CompareTo(tradeY.Open.Date) > 0)
            {
                result = 1;
            }

            if (_direction == ListSortDirection.Ascending)
            {
                result *= -1;
            }

            return result;
        }

        private readonly ListSortDirection _direction;
    }
}
