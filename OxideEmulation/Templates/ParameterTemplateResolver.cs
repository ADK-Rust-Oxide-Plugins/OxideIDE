using System;
using System.Collections.Generic;
using System.Linq;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Resolves all ReferenceTemplateParameters
	/// </summary>
	public static class ParameterTemplateResolver
	{
		/// <summary>
		/// Tries to resolve all ReferenceTemplateParameters
		/// </summary>
		/// <param name="hooks">Hook definitions which should be search for reference parameters.</param>
		/// <param name="storage">Template storage which should be searched for reference parameters</param>
		/// <param name="onError">This callback will be called for once for each non resolved reference parameter</param>
		/// <returns>Returns if all references have been resolved</returns>
		public static bool ResolveAll(OxideHooks hooks, ParameterTemplateStorage storage, Action<string> onError)
		{
			var referenceTemplates = hooks.Hooks
				.SelectMany(h => h.Parameters)
				.SelectMany(GetRecursive)
				.Union(storage.Templates.SelectMany(GetRecursive));

			var result = true;
			foreach(var reference in referenceTemplates)
			{
				if(!storage.Resolve(reference))
				{
					onError(string.Format("Couldn't resolve '{0}'", reference.TemplateName));
					result = false;
				}
			}
			return result;
		}

		/// <summary>
		/// Recursively search through a template and any child templates (Array.ItemTemplate or Object.Fields)
		/// </summary>
		/// <param name="self">The parameter to search in</param>
		/// <returns>A list of all parameters</returns>
		static IEnumerable<ReferenceParameterTemplate> GetRecursive(this AParameterTemplate self)
		{
			if(self is ReferenceParameterTemplate)
			{
				yield return self as ReferenceParameterTemplate;
				yield break;
			}

			var obj = self as ObjectParameterTemplate;
			if(obj != null)
			{
				foreach(var field in obj.Fields)
				{
					foreach(var entry in GetRecursive(field))
						yield return entry;
				}
			}

			var array = self as ArrayParameterTemplate;
			if(array != null)
			{
				foreach (var entry in GetRecursive(array.ItemTemplate))
					yield return entry;
			}
		}
	}
}