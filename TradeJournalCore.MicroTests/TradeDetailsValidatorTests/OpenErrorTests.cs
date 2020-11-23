using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeDetailsValidatorTests
{
    public class OpenErrorTests
    {
        [Gwt("Given a trade details validator",
            "when the open level error property is read",
            "the target open level property is false by default")]
        public void T0()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.OpenLevelHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator has validated details",
            "when open level error is set to true",
            "the valid trade property is false")]
        public void T1()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.OpenLevelHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator",
            "when the size error property is read",
            "the size error property is false by default")]
        public void T2()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.SizeHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator has validated details",
            "when size error is set to true",
            "the valid trade property is false")]
        public void T13()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.SizeHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }
    }
}