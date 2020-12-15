using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class Creation
    {
        [Gwt("Given a null levels",
            "when created",
            "an exception is thrown")]
        public void T0()
        {
            Assert.Throws<ArgumentNullException>("market",
                () => new Trade(null!, new Strategy(string.Empty), new Levels(0, 0, 0),
                    new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                    (Option.None<Excursion>(), Option.None<Excursion>())));
        }

        [Gwt("Given a null levels",
            "when created",
            "an exception is thrown")]
        public void T1()
        {
            Assert.Throws<ArgumentNullException>("strategy",
                () => new Trade(TestMarket, null!, new Levels(0, 0, 0),
                    new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                    (Option.None<Excursion>(), Option.None<Excursion>())));
        }

        [Gwt("Given a null levels",
            "when created",
            "an exception is thrown")]
        public void T2()
        {
            Assert.Throws<ArgumentNullException>("levels",
                () => new Trade(TestMarket, new Strategy(string.Empty), null!,
                    new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                    (Option.None<Excursion>(), Option.None<Excursion>())));
        }

        [Gwt("Given a null open",
            "when created",
            "an exception is thrown")]
        public void T3()
        {
            Assert.Throws<ArgumentNullException>("open",
                () => new Trade(TestMarket, new Strategy(string.Empty), new Levels(0, 0, 0), null!,
                    Option.None<Execution>(), (Option.None<Excursion>(), Option.None<Excursion>())));
        }

        [Gwt("Given a null maximum adverse excursion",
            "when created",
            "an exception is thrown")]
        public void T4()
        {
            Assert.Throws<ArgumentNullException>("adverse",
                () => new Trade(TestMarket, new Strategy(string.Empty), new Levels(0, 0, 0),
                    new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                    (null!, Option.None<Excursion>())));
        }

        [Gwt("Given a null maximum favourable excursion",
            "when created",
            "an exception is thrown")]
        public void T5()
        {
            Assert.Throws<ArgumentNullException>("favourable",
                () => new Trade(TestMarket, new Strategy(string.Empty), new Levels(0, 0, 0),
                    new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                    (Option.None<Excursion>(), null!)));
        }
    }
}
