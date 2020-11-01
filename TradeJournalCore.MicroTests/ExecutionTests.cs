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
            "the date time is the same as the one given at construction")]
        public void T1()
        {
            // Arrange
            var testDateTime = DateTime.Today;
            var execution = new Execution(666, testDateTime, 453);

            // Act 
            var actual = execution.DateTime;

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
    }
}
