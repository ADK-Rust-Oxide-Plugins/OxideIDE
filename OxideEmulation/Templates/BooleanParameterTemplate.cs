using System.Xml.Serialization;
using OxideEmulation.Parameters;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Defines the template for a boolean parameter
	/// </summary>
	public class BooleanParameterTemplate : AParameterTemplate
	{
		[XmlAttribute("default")]
		public bool DefaultValue { get; set; }

		public override AParameter CreateInstance(ParameterFactory factory)
		{
			return factory.BooleanParameter(this, DefaultValue);
		}
	}
}