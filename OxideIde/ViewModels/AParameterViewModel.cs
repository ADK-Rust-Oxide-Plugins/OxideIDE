using System;
using System.Windows;
using OxideEmulation.Parameters;
using OxideEmulation.Templates;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Base view model encapsulating a parameter for display in the gui..
	/// </summary>
	public abstract class AParameterViewModel : DependencyObject
	{
		public AParameterTemplate Template
		{
			get { return mParameter.Template; }
		}

		readonly AParameter mParameter;
		public static ParameterFactory ParameterFactory { get; set; }

		protected AParameterViewModel(AParameter parameter)
		{
			mParameter = parameter;
		}

		public AParameter Model
		{
			get { return mParameter; }
		}

		/// <summary>
		/// Main function to create a view model from a given parameter.
		/// </summary>
		/// <param name="parameter">The parameter to create the viewmodel for</param>
		/// <returns>The created view model.</returns>
		public static AParameterViewModel CreateParameterViewModel(AParameter parameter)
		{
			if(parameter is StringParameter)
				return new StringParameterViewModel(parameter as StringParameter);

			if(parameter is ArrayParameter)
				return new ArrayParameterViewModel(parameter as ArrayParameter, ParameterFactory);

			if(parameter is ObjectParameter)
				return new ObjectParameterViewModel(parameter as ObjectParameter);

			if(parameter is NumberParameter)
				return new NumberParameterViewModel(parameter as NumberParameter);

			if (parameter is BooleanParameter)
				return new BooleanParameterViewModel(parameter as BooleanParameter);

			throw new NotImplementedException();
		}
	}
}