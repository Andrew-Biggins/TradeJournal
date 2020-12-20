using Common.MicroTests;
using Common.Optional;
using System;
using Xunit;
using static TradeJournalCore.MicroTests.Shared;

namespace TradeJournalCore.MicroTests.TradeTests
{
    public sealed class CloseTests
    {
        [Gwt("Given a trade",
            "when the open is read",
            "the open is the same as the one given at construction")]
        public void T0()
        {
            // Arrange
            var testClose = new Execution(70, DateTime.MaxValue, 8);
            var trade = new Trade(TestMarket, new Strategy(string.Empty), new Levels(1, 2, 3),
                new Execution(0, DateTime.MinValue, 0), Option.Some(testClose), (Option.None<double>(), Option.None<double>()));
            var outExecution = new Execution(0, DateTime.MinValue, 0);

            // Act 
            trade.Close.IfExistsThen(x => { outExecution = x; });

            // Assert
            Assert.Equal(testClose, outExecution);
        }
    }
}
