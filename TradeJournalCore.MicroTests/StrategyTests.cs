using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class StrategyTests
    {
        [Gwt("Given a strategy",
            "when the name is read",
            "it is the same as the one given at construction")]
        public void T0()
        {
            // Arrange
            const string testName = "test name";
            var strategy = new Strategy(testName);

            // Act 
            var actual = strategy.Name;

            // Assert
            Assert.Equal(testName, actual);
        }

        [Gwt("Given a strategy",
            "when the is selected flag is read",
            "it is false by default")]
        public void T1()
        {
            // Arrange
            var strategy = new Strategy(string.Empty);

            // Act 
            var actual = strategy.IsSelected;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a strategy with a false is selected flag",
            "when the is selected flag is set to false again",
            "the is selected flag is true")]
        public void T2()
        {
            // Arrange
            var strategy = new Strategy(string.Empty);

            // Act 
            strategy.IsSelected = false;

            // Assert
            Assert.True(strategy.IsSelected);
        }

        [Gwt("Given a strategy with a true is selected flag",
            "when the is selected flag is set to false ",
            "the is selected flag is false")]
        public void T3()
        {
            // Arrange
            var strategy = new Strategy(string.Empty);
            strategy.IsSelected = true;

            // Act 
            strategy.IsSelected = false;

            // Assert
            Assert.False(strategy.IsSelected);
        }
    }
}
