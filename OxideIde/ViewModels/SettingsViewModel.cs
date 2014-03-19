using System.Windows;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Holds all settings of the IDE
	/// </summary>
	public class SettingsViewModel : DependencyObject
	{
		public static readonly DependencyProperty AutocompletionActiveProperty = DependencyProperty.Register(
			"AutocompletionActive", typeof(bool), typeof(SettingsViewModel), new PropertyMetadata(default(bool)));

		public bool AutocompletionActive
		{
			get { return (bool) GetValue(AutocompletionActiveProperty); }
			set { SetValue(AutocompletionActiveProperty, value); }
		}
	}
}