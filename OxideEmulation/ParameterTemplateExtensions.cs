using System.Collections.Generic;
using System.Linq;
using OxideEmulation.Parameters;
using OxideEmulation.Templates;

namespace OxideEmulation
{
	public static class ParameterTemplateExtensions
	{
		/// <summary>
		/// Convenience function to generate all parameters from a list of templates.
		/// </summary>
		/// <param name="self">the templates to create parameters from</param>
		/// <param name="factory">Instance of a parameter factory</param>
		/// <returns>A list of parameter instances corresponding to the templates</returns>
		public static AParameter[] CreateInstances(this IEnumerable<AParameterTemplate> self, ParameterFactory factory)
		{
			return self.Select(p => p.CreateInstance(factory)).ToArray();
		}
	}
}