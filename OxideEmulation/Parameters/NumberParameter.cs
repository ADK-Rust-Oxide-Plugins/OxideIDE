using System.Globalization;
using OxideEmulation.Templates;

namespace OxideEmulation.Parameters
{
	/// <summary>
	/// Defines a parameter with a numeric value
	/// </summary>
	public class NumberParameter : AParameter
	{
		public NumberParameter(AParameterTemplate template)
			: base(template)
		{

		}

		public float Value { get; set; }

		public override object GetParameterValue()
		{
			return Value;
		}

		public override string ToString()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}
	}
}