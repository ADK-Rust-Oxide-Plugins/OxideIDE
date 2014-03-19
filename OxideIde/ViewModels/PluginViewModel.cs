using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using ICSharpCode.AvalonEdit.Document;
using OxideEmulation;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Describes a plugin from the perspective of the gui.
	/// </summary>
	public class PluginViewModel : DependencyObject
	{
		public static readonly DependencyProperty PluginTextProperty = DependencyProperty.Register("PluginText", typeof(TextDocument), typeof(PluginViewModel), new PropertyMetadata(default(TextDocument)));
		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(PluginViewModel), new PropertyMetadata(default(string)));
		public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(PluginViewModel), new PropertyMetadata(default(string)));
		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(PluginViewModel), new PropertyMetadata(default(string)));
		public static readonly DependencyProperty HooksProperty = DependencyProperty.Register("Hooks", typeof(ObservableCollection<CallbackViewModel>), typeof(PluginViewModel), new PropertyMetadata(default(ObservableCollection<CallbackViewModel>)));
		public static readonly DependencyProperty ChatCommandsProperty = DependencyProperty.Register("ChatCommands", typeof(ObservableCollection<CallbackViewModel>), typeof(PluginViewModel), new PropertyMetadata(default(ObservableCollection<CallbackViewModel>)));
		public static readonly DependencyProperty HasUnsavedChangesProperty = DependencyProperty.Register("HasUnsavedChanges", typeof(bool), typeof(PluginViewModel), new PropertyMetadata(default(bool)));
		public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(PluginViewModel), new PropertyMetadata(default(string)));

		readonly Action<LogEntryViewModel> mAddLogEntry;
		readonly string mPath;
		PluginData mPluginData;

		public PluginViewModel(string path, Action<LogEntryViewModel> addLogEntry)
			: this()
		{
			mPath = path;
			mAddLogEntry = addLogEntry;
			PluginText = new TextDocument(File.ReadAllText(path));
			Name = Path.GetFileNameWithoutExtension(path);
			Title = "<Not Executed>";
			Description = "<Not Executed>";
			FileName = Path.GetFileName(path);

			PluginText.Changed += OnDocumentChanged;
		}

		public PluginViewModel()
		{
			Hooks = new ObservableCollection<CallbackViewModel>();
			ChatCommands = new ObservableCollection<CallbackViewModel>();
		}

		/// <summary>
		/// Exposes the filename of the plugin
		/// </summary>
		public string FileName
		{
			get { return (string) GetValue(FileNameProperty); }
			set { SetValue(FileNameProperty, value); }
		}

		/// <summary>
		/// Does the plugin source code have unsaved changes
		/// </summary>
		public bool HasUnsavedChanges
		{
			get { return (bool) GetValue(HasUnsavedChangesProperty); }
			set { SetValue(HasUnsavedChangesProperty, value); }
		}

		/// <summary>
		/// All chat commands discovered up to now, might change based on what hook is executed.
		/// </summary>
		public ObservableCollection<CallbackViewModel> ChatCommands
		{
			get { return (ObservableCollection<CallbackViewModel>) GetValue(ChatCommandsProperty); }
			set { SetValue(ChatCommandsProperty, value); }
		}

		/// <summary>
		/// All hooks of the plugin discovered up to now.
		/// </summary>
		public ObservableCollection<CallbackViewModel> Hooks
		{
			get { return (ObservableCollection<CallbackViewModel>) GetValue(HooksProperty); }
			set { SetValue(HooksProperty, value); }
		}

		/// <summary>
		/// The name of the plugin take from file name
		/// </summary>
		public string Name
		{
			get { return (string) GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		/// <summary>
		/// The title of the plugin taken from PLUGIN.Title
		/// </summary>
		public string Title
		{
			get { return (string) GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		/// <summary>
		/// The description of the plugin taken from PLUGIN.Description
		/// </summary>
		public string Description
		{
			get { return (string) GetValue(DescriptionProperty); }
			set { SetValue(DescriptionProperty, value); }
		}

		/// <summary>
		/// The sourcecode of the plugin in a form consumable by avalonedit.
		/// </summary>
		public TextDocument PluginText
		{
			get { return (TextDocument) GetValue(PluginTextProperty); }
			set { SetValue(PluginTextProperty, value); }
		}

		/// <summary>
		/// Is called every time the text in the editor changes
		/// </summary>
		void OnDocumentChanged(object sender, DocumentChangeEventArgs e)
		{
			HasUnsavedChanges = true;
		}

		/// <summary>
		/// Loads this plugin in the given oxide emulation layer.
		/// </summary>
		/// <param name="oxide">The oxide layer</param>
		public void Load(OxideViewModel oxide)
		{
			Hooks.Clear();
			ChatCommands.Clear();
			var pluginData = oxide.LoadPlugin(Name, PluginText.Text);
			InitializeFromData(pluginData);
		}

		/// <summary>
		/// Processes the data returned from loading a plugin
		/// </summary>
		/// <param name="pluginData"></param>
		void InitializeFromData(PluginData pluginData)
		{
			mPluginData = pluginData;
			Title = pluginData.Title;
			Description = pluginData.Description;

			foreach(var hook in pluginData.Hooks)
			{
				Hooks.Add(new CallbackViewModel(hook, mAddLogEntry));
			}

			mPluginData.ChatCommandAdded += OnChatCommandAdded;
		}

		void OnChatCommandAdded(PluginCallback chatCommand)
		{
			ChatCommands.Add(new CallbackViewModel(chatCommand, mAddLogEntry));
		}

		/// <summary>
		/// Save the plugin text back to the file it came from.
		/// </summary>
		public void Save()
		{
			File.WriteAllText(mPath, PluginText.Text);
			HasUnsavedChanges = false;
		}
	}
}