using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Common.Optional;
using TradeJournalCore;
using TradeJournalCore.Interfaces;

namespace TradeJournalWPF.CustomSorters
{
    public sealed class OptionalExecutionSorter : IComparer
    {
        public OptionalExecutionSorter(ListSortDirection direction, TradeListColumn column)
        {
            _direction = direction;
            _column = column;
        }

        public int Compare(object x, object y)
        {
            var result = 0;

            var tradeX = (ITrade)x;
            var tradeY = (ITrade)y;

            tradeX.Close.IfExistsThen(xClose =>
            {
                tradeY.Close.IfExistsThen(yClose =>
                {
                    switch (_column)
                    {
                        case TradeListColumn.CloseLevel:
                        {
                            if (xClose.Level.CompareTo(yClose.Level) < 0)
                            {
                                result = -1;
                            }

                            if (xClose.Level.CompareTo(yClose.Level) > 0)
                            {
                                result = 1;
                            }

                            break;
                        }
                        case TradeListColumn.CloseTime:
                        {
                            if (xClose.DateTime.CompareTo(yClose.DateTime) < 0)
                            {
                                result = -1;
                            }

                            if (xClose.DateTime.CompareTo(yClose.DateTime) > 0)
                            {
                                result = 1;
                            }

                            break;
                        }
                    }
                }).IfEmpty(() => { result = -1; });
            }).IfEmpty(() =>
            {
                tradeY.Close.IfExistsThen(pp =>
                {
                    result = 1;
                });
            });

            if (_direction == ListSortDirection.Ascending)
            {
                result *= -1;
            }

            return result;
        }

        private readonly ListSortDirection _direction;
        private readonly TradeListColumn _column;
    }
}
