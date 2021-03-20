using System;
using Common.MicroTests;
using Common.Optional;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class ExcursionTests
    {
        [Gwt("Given a long trade with no low",
            "when the maximum adverse excursion is read",
            "the maximum adverse excursion is none")]
        public void T0()
        {
            // Arrange
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 30),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<double>(), Option.None<double>()), EntryOrderType.Limit);

            // Act 
            var actual = trade.MaxAdverseExcursion;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given a long trade with no high",
            "when the maximum favourable excursion is read",
            "the maximum favourable excursion is none")]
        public void T1()
        {
            // Arrange
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 30),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<double>(), Option.None<double>()), EntryOrderType.Limit);

            // Act 
            var actual = trade.MaxFavourableExcursion;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given a long trade with a low",
            "when the maximum adverse excursion is read",
            "the maximum adverse excursion is correct")]
        public void T2()
        {
            // Arrange
            const double open = 123.56;
            const double low = 100.00;
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 30),
                new Execution(open, DateTime.MaxValue, 0), Option.Some(TestClose),
                (Option.None<double>(), Option.Some(low)), EntryOrderType.Limit);
            var outExcursion = 0.00;

            // Act 
            trade.MaxAdverseExcursion.IfExistsThen(x => { outExcursion = x; });

            // Assert
            Assert.Equal(open - low, outExcursion);
        }

        [Gwt("Given a trade with a high",
            "when the maximum favourable excursion is read",
            "the maximum adverse excursion is correct")]
        public void T3()
        {
            // Arrange
            const double open = 789.23;
            const double high = 889.73;
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(20, 10, 30),
                new Execution(open, DateTime.MaxValue, 0), Option.Some(TestClose),
                (Option.Some(high), Option.None<double>()), EntryOrderType.Limit);
            var outExcursion = 0.00;

            // Act 
            trade.MaxFavourableExcursion.IfExistsThen(x => { outExcursion = x; });

            // Assert
            Assert.Equal(high - open, outExcursion);
        }
    }
}
