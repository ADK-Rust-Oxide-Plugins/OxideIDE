using System.Collections.Generic;
using OxideEmulation.Parameters;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Defines the template for an object parameter and its fields
	/// </summary>
	public class ObjectParameterTemplate : AParameterTemplate
	{
		/// <summary>
		/// What fields should exist in this object
		/// </summary>
		public List<AParameterTemplate> Fields { get; set; }

		public ObjectParameterTemplate()
		{
			Fields = new List<AParameterTemplate>();
		}

		public override AParameter CreateInstance(ParameterFactory factory)
		{
			var objectParameter = factory.ObjectParameter(this);
			foreach(var field in Fields)
				objectParameter.Fields.Add(field.CreateInstance(factory));
			return objectParameter;
		}
	}
}