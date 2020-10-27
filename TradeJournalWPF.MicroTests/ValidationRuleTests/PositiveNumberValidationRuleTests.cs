using Common.MicroTests;
using System.Windows.Controls;
using TradeJournalWPF.ValidationRules;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ValidationRuleTests
{
    public sealed class PositiveNumberValidationRuleTests
    {
        [Gwt("Given a null value",
            "when validated",
            "the result is false with the cannot be empty error message")]
        public void T0()
        {
            // Arrange
            var testValidationRule = new PositiveNumberValidationRule();

            // Act 
            var actual = testValidationRule.Validate(null, InvariantCulture);

            // Assert
            Assert.Equal(new ValidationResult(false, "Value cannot be empty"), actual);
        }

        [Gwt("Given an empty string",
            "when validated",
            "the result is false with the cannot be empty error message")]
        public void T1()
        {
            // Arrange
            var testValidationRule = new PositiveNumberValidationRule();

            // Act 
            var actual = testValidationRule.Validate(string.Empty, InvariantCulture);

            // Assert
            Assert.Equal(new ValidationResult(false, "Value cannot be empty"), actual);
        }

        [Gwt("Given a negative numeric string",
            "when validated",
            "the result is false with the value must be positive error message")]
        public void T2()
        {
            // Arrange
            var testValidationRule = new PositiveNumberValidationRule();

            // Act 
            var actual = testValidationRule.Validate("-5", InvariantCulture);

            // Assert
            Assert.Equal(new ValidationResult(false, "Value must be positive"), actual);
        }

        [Gwt("Given a positive numeric string",
            "when validated",
            "the result is a valid result")]
        public void T3()
        {
            // Arrange
            var testValidationRule = new PositiveNumberValidationRule();

            // Act 
            var actual = testValidationRule.Validate("123.6", InvariantCulture);

            // Assert
            Assert.Equal(ValidationResult.ValidResult, actual);
        }

        [Gwt("Given a non-numeric string",
            "when validated",
            "the result is false with the value must be a number error message")]
        public void T4()
        {
            // Arrange
            var testValidationRule = new PositiveNumberValidationRule();

            // Act 
            var actual = testValidationRule.Validate("test string", InvariantCulture);

            // Assert
            Assert.Equal(new ValidationResult(false, "Value must be a number"), actual);
        }
    }
}
