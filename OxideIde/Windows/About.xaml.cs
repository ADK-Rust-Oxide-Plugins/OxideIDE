using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace OxideIde.Windows
{
	/// <summary>
	/// Interaction logic for About.xaml
	/// </summary>
	public partial class About : Window
	{
		public About()
		{
			InitializeComponent();
		}

		void OnLinkClicked(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(e.Uri.AbsoluteUri);
			e.Handled = true;
		}
	}
}
