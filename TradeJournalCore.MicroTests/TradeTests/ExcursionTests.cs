using System;
using Common.MicroTests;
using Common.Optional;
using Xunit;

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
            var trade = new Trade(new Market(string.Empty), new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<Excursion>(), Option.None<Excursion>()));

            // Act 
            var actual = trade.MaxAdverseExcursion;

            // Assert
            Assert.IsType<Option.OptionNone<Excursion>>(actual);
        }

        [Gwt("Given a trade with no maximum favourable excursion",
            "when the maximum favourable excursion is read",
            "the maximum favourable excursion is none")]
        public void T1()
        {
            // Arrange
            var trade = new Trade(new Market(string.Empty), new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<Excursion>(), Option.None<Excursion>()));

            // Act 
            var actual = trade.MaxFavourableExcursion;

            // Assert
            Assert.IsType<Option.OptionNone<Excursion>>(actual);
        }

        [Gwt("Given a trade with a maximum adverse excursion",
            "when the maximum adverse excursion is read",
            "the maximum adverse excursion is the same one used at construction")]
        public void T2()
        {
            // Arrange
            var testExcursion = new Excursion(123, 456);
            var trade = new Trade(new Market(string.Empty), new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.Some(testExcursion), Option.None<Excursion>()));
            var outExcursion = new Excursion();

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
            var testExcursion = new Excursion(456, 789);
            var trade = new Trade(new Market(string.Empty), new Strategy(string.Empty), new Levels(0, 0, 0),
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<Excursion>(), Option.Some(testExcursion)));
            var outExcursion = new Excursion();

            // Act 
            trade.MaxFavourableExcursion.IfExistsThen(x => { outExcursion = x; });

            // Assert
            Assert.Equal(testExcursion, outExcursion);
        }
    }
}
