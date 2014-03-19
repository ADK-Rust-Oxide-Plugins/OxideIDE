using System.Windows;
using OxideEmulation.Parameters;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Encapsulates a BooleanParameter for display in the gui.
	/// </summary>
	public class BooleanParameterViewModel : AParameterViewModel
	{
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
			"Value", typeof(bool), typeof(BooleanParameterViewModel), new PropertyMetadata(OnValueChanged));

		static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var instance = (BooleanParameterViewModel)d;
			instance.mParameter.Value = (bool)e.NewValue;
		}

		public bool Value
		{
			get { return (bool) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		readonly BooleanParameter mParameter;

		public BooleanParameterViewModel(BooleanParameter parameter)
			: base(parameter)
		{
			mParameter = parameter;
		}
	}
}