using Common.MicroTests;
using System;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class ExecutionTests
    {
        [Gwt("Given an execution",
            "when the level is read",
            "the level is the same as the one given at construction")]
        public void T0()
        {
            // Arrange
            const int testLevel = 555;
            var execution = new Execution(testLevel, DateTime.MaxValue, 453);

            // Act 
            var actual = execution.Level;

            // Assert
            Assert.Equal(testLevel, actual);
        }

        [Gwt("Given an execution",
            "when the date time is read",
            "the date is the same as the one given at construction")]
        public void T1()
        {
            // Arrange
            var testDateTime = DateTime.Today;
            var execution = new Execution(666, testDateTime, 453);

            // Act 
            var actual = execution.Date;

            // Assert
            Assert.Equal(testDateTime, actual);
        }

        [Gwt("Given an execution",
            "when the level is read",
            "the level is the same as the one given at construction")]
        public void T2()
        {
            // Arrange
            const int testSize = 321;
            var execution = new Execution(789, DateTime.MaxValue, testSize);

            // Act 
            var actual = execution.Size;

            // Assert
            Assert.Equal(testSize, actual);
        }

        [Gwt("Given an execution",
            "when the date time is set",
            "the property changed changed event is raised for date")]
        public void T3()
        {
            // Arrange
            var execution = new Execution(789, DateTime.MaxValue, 123);
            var catcher = Catcher.Simple;
            execution.PropertyChanged += catcher.Catch;

            // Act 
            execution.Date = DateTime.MinValue;

            // Assert
            catcher.CaughtPropertyChanged(execution, nameof(execution.Date));
        }

        [Gwt("Given an execution",
            "when the level is set",
            "the property changed changed event is raised for level")]
        public void T4()
        {
            // Arrange
            var execution = new Execution(789, DateTime.MaxValue, 123);
            var catcher = Catcher.Simple;
            execution.PropertyChanged += catcher.Catch;

            // Act 
            execution.Level = 555;

            // Assert
            catcher.CaughtPropertyChanged(execution, nameof(execution.Level));
        }

        [Gwt("Given an execution has a new level set",
            "when the level is read",
            "the level is the new level")]
        public void T5()
        {
            // Arrange
            var execution = new Execution(789, DateTime.MaxValue, 1);
            const int testLevel = 321;

            // Act 
            execution.Level = testLevel;

            // Assert
            Assert.Equal(testLevel, execution.Level);
        }

        [Gwt("Given an execution has a new date time set",
            "when the date is read",
            "the date is the new level")]
        public void T6()
        {
            // Arrange
            var execution = new Execution(789, DateTime.MaxValue, 1);
            var testDate = DateTime.Today;

            // Act 
            execution.Date = testDate;

            // Assert
            Assert.Equal(testDate, execution.Date);
        }

        [Gwt("Given an execution",
            "when the date time is set with the same date",
            "the property changed changed event is not raised")]
        public void T7()
        {
            // Arrange
            var execution = new Execution(789, DateTime.MaxValue, 123);
            var catcher = Catcher.Simple;
            execution.PropertyChanged += catcher.Catch;

            // Act 
            execution.Date = DateTime.MaxValue;

            // Assert
            catcher.DidNotCatchPropertyChanged(execution, nameof(execution.Date));
        }

        [Gwt("Given an execution",
            "when the level is set with the same level",
            "the property changed changed event is not raised")]
        public void T8()
        {
            // Arrange
            var execution = new Execution(789, DateTime.MaxValue, 123);
            var catcher = Catcher.Simple;
            execution.PropertyChanged += catcher.Catch;

            // Act 
            execution.Level = 789;

            // Assert
            catcher.DidNotCatchPropertyChanged(execution, nameof(execution.Level));
        }
    }
}
