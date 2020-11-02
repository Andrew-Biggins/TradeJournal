using System.Globalization;
using System.Windows.Controls;

namespace TradeJournalWPF.ValidationRules
{
    public class CanBeEmptyPositiveNumberValidationRule : PositiveNumberValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.ValidResult;
            }

            return ValidateText(value);
        }
    }
}