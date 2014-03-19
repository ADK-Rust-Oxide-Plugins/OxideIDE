using OxideEmulation.Parameters;

namespace OxideEmulation.Templates
{
	/// <summary>
	/// Defines an array parameter and its content type
	/// </summary>
	public class ArrayParameterTemplate : AParameterTemplate
	{
		/// <summary>
		/// Defines what kind of objects should be present in the array.
		/// </summary>
		public AParameterTemplate ItemTemplate { get; set; }

		public override AParameter CreateInstance(ParameterFactory factory)
		{
			return factory.ArrayParameter(this);
		}
	}
}