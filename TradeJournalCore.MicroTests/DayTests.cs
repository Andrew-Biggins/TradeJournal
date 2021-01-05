using System;
using System.Collections.Generic;
using System.Text;
using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class DayTests
    {
        [Gwt("Given a day",
            "when the name is read",
            "it is the same as the one given at construction")]
        public void T0()
        {
            // Arrange
            var day = new Day(DayOfWeek.Thursday);

            // Act 
            var actual = day.Name;

            // Assert
            Assert.Equal("Thursday", actual);
        }

        [Gwt("Given a day",
            "when the is selected flag is read",
            "it is false by default")]
        public void T1()
        {
            // Arrange
            var day = new Day(DayOfWeek.Friday);

            // Act 
            var actual = day.IsSelected;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a day with a false is selected flag",
            "when the is selected flag is updated",
            "property changed is raised for is selected")]
        public void T2()
        {
            // Arrange
            var day = new Day(DayOfWeek.Friday);
            var catcher = Catcher.For(day);

            // Act 
            day.IsSelected = false;

            // Assert
            catcher.CaughtPropertyChanged(day, nameof(day.IsSelected));
        }
    }
}
