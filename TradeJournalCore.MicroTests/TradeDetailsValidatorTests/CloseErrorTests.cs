using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeDetailsValidatorTests
{
    public class CloseErrorTests
    {
        [Gwt("Given a trade details validator",
            "when the close level error property is read",
            "the close level error property is false by default")]
        public void T0()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.CloseLevelHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator has validated details",
            "when close level error is set to true",
            "the valid trade property is false")]
        public void T1()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.CloseLevelHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }
    }
}