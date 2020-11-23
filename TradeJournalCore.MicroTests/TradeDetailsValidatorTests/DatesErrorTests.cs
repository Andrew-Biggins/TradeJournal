using System;
using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeDetailsValidatorTests
{
    public sealed class DatesErrorTests
    {
        [Gwt("Given a trade details validator",
            "when the dates error property is read",
            "the dates error property is false by default")]
        public void T0()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.DatesHaveError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator",
            "when invalid dates are verified",
            "property changed is raised for the dates error property")]
        public void T1()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            var catcher = Catcher.For(validator);

            // Act 
            validator.VerifyDates(new DateTime(2020, 11, 12, 20, 20, 00), new DateTime(2020, 11, 11, 12, 34, 00));

            // Assert
            catcher.CaughtPropertyChanged(validator, nameof(validator.DatesHaveError));
        }

        [Gwt("Given a trade details validator",
            "when invalid dates are verified",
            "the valid trade property is false")]
        public void T2()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.VerifyDates(new DateTime(2020, 11, 12, 20, 20, 00), new DateTime(2020, 11, 11, 12, 34, 00));

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator",
            "when invalid dates are verified",
            "the dates error property is true")]
        public void T3()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            validator.VerifyDates(new DateTime(2020, 11, 12, 20, 20, 00), new DateTime(2020, 11, 11, 12, 34, 00));

            // Assert
            Assert.True(validator.DatesHaveError);
        }

        [Gwt("Given a trade details validator",
            "when invalid times are verified",
            "the valid trade property is false")]
        public void T4()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.VerifyDates(new DateTime(2020, 11, 11, 20, 20, 00), new DateTime(2020, 11, 11, 12, 34, 00));

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator",
            "when invalid times are verified",
            "the dates error property is true")]
        public void T5()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.VerifyDates(new DateTime(2020, 11, 11, 20, 20, 00), new DateTime(2020, 11, 11, 12, 34, 00));

            // Assert
            Assert.True(validator.DatesHaveError);
        }

        [Gwt("Given a trade details validator",
            "when valid dates are verified",
            "the valid trade property is true")]
        public void T6()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.VerifyDates(new DateTime(2020, 11, 12, 20, 20, 00), new DateTime(2020, 11, 16, 12, 34, 00));

            // Assert
            Assert.True(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator",
            "when valid time are verified",
            "the valid trade property is true")]
        public void T7()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.VerifyDates(new DateTime(2020, 11, 11, 20, 20, 00), new DateTime(2020, 11, 11, 20, 34, 00));

            // Assert
            Assert.True(validator.IsTradeValid);
        }
    }
}
