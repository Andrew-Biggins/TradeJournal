using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class LevelsTests
    {
        [Gwt("Given a Levels",
            "when entry is read",
            "it is the same as the entry given at construction")]
        public void T0()
        {
            // Arrange
            var levels = new Levels(5, 0, 0);

            // Act 
            var actual = levels.Entry;

            // Assert
            Assert.Equal(5, actual);
        }

        [Gwt("Given a Levels",
            "when stop is read",
            "it is the same as the stop given at construction")]
        public void T1()
        {
            // Arrange
            var levels = new Levels(0, 63, 0);

            // Act 
            var actual = levels.Stop;

            // Assert
            Assert.Equal(63, actual);
        }

        [Gwt("Given a Levels",
            "when stop is read",
            "it is the same as the stop given at construction")]
        public void T2()
        {
            // Arrange
            var levels = new Levels(0, 0, 123);

            // Act 
            var actual = levels.Target;

            // Assert
            Assert.Equal(123, actual);
        }
    }
}
