using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using OxideIde.ViewModels;

namespace OxideIde.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			var highlighting = HighlightingLoader.Load(XmlReader.Create(Path.Combine(Environment.CurrentDirectory, "lua.xshd")), HighlightingManager.Instance);
			HighlightingManager.Instance.RegisterHighlighting(highlighting.Name, new[] { ".lua" }, highlighting);

			InitializeComponent();
			Context.Initialize();
		}

		IdeViewModel Context
		{
			get { return (IdeViewModel) DataContext; }
		}

		void OnExit(object sender, ExecutedRoutedEventArgs e)
		{
			Close();
		}

		void OnOpen(object sender, ExecutedRoutedEventArgs e)
		{
			var ofd = new OpenFileDialog
			{
				CheckFileExists = true,
				Filter = "oxide plugins (*.lua)|*.lua|All Files (*.*)|*.*",
			};
			if(ofd.ShowDialog(this) == true)
			{
				LoadPlugin(ofd.FileName);
			}
			e.Handled = true;
		}

		void LoadPlugin(string fileName)
		{
			Context.Settings.AutocompletionActive = false;
			Context.Load(fileName);
			Context.Settings.AutocompletionActive = true;
		}

		void OnPluginLoad(object sender, ExecutedRoutedEventArgs e)
		{
			Context.ClearOutput();
			Context.CurrentPlugin.Load(Context.Oxide);
			e.Handled = true;
		}

		void HasPluginLoaded(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = Context.CurrentPlugin != null;
			e.Handled = true;
		}

		void OnNew(object sender, ExecutedRoutedEventArgs e)
		{
			if(!CheckAndWarnOfUnsavedChanges())
				return;

			var sfd = new SaveFileDialog
			{
				AddExtension = true,
				CheckPathExists = true,
				OverwritePrompt = true,
				RestoreDirectory = true,
				Filter = "oxide plugins (*.lua)|*.lua|All Files (*.*)|*.*",
			};
			if(sfd.ShowDialog() == true)
			{
				var templateName = (string)e.Parameter;
				var templatePath = Path.Combine(Constants.TemplatesFilesDirectory, templateName);
                File.Copy(templatePath, sfd.FileName, true);
				LoadPlugin(sfd.FileName);
			}
			e.Handled = true;
		}

		bool CheckAndWarnOfUnsavedChanges()
		{
			if(Context.CurrentPlugin != null && Context.CurrentPlugin.HasUnsavedChanges)
			{
				var result = MessageBox.Show(this,
					string.Format("{0} has unsaved changes. Continue without saving?.",
						Context.CurrentPlugin.FileName), "Unsaved changes", MessageBoxButton.YesNo, MessageBoxImage.Warning);
				return result == MessageBoxResult.Yes;
			}
			return true;
		}

		void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !CheckAndWarnOfUnsavedChanges();
		}

		void OnMainMenuAboutExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var aboutWindow = new About();
			aboutWindow.ShowDialog();
		}
	}
}
