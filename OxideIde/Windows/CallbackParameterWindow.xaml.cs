using System.Windows;
using System.Windows.Controls;
using OxideIde.Helpers;
using OxideIde.ViewModels;

namespace OxideIde.Windows
{
	/// <summary>
	/// Interaction logic for CallbackParameterWindow.xaml
	/// </summary>
	public partial class CallbackParameterWindow : Window
	{
		public CallbackParameterWindow()
		{
			InitializeComponent();
		}

		void OnRunClicked(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			e.Handled = true;
		}

		void OnAbortClicked(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			e.Handled = true;
		}

		void OnAddToArrayClicked(object sender, RoutedEventArgs e)
		{
			var source = (ArrayParameterViewModel) ((FrameworkElement) e.OriginalSource).DataContext;
			source.AddItem();
			e.Handled = true;
		}

		void OnRemoveFromArrayClicked(object sender, RoutedEventArgs e)
		{
			var element = ((FrameworkElement)e.OriginalSource);
			var parent = ((FrameworkElement) element.Parent);
			var parentParent = ((FrameworkElement) parent.Parent);
			var listView = parentParent.GetChildOfType<ListView>();

			var array = (ArrayParameterViewModel)listView.DataContext;
			var item = (AParameterViewModel)listView.SelectedValue;
			if(item != null)
			{
				array.Remove(item);
			}
		}
	}
}
