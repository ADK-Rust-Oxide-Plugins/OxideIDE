using System.Collections.Generic;
using System.Linq;
using NLua;
using OxideEmulation.Templates;

namespace OxideEmulation.Parameters
{
	/// <summary>
	/// Defines a parameter of the object type (table in lua)
	/// </summary>
	public class ObjectParameter : AParameter
	{
		readonly List<AParameter> mFields = new List<AParameter>();

		public ObjectParameter(ObjectParameterTemplate template)
			: base(template)
		{

		}

		public Lua Lua { get; set; }

		/// <summary>
		/// The fields of this object
		/// </summary>
		public List<AParameter> Fields
		{
			get { return mFields; }
		}

		public override object GetParameterValue()
		{
			var result = Lua.DoString("return {}");
			var obj = (LuaTable) result[0];
			foreach(var field in Fields)
			{
				obj[field.Template.Name] = field.GetParameterValue();
			}
			return obj;
		}

		public override string ToString()
		{
			return string.Format("{{{0}}}", string.Join(",", Fields.Select(f => string.Format("{0}:{1}", f.Template.Name, f))));
		}
	}
}