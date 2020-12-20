using Common.MicroTests;
using Common.Optional;
using Xunit;

namespace TradeJournalCore.MicroTests.TradeDetailsValidatorTests
{
    public class ExcursionErrorTests
    {
        [Gwt("Given a trade details validator",
            "when the MAE error property is read",
            "the MAE error property is false by default")]
        public void T0()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.MaeHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator",
            "when the MFE error property is read",
            "the MFE error property is false by default")]
        public void T1()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.MfeHasError;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given a trade details validator has validated details",
            "when MAE error is set to true",
            "the valid trade property is false")]
        public void T2()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.MaeHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator has validated details",
            "when MFE error is set to true",
            "the valid trade property is false")]
        public void T3()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.EntryHasError = false; // Call the verify method

            // Act 
            validator.MfeHasError = true;

            // Assert
            Assert.False(validator.IsTradeValid);
        }

        [Gwt("Given a trade details validator has validated details",
            "when MAE error is set to true",
            "property changed is raised for the MAE error property")]
        public void T4()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            var catcher = Catcher.For(validator);

            // Act 
            validator.MaeHasError = true;

            // Assert
            catcher.CaughtPropertyChanged(validator, nameof(validator.MaeHasError));
        }

        [Gwt("Given a trade details validator has validated details",
            "when MFE error is set to true",
            "property changed is raised for the MAE error property")]
        public void T5()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            var catcher = Catcher.For(validator);

            // Act 
            validator.MfeHasError = true;

            // Assert
            catcher.CaughtPropertyChanged(validator, nameof(validator.MfeHasError));
        }

        [Gwt("Given a trade details validator",
            "when the maximum MAE is read",
            "the maximum MAE is positive infinity by default")]
        public void T6()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.MaximumMae;

            // Assert
            Assert.Equal(double.PositiveInfinity, actual);
        }

        [Gwt("Given a trade details validator",
            "when the minimum MFE is read",
            "the minimum MFE is zero by default")]
        public void T7()
        {
            // Arrange
            var validator = new TradeDetailsValidator();

            // Act 
            var actual = validator.MinimumMfe;

            // Assert
            Assert.Equal(0, actual);
        }

        [Gwt("Given a trade details validator has an MAE error",
            "when an option none MAE is validated",
            "the MAE error is false")]
        public void T8()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.MaeHasError = true;

            // Act 
            validator.ValidateMae(Option.None<double>());

            // Assert
            Assert.False(validator.MaeHasError);
        }

        [Gwt("Given a trade details validator has an MAE error",
            "when an option none MFE is validated",
            "the MFE error is false")]
        public void T9()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.MfeHasError = true;

            // Act 
            validator.ValidateMfe(Option.None<double>());

            // Assert
            Assert.False(validator.MfeHasError);
        }

        [Gwt("Given a trade details validator has an MAE limit",
            "when an MAE above the limit is validated",
            "the MAE error is true")]
        public void T10()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(100), 200);

            // Act 
            validator.ValidateMae(Option.Some(101.00));

            // Assert
            Assert.True(validator.MaeHasError);
        }

        [Gwt("Given a trade details validator has an MAE limit and an MAE error",
            "when an MAE below the limit is validated",
            "the MAE error is false")]
        public void T11()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(100), 200);
            validator.MaeHasError = true;

            // Act 
            validator.ValidateMae(Option.Some(99.00));

            // Assert
            Assert.False(validator.MaeHasError);
        }

        [Gwt("Given a trade details validator has an MFE limit",
            "when an MFE below the limit is validated",
            "the MFE error is true")]
        public void T12()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(200), 100);

            // Act 
            validator.ValidateMfe(Option.Some(99.99));

            // Assert
            Assert.True(validator.MfeHasError);
        }

        [Gwt("Given a trade details validator has an MFE limit and an MFE error",
            "when an MFE above the limit is validated",
            "the MFE error is false")]
        public void T13()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(200), 100);
            validator.MfeHasError = true;

            // Act 
            validator.ValidateMfe(Option.Some(101.1));

            // Assert
            Assert.False(validator.MaeHasError);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a winning long trade",
            "the maximum MAE is the same as the open level")]
        public void T14()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(300), testOpen);

            // Assert
            Assert.Equal(testOpen, validator.MaximumMae);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a losing long trade",
            "the maximum MAE is equal to the difference between the open and close")]
        public void T15()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;
            const double testClose = 50;

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(testClose), testOpen);

            // Assert
            Assert.Equal(testOpen - testClose, validator.MaximumMae);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a long trade with no close",
            "the maximum MAE is equal to the open level")]
        public void T16()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, Option.None<double>(), testOpen);

            // Assert
            Assert.Equal(testOpen, validator.MaximumMae);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a winning long trade",
            "the minimum MFE is equal to the difference between the open and close")]
        public void T17()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;
            const double testClose = 350;

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(testClose), testOpen);

            // Assert
            Assert.Equal(testClose - testOpen, validator.MinimumMfe);
        }

        [Gwt("Given a trade details validator has a minimum MFE set above zero",
            "when excursion limits are updated for a losing long trade",
            "the minimum MFE is zero")]
        public void T18()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(200), 100);

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(50), 100);

            // Assert
            Assert.Equal(0, validator.MinimumMfe);
        }

        [Gwt("Given a trade details validator has a minimum MFE set above zero",
            "when excursion limits are updated for a long trade with no close",
            "the minimum MFE is zero")]
        public void T19()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(200), 100);

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, Option.None<double>(), 100);

            // Assert
            Assert.Equal(0, validator.MinimumMfe);
        }

        [Gwt("Given a trade details validator has an MAE below positive infinity",
            "when excursion limits are updated for a winning short trade",
            "the maximum MAE is positive infinity")]
        public void T20()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Short, new Option.OptionSome<double>(200), 100);

            // Act 
            validator.UpdateExcursionLimits(Direction.Short, new Option.OptionSome<double>(50), 100);

            // Assert
            Assert.Equal(double.PositiveInfinity, validator.MaximumMae);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a losing short trade",
            "the maximum MAE is equal to the difference between the open and close")]
        public void T21()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;
            const double testClose = 350;

            // Act 
            validator.UpdateExcursionLimits(Direction.Short, new Option.OptionSome<double>(testClose), testOpen);

            // Assert
            Assert.Equal(testClose - testOpen, validator.MaximumMae);
        }

        [Gwt("Given a trade details validator has a maximum MAE below positive infinity",
            "when excursion limits are updated for a short trade with no close",
            "the maximum MAE is positive infinity")]
        public void T22()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Short, new Option.OptionSome<double>(200), 100);

            // Act 
            validator.UpdateExcursionLimits(Direction.Short, Option.None<double>(), 100);

            // Assert
            Assert.Equal(double.PositiveInfinity, validator.MaximumMae);
        }

        [Gwt("Given a trade details validator",
            "when excursion limits are updated for a winning short trade",
            "the minimum MFE is equal to the difference between the open and close")]
        public void T23()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            const double testOpen = 200;
            const double testClose = 50;

            // Act 
            validator.UpdateExcursionLimits(Direction.Short, new Option.OptionSome<double>(testClose), testOpen);

            // Assert
            Assert.Equal(testOpen - testClose, validator.MinimumMfe);
        }

        [Gwt("Given a trade details validator has an MFE set above zero",
            "when excursion limits are updated for a losing short trade",
            "the minimum MFE is zero")]
        public void T24()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Short, new Option.OptionSome<double>(50), 100);

            // Act 
            validator.UpdateExcursionLimits(Direction.Short, new Option.OptionSome<double>(200), 100);

            // Assert
            Assert.Equal(0, validator.MinimumMfe);
        }

        [Gwt("Given a trade details validator has an minimum MFE above zero",
            "when excursion limits are updated for a short trade with no close",
            "the minimum MFE is zero")]
        public void T25()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.UpdateExcursionLimits(Direction.Short, new Option.OptionSome<double>(50), 100);

            // Act 
            validator.UpdateExcursionLimits(Direction.Short, Option.None<double>(), 100);

            // Assert
            Assert.Equal(0, validator.MinimumMfe);
        }

        [Gwt("Given a trade details validator has validated an MAE",
            "when the excursion limits are updated to invalidate the MAE",
            "the MAE error property is true")]
        public void T26()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.ValidateMae(Option.Some(100.00));

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(50), 100);

            // Assert
            Assert.True(validator.MaeHasError);
        }

        [Gwt("Given a trade details validator has validated an MFE",
            "when the excursion limits are updated to invalidate the MFE",
            "the MFE error property is true")]
        public void T27()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.ValidateMfe(Option.Some(100.00));

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(250), 100);

            // Assert
            Assert.True(validator.MfeHasError);
        }

        // Tests the validator's mae field default is option none
        [Gwt("Given a trade details validator has an MAE error",
            "when the excursion limits are updated to a maximum MAE below positive infinity",
            "the MAE error property is false")]
        public void T28()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.MaeHasError = true;

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(50), 100);

            // Assert
            Assert.False(validator.MaeHasError);
        }

        // Tests the validator's mfe field default is option none
        [Gwt("Given a trade details validator has an MFE error",
            "when the excursion limits are updated to a minimum MFE above zero",
            "the MFE error property is false")]
        public void T29()
        {
            // Arrange
            var validator = new TradeDetailsValidator();
            validator.MfeHasError = true;

            // Act 
            validator.UpdateExcursionLimits(Direction.Long, new Option.OptionSome<double>(150), 100);

            // Assert
            Assert.False(validator.MfeHasError);
        }
    }
}