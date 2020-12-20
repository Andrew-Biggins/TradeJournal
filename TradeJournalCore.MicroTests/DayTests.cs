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
            "when the is selected flag is set to false again",
            "the is selected flag is true")]
        public void T2()
        {
            // Arrange
            var day = new Day(DayOfWeek.Friday);

            // Act 
            day.IsSelected = false;

            // Assert
            Assert.True(day.IsSelected);
        }

        [Gwt("Given an asset type with a true is selected flag",
            "when the is selected flag is set to false ",
            "the is selected flag is false")]
        public void T3()
        {
            // Arrange
            var day = new Day(DayOfWeek.Friday);
            day.IsSelected = true;

            // Act 
            day.IsSelected = false;

            // Assert
            Assert.False(day.IsSelected);
        }
    }
}
