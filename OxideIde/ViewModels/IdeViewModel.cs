using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using OxideEmulation;
using OxideEmulation.Templates;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Basic viewmodel for the application, this is the root instantiated once in the MainWindow.
	/// </summary>
	public class IdeViewModel : DependencyObject
	{
		public static readonly DependencyProperty CurrentPluginProperty = DependencyProperty.Register("CurrentPlugin", typeof(PluginViewModel), typeof(IdeViewModel));
		public static readonly DependencyProperty OxideProperty = DependencyProperty.Register("Oxide", typeof(OxideViewModel), typeof(IdeViewModel), new PropertyMetadata(default(Oxide)));
		public static readonly DependencyProperty OutputLogProperty = DependencyProperty.Register("OutputLog", typeof(ObservableCollection<LogEntryViewModel>), typeof(IdeViewModel));
		public static readonly DependencyProperty NewFileTemplatesProperty = DependencyProperty.Register("NewFileTemplates", typeof(ObservableCollection<string>), typeof(IdeViewModel));
		public static readonly DependencyProperty SettingsProperty = DependencyProperty.Register("Settings", typeof(SettingsViewModel), typeof(IdeViewModel));
		public static readonly DependencyProperty AvailableOutputLogTypesProperty = DependencyProperty.Register("AvailableOutputLogTypes", typeof(ObservableCollection<string>), typeof(IdeViewModel));

		OxideHooks mHookDefinitions;
		ParameterTemplateStorage mTemplateStorage;

		public IdeViewModel()
		{
			OutputLog = new ObservableCollection<LogEntryViewModel>();
			NewFileTemplates = new ObservableCollection<string>();
			Settings = new SettingsViewModel();
			AvailableOutputLogTypes = new ObservableCollection<string>();
		}

		/// <summary>
		/// Defines which icons are present in the output log, is used to display a filter.
		/// </summary>
		public ObservableCollection<string> AvailableOutputLogTypes
		{
			get { return (ObservableCollection<string>)GetValue(AvailableOutputLogTypesProperty); }
			set { SetValue(AvailableOutputLogTypesProperty, value); }
		}

		/// <summary>
		/// Exposes the settings of the application
		/// </summary>
		public SettingsViewModel Settings
		{
			get { return (SettingsViewModel) GetValue(SettingsProperty); }
			set { SetValue(SettingsProperty, value); }
		}

		/// <summary>
		/// List of all "new file" templates found during editor startup.
		/// </summary>
		public ObservableCollection<string> NewFileTemplates
		{
			get { return (ObservableCollection<string>) GetValue(NewFileTemplatesProperty); }
			set { SetValue(NewFileTemplatesProperty, value); }
		}

		/// <summary>
		/// All logging output is redirected to this collection
		/// </summary>
		public ObservableCollection<LogEntryViewModel> OutputLog
		{
			get { return (ObservableCollection<LogEntryViewModel>) GetValue(OutputLogProperty); }
			set { SetValue(OutputLogProperty, value); }
		}

		/// <summary>
		/// Exposes the basic oxide interaction layer.
		/// </summary>
		public OxideViewModel Oxide
		{
			get { return (OxideViewModel)GetValue(OxideProperty); }
			private set { SetValue(OxideProperty, value); }
		}

		/// <summary>
		/// The currently loaded plugin
		/// </summary>
		public PluginViewModel CurrentPlugin
		{
			get { return (PluginViewModel) GetValue(CurrentPluginProperty); }
			private set { SetValue(CurrentPluginProperty, value); }
		}

		/// <summary>
		/// Gets or loads the hook definitions.
		/// </summary>
		public OxideHooks HookDefinitions
		{
			get { return mHookDefinitions ?? (mHookDefinitions = OxideHooks.Load("hooks.xml")); }
		}

		/// <summary>
		/// Gets or loads the templates.
		/// </summary>
		public ParameterTemplateStorage TemplateStorage
		{
			get { return mTemplateStorage ?? (mTemplateStorage = ParameterTemplateStorage.Load("templates.xml")); }
		}

		/// <summary>
		/// Called at the beginning of the application to do some post processing of the startup sequence.
		/// </summary>
		/// <returns></returns>
		public bool Initialize()
		{
			if(!ParameterTemplateResolver.ResolveAll(HookDefinitions, TemplateStorage, AddError))
				return false;

			foreach(var file in Directory.EnumerateFiles(Constants.TemplatesFilesDirectory, "*.lua", SearchOption.TopDirectoryOnly))
				NewFileTemplates.Add(Path.GetFileName(file));

			return true;
		}

		void AddError(string msg)
		{
			AddLogEntry(new LogEntryViewModel
			{
				Icon = "Icons\\Log\\error.png",
				Message = msg
			});
		}

		/// <summary>
		/// Load the plugin with the given path and initialize an oxide environment for it.
		/// </summary>
		/// <param name="path">The path to load the plugin from</param>
		public void Load(string path)
		{
			Oxide = new OxideViewModel(AddLogEntry, TemplateStorage, HookDefinitions);
			CurrentPlugin = new PluginViewModel(path, AddLogEntry);
		}

		void AddLogEntry(LogEntryViewModel logEntry)
		{
			OutputLog.Add(logEntry);
			if(OutputLog.Count > 100)
				OutputLog.RemoveAt(0);

			AvailableOutputLogTypes = new ObservableCollection<string>(OutputLog.Select(l => l.Icon).Distinct());
		}

		public void ClearOutput()
		{
			OutputLog.Clear();
			AvailableOutputLogTypes.Clear();
		}
	}
}
