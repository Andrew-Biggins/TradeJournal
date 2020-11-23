using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeDetailsValidatorTests
{
    public class LevelErrorTests
    {
        [Gwt("Given a trade details validator",
            "when the entry error property is read",
            "the entry error property is false by default")]
        public void T0()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.EntryHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator",
            "when the target error property is read",
            "the target error property is false by default")]
        public void T1()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.TargetHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator",
            "when the stop error property is read",
            "the stop error property is false by default")]
        public void T2()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.StopHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator has validated details",
            "when entry error is set to true",
            "the valid trade property is false")]
        public void T3()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.EntryHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator has validated details",
            "when target error is set to true",
            "the valid trade property is false")]
        public void T4()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.TargetHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator has validated details",
            "when stop error is set to true",
            "the valid trade property is false")]
        public void T5()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.StopHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }
    }
}