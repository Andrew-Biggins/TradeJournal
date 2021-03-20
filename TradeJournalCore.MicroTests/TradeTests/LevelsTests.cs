using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class LevelsTests
    {
        [Gwt("Given a trade",
            "when the levels are read",
            "then the levels are the same as the ones given at construction")]
        public void T0()
        {
            // Arrange
            var testLevels = new Levels(1, 2, 3);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), testLevels,
                new Execution(0, DateTime.MaxValue, 0), Option.None<Execution>(),
                (Option.None<double>(), Option.None<double>()), EntryOrderType.Limit);

            // Act 
            var actual = trade.Levels;

            // Assert
            Assert.Equal(testLevels, actual);
        }
    }
}
