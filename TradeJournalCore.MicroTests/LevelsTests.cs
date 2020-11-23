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

        [Gwt("Given a Levels has a new entry set",
            "when the entry is read",
            "it is the new entry")]
        public void T3()
        {
            // Arrange
            var levels = new Levels(5, 0, 0);
            const double testEntry = 67.67;

            // Act 
            levels.Entry = testEntry;

            // Assert
            Assert.Equal(testEntry, levels.Entry);
        }

        [Gwt("Given a Levels has a new stop set",
            "when the stop is read",
            "it is the new stop")]
        public void T4()
        {
            // Arrange
            var levels = new Levels(0, 15, 0);
            const double testStop = 87.65;

            // Act 
            levels.Stop = testStop;

            // Assert
            Assert.Equal(testStop, levels.Stop);
        }

        [Gwt("Given a Levels has a new target set",
            "when the target is read",
            "it is the new target")]
        public void T5()
        {
            // Arrange
            var levels = new Levels(0, 0, 10);
            const double testTarget = 99.23;

            // Act 
            levels.Target = testTarget;

            // Assert
            Assert.Equal(testTarget, levels.Target);
        }

        //[Gwt("Given a Levels",
        //    "when a new entry is set",
        //    "property changed is raised for entry")]
        //public void T6()
        //{
        //    // Arrange
        //    var levels = new Levels(5, 0, 0);
        //    var catcher = Catcher.PropertyChanged;
        //    levels.PropertyChanged += catcher.Catch;

        //    // Act 
        //    levels.Entry = 1234.56;

        //    // Assert
        //    catcher.CaughtPropertyChanged(levels, nameof(levels.Entry));
        //}

        [Gwt("Given a Levels",
            "when a new stop is set",
            "property changed is raised for stop")]
        public void T7()
        {
            // Arrange
            var levels = new Levels(0, 0, 0);
            var catcher = Catcher.PropertyChanged;
            levels.PropertyChanged += catcher.Catch;

            // Act 
            levels.Stop = 1234.56;

            // Assert
            catcher.CaughtPropertyChanged(levels, nameof(levels.Stop));
        }

        [Gwt("Given a Levels",
            "when a new target is set",
            "property changed is raised for target")]
        public void T8()
        {
            // Arrange
            var levels = new Levels(0, 0, 0);
            var catcher = Catcher.PropertyChanged;
            levels.PropertyChanged += catcher.Catch;

            // Act 
            levels.Target = 1234.56;

            // Assert
            catcher.CaughtPropertyChanged(levels, nameof(levels.Target));
        }

        [Gwt("Given a Levels with a target greater than stop",
            "when the trade direction is read",
            "the trade direction is long")]
        public void T9()
        {
            // Arrange
            var levels = new Levels(100, 50, 150);

            // Act 
            var actual = levels.TradeDirection;

            // Assert
            Assert.Equal(Direction.Long, actual);
        }

        [Gwt("Given a Levels with a target less than stop",
            "when the trade direction is read",
            "the trade direction is long")]
        public void T10()
        {
            // Arrange
            var levels = new Levels(100, 150, 50);

            // Act 
            var actual = levels.TradeDirection;

            // Assert
            Assert.Equal(Direction.Short, actual);
        }

        [Gwt("Given a Levels with a target greater than the stop",
            "when the target is updated with a value less than the stop",
            "the trade direction is short")]
        public void T11()
        {
            // Arrange
            var levels = new Levels(100, 50, 200);
            levels.Stop = 50; // Needs to be set outside constructor to initialise to long and test the right code segment

            // Act 
            levels.Target = 10;

            // Assert
            Assert.Equal(Direction.Short, levels.TradeDirection);
        }

        [Gwt("Given a Levels with a stop greater than the target",
            "when the stop is updated with a value less than the target",
            "the trade direction is long")]
        public void T12()
        {
            // Arrange
            var levels = new Levels(150, 200, 100);

            // Act 
            levels.Stop = 50;

            // Assert
            Assert.Equal(Direction.Long, levels.TradeDirection);
        }

        [Gwt("Given a Levels has a stop set to the same value as entry",
            "when the valid levels property is read",
            "the valid levels property is false")]
        public void T13()
        {
            // Arrange
            var levels = new Levels(5, 1, 15);
            levels.Stop = 5;

            // Act 
            var actual = levels.IsValidLevels;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a Levels has a target greater entry and has a stop set to a value greater than entry",
            "when the valid levels property is read",
            "the valid levels property is false")]
        public void T14()
        {
            // Arrange
            var levels = new Levels(5, 1, 15);
            levels.Stop = 10;

            // Act 
            var actual = levels.IsValidLevels;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a Levels has a target set to the same value as entry",
            "when the valid levels property is read",
            "the valid levels property is false")]
        public void T15()
        {
            // Arrange
            var levels = new Levels(5, 1, 15);
            levels.Target = 5;

            // Act 
            var actual = levels.IsValidLevels;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a Levels has a stop greater entry and has a target set to a value greater than entry",
            "when the valid levels property is read",
            "the valid levels property is false")]
        public void T16()
        {
            // Arrange
            var levels = new Levels(5, 10, 1);
            levels.Target = 9;

            // Act 
            var actual = levels.IsValidLevels;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a Levels has an entry set greater than stop and target",
            "when the valid levels property is read",
            "the valid levels property is false")]
        public void T17()
        {
            // Arrange
            var levels = new Levels(5, 10, 1);
            levels.Entry = 11;

            // Act 
            var actual = levels.IsValidLevels;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a Levels has an entry set less than stop and target",
            "when the valid levels property is read",
            "the valid levels property is false")]
        public void T18()
        {
            // Arrange
            var levels = new Levels(5, 10, 3);
            levels.Entry = 2;

            // Act 
            var actual = levels.IsValidLevels;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a Levels has an target greater than entry and a stop less than entry",
            "when the valid levels property is read",
            "the valid levels property is true")]
        public void T19()
        {
            // Arrange
            var levels = new Levels(15, 10, 30);

            // Act 
            var actual = levels.IsValidLevels;

            // Assert
            Assert.True(actual);
        }

        [Gwt("Given a Levels has an target less than entry and a stop greater than entry",
            "when the valid levels property is read",
            "the valid levels property is true")]
        public void T20()
        {
            // Arrange
            var levels = new Levels(10, 20, 3);

            // Act 
            var actual = levels.IsValidLevels;

            // Assert
            Assert.True(actual);
        }

        [Gwt("Given a Levels has valid levels",
            "when a new target is set to invalidate the levels",
            "property changed is raised for the valid levels property")]
        public void T21()
        {
            // Arrange
            var levels = new Levels(10, 5, 20);
            var catcher = Catcher.PropertyChanged;
            levels.PropertyChanged += catcher.Catch;

            // Act 
            levels.Target = 2;

            // Assert
            catcher.CaughtPropertyChanged(levels, nameof(levels.IsValidLevels));
        }
    }
}
