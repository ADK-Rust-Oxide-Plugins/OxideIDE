using System.Collections.Generic;
using System.Linq;

namespace OxideEmulation
{
	/// <summary>
	/// Container for OxideHook objects
	/// </summary>
	public class OxideHooks : XmlLoaded<OxideHooks>
	{
		Dictionary<string, OxideHook> mLookup;
		public List<OxideHook> Hooks { get; set; }

		public OxideHooks()
		{
			Hooks = new List<OxideHook>();
		}

		public OxideHook this[string hookName]
		{
			get
			{
				EnsureLookup();
				return mLookup[hookName];
			}
		}

		void EnsureLookup()
		{
			if(mLookup == null)
				mLookup = Hooks.ToDictionary(h => h.FunctionName);
		}
	}
}