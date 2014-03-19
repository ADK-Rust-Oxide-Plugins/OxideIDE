using System.Windows;
using OxideEmulation.Parameters;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Encapsulates a NumberParameter for display in the gui
	/// </summary>
	public class NumberParameterViewModel : AParameterViewModel
	{
		readonly NumberParameter mParameter;

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
			"Value", typeof(float), typeof(NumberParameterViewModel), new PropertyMetadata(OnValueChanged));

		static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var instance = (NumberParameterViewModel)d;
			instance.mParameter.Value = (float)e.NewValue;
		}

		public float Value
		{
			get { return (float) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public NumberParameterViewModel(NumberParameter parameter)
			: base(parameter)
		{
			mParameter = parameter;
		}
	}
}