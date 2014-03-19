using System.Xml.Serialization;
using OxideEmulation.Parameters;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Defines a reference to another template. Can be used to avoid duplication of common parameters like complex objects
	/// </summary>
	public class ReferenceParameterTemplate : AParameterTemplate
	{
		AParameterTemplate mReference;

		/// <summary>
		/// The name of the template this parameter references
		/// </summary>
		[XmlAttribute("templateName")]
		public string TemplateName { get; set; }

		/// <summary>
		/// The actual reference of the parameter after it has been resolved.
		/// </summary>
		[XmlIgnore]
		public AParameterTemplate Reference
		{
			get { return mReference; }
			set
			{
				mReference = value;
				if(string.IsNullOrWhiteSpace(Name))
					Name = mReference.Name;

				if(string.IsNullOrWhiteSpace(Description))
					Description = mReference.Description;
			}
		}

		public override AParameter CreateInstance(ParameterFactory factory)
		{
			var instance = Reference.CreateInstance(factory);
			instance.Template = this;
			return instance;
		}
	}
}