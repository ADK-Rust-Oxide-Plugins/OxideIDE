using System.Globalization;
using OxideEmulation.Templates;

namespace OxideEmulation.Parameters
{
	/// <summary>
	/// Defines a parameter with a boolean value
	/// </summary>
	public class BooleanParameter : AParameter
	{
		public bool Value { get; set; }

		public BooleanParameter(AParameterTemplate template)
			: base(template)
		{

		}

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