using System.Globalization;
using System.Windows.Controls;

namespace OxideIde.Validators
{
	/// <summary>
	/// Validates if the input string is valid number.
	/// </summary>
	public class NumberValidator : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			float result;
			return new ValidationResult(float.TryParse((string)value, NumberStyles.Number, CultureInfo.InvariantCulture, out result), 0);
		}
	}
}