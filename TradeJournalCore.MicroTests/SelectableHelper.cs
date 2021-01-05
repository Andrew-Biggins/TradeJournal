using System;
using System.Collections.Generic;
using System.Text;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore.MicroTests
{
    internal static class SelectableHelper
    {
        internal static bool IsAllSelected(IEnumerable<ISelectable> list)
        {
            var isAllSelected = true;

            foreach (var selectable in list)
            {
                if (!selectable.IsSelected)
                {
                    isAllSelected = false;
                }
            }

            return isAllSelected;
        }
    }
}
