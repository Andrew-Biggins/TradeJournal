using System.Globalization;
using System.Windows.Controls;

namespace TradeJournalWPF.ValidationRules
{
    public class PositiveNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Value cannot be empty");
            }

            return ValidateText(value);
        }

        public ValidationResult ValidateText(object value)
        {
            if (double.TryParse((string)value, out var number))
            {
                return number < 0
                    ? new ValidationResult(false, "Value must be positive")
                    : ValidationResult.ValidResult;
            }

            return new ValidationResult(false, "Value must be a number");
        }
    }
}
