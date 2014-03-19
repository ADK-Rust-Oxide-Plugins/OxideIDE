using System;
using System.Linq;
using System.Windows;
using OxideEmulation;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Encapsulates a PluginCallback for display in the gui
	/// </summary>
	public class CallbackViewModel : DependencyObject
	{
		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(CallbackViewModel), new PropertyMetadata(default(string)));
		public static readonly DependencyProperty ParametersProperty = DependencyProperty.Register("Parameters", typeof(AParameterViewModel[]), typeof(CallbackViewModel), new PropertyMetadata(default(AParameterViewModel[])));
		public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(CallbackViewModel), new PropertyMetadata(default(string)));

		readonly Action<LogEntryViewModel> mAddLogEntry;
		readonly PluginCallback mHook;

		public CallbackViewModel(PluginCallback hook, Action<LogEntryViewModel> addLogEntry)
		{
			mHook = hook;
			mAddLogEntry = addLogEntry;
			Name = hook.Name;
			Description = hook.Description;
			Parameters = hook.Parameters.Select(AParameterViewModel.CreateParameterViewModel).ToArray();
		}

		public CallbackViewModel()
		{
			
		}

		public string Description
		{
			get { return (string) GetValue(DescriptionProperty); }
			set { SetValue(DescriptionProperty, value); }
		}

		/// <summary>
		/// The parameters this function needs to work
		/// </summary>
		public AParameterViewModel[] Parameters
		{
			get { return (AParameterViewModel[])GetValue(ParametersProperty); }
			set { SetValue(ParametersProperty, value); }
		}

		public string Name
		{
			get { return (string) GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		/// <summary>
		/// Call the callback with the parameter values exposed by Parameters
		/// </summary>
		/// <returns></returns>
		public object[] Call()
		{
			var parameters = string.Join(", ", mHook.Parameters.Select(p => p.ToString()));
			mAddLogEntry(new LogEntryViewModel { Message = string.Format("Calling {0}({1})", Name, parameters), Icon = Constants.LOG_INFO });
			var result = mHook.Call();
			mAddLogEntry(new LogEntryViewModel { Message = string.Format("Finished {0}", Name), Icon = Constants.LOG_INFO });
			return result;
		}
	}
}