using Common.MicroTests;
using System;
using Xunit;

namespace TradeJournalWPF.MicroTests
{
    public sealed class RunnerTests
    {
        [Gwt("Given a null context",
            "when created",
            "an exception is thrown")]
        public void T0()
        {
            Assert.Throws<ArgumentNullException>("context", () => new Runner(null!));
        }
    }
}
