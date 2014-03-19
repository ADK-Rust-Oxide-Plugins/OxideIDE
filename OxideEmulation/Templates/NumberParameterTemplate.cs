using System.Xml.Serialization;
using OxideEmulation.Parameters;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Defines the template for a numeric parameter
	/// </summary>
	public class NumberParameterTemplate : AParameterTemplate
	{
		[XmlAttribute("default")]
		public float DefaultValue { get; set; }

		public override AParameter CreateInstance(ParameterFactory factory)
		{
			return factory.NumberParameter(this, DefaultValue);
		}
	}
}