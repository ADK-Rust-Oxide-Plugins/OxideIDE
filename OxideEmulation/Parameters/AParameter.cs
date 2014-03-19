using OxideEmulation.Templates;

namespace OxideEmulation.Parameters
{
	/// <summary>
	/// Base class for a parameter of any function
	/// </summary>
	public abstract class AParameter
	{
		protected AParameter(AParameterTemplate template)
		{
			Template = template;
		}

		/// <summary>
		/// The template from which this parameter was created
		/// </summary>
		public AParameterTemplate Template { get; set; }


		/// <summary>
		/// Called when someone wants to get the actual value of a parameter
		/// </summary>
		public abstract object GetParameterValue();
	}
}