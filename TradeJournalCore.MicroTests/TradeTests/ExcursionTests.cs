using System;
using Common.MicroTests;
using Common.Optional;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class ExcursionTests
    {
        [Gwt("Given a trade with no maximum adverse excursion",
            "when the maximum adverse excursion is read",
            "the maximum adverse excursion is none")]
        public void T0()
        {
            // Arrange
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<double>(), Option.None<double>()));

            // Act 
            var actual = trade.MaxAdverseExcursion;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given a trade with no maximum favourable excursion",
            "when the maximum favourable excursion is read",
            "the maximum favourable excursion is none")]
        public void T1()
        {
            // Arrange
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<double>(), Option.None<double>()));

            // Act 
            var actual = trade.MaxFavourableExcursion;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given a trade with a maximum adverse excursion",
            "when the maximum adverse excursion is read",
            "the maximum adverse excursion is the same one used at construction")]
        public void T2()
        {
            // Arrange
            const double testExcursion = 123.56;
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.Some(testExcursion), Option.None<double>()));
            var outExcursion = 0.00;

            // Act 
            trade.MaxAdverseExcursion.IfExistsThen(x => { outExcursion = x; });

            // Assert
            Assert.Equal(testExcursion, outExcursion);
        }

        [Gwt("Given a trade with a maximum favourable excursion",
            "when the maximum favourable excursion is read",
            "the maximum adverse excursion is the same one used at construction")]
        public void T3()
        {
            // Arrange
            const double testExcursion = 789.23;
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<double>(), Option.Some(testExcursion)));
            var outExcursion = 0.00;

            // Act 
            trade.MaxFavourableExcursion.IfExistsThen(x => { outExcursion = x; });

            // Assert
            Assert.Equal(testExcursion, outExcursion);
        }
    }
}
