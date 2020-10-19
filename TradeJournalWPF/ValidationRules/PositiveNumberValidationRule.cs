using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace TradeJournalWPF.ValidationRules
{
    public sealed class PositiveNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Value cannot be empty");
            }

            if (double.TryParse((string)value, out var number))
            {
                if (number < 0)
                {
                    return new ValidationResult(false, "Value must be positive");
                }

                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, "Value must be a number");
        }
    }
}
