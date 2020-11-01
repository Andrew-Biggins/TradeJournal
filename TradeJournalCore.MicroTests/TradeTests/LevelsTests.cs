using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class LevelsTests
    {
        [Gwt("Given a trade",
            "when the levels are read",
            "the levels are the same as the ones given at construction")]
        public void T0()
        {
            // Arrange
            var testLevels = new Levels(1, 2, 3);
            var trade = new Trade(new Market(string.Empty), new Strategy(string.Empty), testLevels,
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<Excursion>(), Option.None<Excursion>()));

            // Act 
            var actual = trade.Levels;

            // Assert
            Assert.Equal(testLevels, actual);
        }
    }
}
