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

        [Gwt("Given a strategy",
            "when the is selected flag is updated",
            "property changed is raised for is selected")]
        public void T2()
        {
            // Arrange
            var strategy = new Strategy(string.Empty);
            var catcher = Catcher.For(strategy);

            // Act 
            strategy.IsSelected = false;

            // Assert
            catcher.CaughtPropertyChanged(strategy, nameof(strategy.IsSelected));
        }
    }
}
