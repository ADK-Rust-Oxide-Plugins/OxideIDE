using System.Xml.Serialization;
using OxideEmulation.Parameters;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Defines the template for a string parameter
	/// </summary>
	public class StringParameterTemplate : AParameterTemplate
	{
		[XmlAttribute("default")]
		public string DefaultValue { get; set; }

		public StringParameterTemplate()
		{
			DefaultValue = string.Empty;
		}

		public override AParameter CreateInstance(ParameterFactory factory)
		{
			return factory.StringParameter(this, DefaultValue);
		}
	}
}