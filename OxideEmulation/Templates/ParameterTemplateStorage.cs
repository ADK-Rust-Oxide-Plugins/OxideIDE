using System.Collections.Generic;
using System.Linq;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Holds a list of parameter templates which can be referenced from plugin hooks.
	/// </summary>
	public class ParameterTemplateStorage : XmlLoaded<ParameterTemplateStorage>
	{
		/// <summary>
		/// A list of templates of any kind
		/// </summary>
		public List<AParameterTemplate> Templates { get; set; }

		IDictionary<string, AParameterTemplate> mLookup;

		/// <summary>
		/// Lookup a template by name
		/// </summary>
		/// <param name="name">The name of the template to get</param>
		/// <returns></returns>
		public AParameterTemplate this[string name]
		{
			get
			{
				EnsureLookup();
				return mLookup[name];
			}
		}

		/// <summary>
		/// Resolves a reference parameter
		/// </summary>
		/// <param name="template">The reference parameter </param>
		/// <returns>Returns wether the parameter has been resolved successfully or not.</returns>
		public bool Resolve(ReferenceParameterTemplate template)
		{
			EnsureLookup();
			AParameterTemplate reference;
			if(mLookup.TryGetValue(template.TemplateName, out reference))
			{
				template.Reference = reference;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Creates the lookup dictionary if necessary
		/// </summary>
		void EnsureLookup()
		{
			if(mLookup == null)
			{
				mLookup = Templates.ToDictionary(t => t.Name);
			}
		}
	}
}