using NLua;
using OxideEmulation.Templates;

namespace OxideEmulation.Parameters
{
	/// <summary>
	/// Creates the parameter with of the given template
	/// </summary>
	public class ParameterFactory
	{
		readonly Lua mLua;

		public ParameterFactory(Lua lua)
		{
			mLua = lua;
		}

		public StringParameter StringParameter(StringParameterTemplate template, string defaultValue)
		{
			return new StringParameter(template)
			{
				Value = defaultValue,
			};
		}

		public ArrayParameter ArrayParameter(ArrayParameterTemplate template)
		{
			return new ArrayParameter(template)
			{
				Lua = mLua,
			};
		}

		public ObjectParameter ObjectParameter(ObjectParameterTemplate template)
		{
			return new ObjectParameter(template)
			{
				Lua = mLua,
			};
		}

		public AParameter NumberParameter(NumberParameterTemplate template, float defaultValue)
		{
			return new NumberParameter(template)
			{
				Value = defaultValue,
			};
		}

		public AParameter BooleanParameter(BooleanParameterTemplate template, bool defaultValue)
		{
			return new BooleanParameter(template)
			{
				Value = defaultValue,
			};
		}
	}
}