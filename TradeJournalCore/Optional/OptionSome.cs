using System;

namespace TradeJournalCore.Optional
{
    public static partial class Option
    {
        internal class OptionSome<T> : Optional<T>
        {
            internal OptionSome(T content) => _content = content;

            public override Optional<T> IfExistsThen(Action<T> action)
            {
                action(_content);
                return this;
            }

            public override void IfEmpty(Action whenNone) { }

            private readonly T _content;
        }
    }
}
