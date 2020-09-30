using System;

namespace TradeJournalCore.Optional
{
    public static partial class Option
    {
        internal class OptionNone<T> : Optional<T>
        {
            public override Optional<T> IfExistsThen(Action<T> action) => this;

            public override void IfEmpty(Action whenNone) => whenNone();
        }
    }
}
