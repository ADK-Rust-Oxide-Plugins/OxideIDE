using System.Windows;
using OxideEmulation.Parameters;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Encapsulates a StringParameter for display in the gui.
	/// </summary>
	public class StringParameterViewModel : AParameterViewModel
	{
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(StringParameterViewModel), new PropertyMetadata(OnValueChanged));
		readonly StringParameter mParameter;

		static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var instance = (StringParameterViewModel) d;
			instance.mParameter.Value = (string)e.NewValue;
		}

		public string Value
		{
			get { return (string) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public StringParameterViewModel(StringParameter parameter)
			: base(parameter)
		{
			mParameter = parameter;
			Value = parameter.Value;
		}
	}
}