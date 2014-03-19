using System;
using System.Diagnostics;
using System.Windows.Input;

namespace OxideIde.Commands
{
	/// <summary>
	/// Open the website given as string parameter to this command.
	/// </summary>
	public class OpenWebsiteCommand : ICommand 
	{
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			Process.Start((string) parameter);
		}

		public event EventHandler CanExecuteChanged;
	}
}