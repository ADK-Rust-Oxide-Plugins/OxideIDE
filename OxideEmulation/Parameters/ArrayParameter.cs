using System.Collections.Generic;
using System.Linq;
using NLua;
using OxideEmulation.Templates;

namespace OxideEmulation.Parameters
{
	/// <summary>
	/// Defines an array parameter that can have one type of content
	/// </summary>
	public class ArrayParameter : AParameter
	{
		readonly List<AParameter> mValues = new List<AParameter>();
		readonly ArrayParameterTemplate mTemplate;

		/// <summary>
		/// The type of content for this array
		/// </summary>
		public AParameterTemplate ItemTemplate
		{
			get { return mTemplate.ItemTemplate; }
		}

		public ArrayParameter(ArrayParameterTemplate template)
			: base(template)
		{
			mTemplate = template;
		}

		public Lua Lua { get; set; }

		/// <summary>
		/// The values currently present in the array
		/// </summary>
		public ICollection<AParameter> Values
		{
			get { return mValues; }
		}

		public override object GetParameterValue()
		{
			var array = (LuaTable)Lua.DoString("return {}")[0];
			for(var i = 0; i < mValues.Count; i++)
			{
				array[i + 1] = mValues[i].GetParameterValue();
			}
			return array;
		}

		public override string ToString()
		{
			return string.Format("[{0}]", string.Join(",", mValues.Select(v => v.ToString())));
		}
	}
}