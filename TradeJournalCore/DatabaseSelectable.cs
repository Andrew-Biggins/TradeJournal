using System;
using System.Collections.Generic;
using System.Text;

namespace TradeJournalCore
{
    public abstract class DatabaseSelectable : Selectable
    {
        public int Id { get; set; }

        protected DatabaseSelectable(string name) : base(name)
        {
            
        }
    }
}
