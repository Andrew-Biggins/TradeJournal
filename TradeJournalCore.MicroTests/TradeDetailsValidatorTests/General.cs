using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeDetailsValidatorTests
{
    public sealed class General
    {
        [Gwt("Given a trade details validator has an error",
            "when the error is cleared",
            "the valid trade property is true")]
        public void T0()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = true;

            // Act 
            validator.EntryHasError = false;

            // Assert
            Assert.True(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator has an error",
            "when the error is cleared",
            "property changed is raised for the valid trade property")]
        public void T1()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = true;
            var catcher = Catcher.For(validator);

            // Act 
            validator.EntryHasError = false;

            // Assert
            catcher.CaughtPropertyChanged(validator, nameof(validator.IsTradeValid));
        }
    }
}
