using System.Collections.Generic;
using System.Xml.Serialization;
using OxideEmulation.Templates;

namespace OxideEmulation
{
	/// <summary>
	/// Describes an oxide hook function
	/// </summary>
	public class OxideHook
	{
		/// <summary>
		/// The name of the function
		/// </summary>
		[XmlAttribute("function")]
		public string FunctionName { get; set; }

		/// <summary>
		/// A description to show the user
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The parameters that need to be passed to that function
		/// </summary>
		[XmlArray("Parameters")]
		public List<AParameterTemplate> Parameters { get; set; }

		public OxideHook()
		{
			//Parameters = new List<AParameterTemplate>();
		}
	}
}