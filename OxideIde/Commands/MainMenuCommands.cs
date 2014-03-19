using System.Windows.Input;

namespace OxideIde.Commands
{
	/// <summary>
	/// Non standard commands executed from main menu
	/// </summary>
	public static class MainMenuCommands
	{
		public static readonly RoutedCommand Exit = new RoutedCommand();
		public static readonly RoutedCommand NewFileFromTemplate = new RoutedCommand();
		public static readonly RoutedCommand About = new RoutedCommand();
	}
}
