using Common.MicroTests;
using Common.Optional;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeDetailsValidatorTests
{
    public class ExcursionErrorTests
    {
        [Gwt("Given a trade details validator",
            "when the high error property is read",
            "the high error property is false by default")]
        public void T0()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.HighHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator",
            "when the low error property is read",
            "the low error property is false by default")]
        public void T1()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.LowHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator has validated details",
            "when high error is set to true",
            "the valid trade property is false")]
        public void T2()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.HighHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator has validated details",
            "when low error is set to true",
            "the valid trade property is false")]
        public void T3()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.LowHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator has validated details",
            "when high error is set to true",
            "property changed is raised for the MAE error property")]
        public void T4()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            var catcher = Catcher.For(validator);

            // Act 
            validator.HighHasError = true;

            // Assert
            catcher.CaughtPropertyChanged(validator, nameof(validator.HighHasError));
        }

        [Gwt("Given a trade details validator has validated details",
            "when low error is set to true",
            "property changed is raised for the MAE error property")]
        public void T5()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            var catcher = Catcher.For(validator);

            // Act 
            validator.LowHasError = true;

            // Assert
            catcher.CaughtPropertyChanged(validator, nameof(validator.LowHasError));
        }

        [Gwt("Given a trade details validator has an high error",
            "when an option none high is validated",
            "the high error is false")]
        public void T6()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.HighHasError = true;

            // Act 
            validator.ValidateHigh(Option.None<double>());

            // Assert
            Assert.False(validator.HighHasError);
        }

        [Gwt("Given a trade details validator has an low error",
            "when an option none low is validated",
            "the MFE error is false")]
        public void T7()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.LowHasError = true;

            // Act 
            validator.ValidateLow(Option.None<double>());

            // Assert
            Assert.False(validator.LowHasError);
        }

        [Gwt("Given a trade details validator has a minimum high set",
            "when a high above the limit is validated",
            "the high error is true")]
        public void T8()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, Option.Some(100.00), 200);

            // Act 
            validator.ValidateHigh(Option.Some(199.00));

            // Assert
            Assert.True(validator.HighHasError);
        }

        [Gwt("Given a trade details validator has a maximum high limit and an high error",
            "when an high below the limit is validated",
            "the high error is false")]
        public void T11()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(100), 200);
            validator.HighHasError = true;

            // Act 
            validator.ValidateHigh(Option.Some(201.00));

            // Assert
            Assert.False(validator.HighHasError);
        }

        [Gwt("Given a trade details validator has an maximum low limit",
            "when a low below the limit is validated",
            "the low error is true")]
        public void T12()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, Option.Some(200.00), 100);

            // Act 
            validator.ValidateLow(Option.Some(101.00));

            // Assert
            Assert.True(validator.LowHasError);
        }

        [Gwt("Given a trade details validator has an maximum low limit and a low error",
            "when an low above the limit is validated",
            "the MFE error is false")]
        public void T13()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(200), 100);
            validator.LowHasError = true;

            // Act 
            validator.ValidateLow(Option.Some(99.00));

            // Assert
            Assert.False(validator.HighHasError);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a winning long trade",
            "the maximum low is the same as the open level")]
        public void T14()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, Option.Some(300.00), testOpen);

            // Assert
            Assert.Equal(testOpen, validator.MaximumLow);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a losing short trade",
            "the minimum high is equal to the open")]
        public void T15()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;

            // Act 
            validator.UpdateExcursionLimits(Direction.Short, Option.Some(350.00), testOpen);

            // Assert
            Assert.Equal(testOpen, validator.MinimumHigh);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a long trade with no close",
            "the minimum high is equal to the open level")]
        public void T16()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, Option.None<double>(), 200.00);

            // Assert
            Assert.Equal(0, validator.MinimumHigh);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a winning long trade",
            "the maximum low is equal to the open")]
        public void T17()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, Option.Some(350.00), testOpen);

            // Assert
            Assert.Equal(testOpen, validator.MaximumLow);
        }
    }
}