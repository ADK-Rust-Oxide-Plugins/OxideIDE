using OxideEmulation.Templates;

namespace OxideEmulation.Parameters
{
	/// <summary>
	/// Defines a parameter with a string value
	/// </summary>
	public class StringParameter : AParameter
	{
		public StringParameter(StringParameterTemplate template)
			: base(template)
		{

		}

		public string Value { get; set; }

		public override object GetParameterValue()
		{
			return Value;
		}

		public override string ToString()
		{
			return string.Format("'{0}'", Value);
		}
	}
}