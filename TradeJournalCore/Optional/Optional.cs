using System;
using System.Collections.Generic;
using System.Text;

namespace TradeJournalCore.Optional
{
    public abstract class Optional<T>
    {
        public abstract Optional<T> IfExistsThen(Action<T> action);

        public abstract void IfEmpty(Action whenNone);
    }
}
