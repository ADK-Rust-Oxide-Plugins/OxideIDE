using System.Windows.Controls;
using System.Windows.Input;
using OxideIde.ViewModels;

namespace OxideIde.Windows
{
	/// <summary>
	/// Interaction logic for PluginView.xaml
	/// </summary>
	public partial class PluginView : UserControl
	{
		public PluginView()
		{
			InitializeComponent();
		}

		void OnCallHook(object sender, ExecutedRoutedEventArgs e)
		{
			var originalSource = (Button) e.OriginalSource;
			var callback = (CallbackViewModel) originalSource.DataContext;
			if(callback.Parameters.Length > 0)
			{
				var parameterWindow = new CallbackParameterWindow
				{
					DataContext = callback
				};
				if(parameterWindow.ShowDialog() == true)
					callback.Call();
			}
			else
			{
				callback.Call();
			}
		}
	}
}
