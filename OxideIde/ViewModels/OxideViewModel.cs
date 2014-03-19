using System;
using System.Collections.Generic;
using System.Windows;
using ICSharpCode.AvalonEdit.CodeCompletion;
using NLua;
using OxideEmulation;
using OxideEmulation.Templates;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Encapsulates the interaction with the oxide emulation layer
	/// </summary>
	public class OxideViewModel : DependencyObject
	{
		readonly Action<LogEntryViewModel> mAddLogEntry;
		readonly Oxide mOxide;

		public OxideViewModel(Action<LogEntryViewModel> addLogEntry, ParameterTemplateStorage templateStorage, OxideHooks hookDefinitions)
		{
			mAddLogEntry = addLogEntry;
			Action<string, string> logAction = (icon, msg) => addLogEntry(new LogEntryViewModel{Message = msg, Icon = icon});
			mOxide = new Oxide(logAction, hookDefinitions,
				new Dictionary<string, Delegate>
				{
					{"Log", logAction},
					{"_InternalDebug_",  Delegate.CreateDelegate(typeof(Action<object>), this, "InternalDebug")},
				},
				new Dictionary<string, object>
				{
					{"Templates", templateStorage},
				});
			mOxide.RegisterObject("ParameterFactory", mOxide.ParameterFactory);

			AParameterViewModel.ParameterFactory = mOxide.ParameterFactory;
			mOxide.Error += OnError;
		}

		/// <summary>
		/// Function to put a breakpoint in for debugging what lua passes as object.
		/// </summary>
		void InternalDebug(object o)
		{
			
		}

		void OnError(string message)
		{
			mAddLogEntry(new LogEntryViewModel { Message = message, Icon = "Icons\\Log\\error.png" });
		}

		/// <summary>
		/// Loads the specified plugin into the oxide emulation layer
		/// </summary>
		/// <param name="name">The plugin name</param>
		/// <param name="pluginText">The plugin source code</param>
		/// <returns></returns>
		public PluginData LoadPlugin(string name, string pluginText)
		{
			return mOxide.LoadPlugin(name, pluginText);
		}

		/// <summary>
		/// Returns list with all fields and methods of the given global lua table.
		/// </summary>
		/// <param name="identifier">The global identifier for which to autocomplete</param>
		/// <returns>List of completion data</returns>
		public IEnumerable<ICompletionData> GetCompletionDataFor(string identifier)
		{
			var result = mOxide.GetGlobal(identifier);
			var table = result as LuaTable;
			if(table != null)
			{
				var completions = new List<ICompletionData>();
				var enumerator = table.GetEnumerator();
				while(enumerator.MoveNext())
				{
					var entry = (KeyValuePair<object, object>)enumerator.Current;
					completions.Add(CompletionData.CreateFor(entry.Key, entry.Value));
				}
				return completions;

			}
			return new ICompletionData[0];
		}
	}
}