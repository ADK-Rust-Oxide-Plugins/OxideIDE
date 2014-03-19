using System.Windows.Input;

namespace OxideIde.Commands
{
	/// <summary>
	/// Non standard commands executed from plugin view
	/// </summary>
	public static class PluginCommands
	{
		public static readonly RoutedCommand Execute = new RoutedCommand();
		public static readonly RoutedCommand CallHook = new RoutedCommand();
	}
}