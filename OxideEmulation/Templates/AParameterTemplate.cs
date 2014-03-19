using System.Xml.Serialization;
using OxideEmulation.Parameters;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Base type for a parameter template which can be loaded from xml
	/// </summary>
	[XmlInclude(typeof(StringParameterTemplate))]
	[XmlInclude(typeof(ObjectParameterTemplate))]
	[XmlInclude(typeof(ArrayParameterTemplate))]
	[XmlInclude(typeof(ReferenceParameterTemplate))]
	[XmlInclude(typeof(BooleanParameterTemplate))]
	[XmlInclude(typeof(NumberParameterTemplate))]
	public abstract class AParameterTemplate
	{
		protected AParameterTemplate()
		{
			Name = string.Empty;
		}

		/// <summary>
		/// The name of the parameter
		/// </summary>
		[XmlAttribute("name")]
		public string Name { get; set; }

		/// <summary>
		/// A description to be displayed to the user
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Called when someone wants an actual parameter instance from this template
		/// </summary>
		/// <param name="factory"></param>
		/// <returns>An instance of this template</returns>
		public abstract AParameter CreateInstance(ParameterFactory factory);

		public override string ToString()
		{
			return string.Format("{0}('{1}')", GetType().Name, Name);
		}
	}
}