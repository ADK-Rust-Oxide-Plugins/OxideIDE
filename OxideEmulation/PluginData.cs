using System;
using System.Collections.Generic;
using NLua;
using OxideEmulation.Parameters;

namespace OxideEmulation
{
	/// <summary>
	/// Holds information about a plugin
	/// </summary>
	public class PluginData
	{
		readonly Action<string> mInvokeError;
		readonly ParameterFactory mParameterFactory;
		readonly OxideHooks mHookDefinitions;
		readonly ICollection<PluginCallback> mHooks = new List<PluginCallback>();
		readonly ICollection<PluginCallback> mChatCommands = new List<PluginCallback>(); 

		public PluginData(Action<string> invokeError, ParameterFactory parameterFactory, OxideHooks hookDefinitions)
		{
			mInvokeError = invokeError;
			mParameterFactory = parameterFactory;
			mHookDefinitions = hookDefinitions;
		}

		/// <summary>
		/// The title of the plugin defined in PLUGIN.Title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The description of the plugin defined in PLUGIN.Description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Adds the given chat command, called from lua. Stores them among the other plugin hooks
		/// </summary>
		/// <param name="self">The PLUGIN table</param>
		/// <param name="cmd">The command</param>
		/// <param name="callback">The function to execute</param>
		public void AddChatCommand(LuaTable self, string cmd, LuaFunction callback)
		{
			var chatCommand = new PluginCallback(cmd, callback, self, mInvokeError, CreateChatCommandParameters());
			mChatCommands.Add(chatCommand);
			if(ChatCommandAdded != null)
				ChatCommandAdded(chatCommand);
		}

		AParameter[] CreateChatCommandParameters()
		{
			return mHookDefinitions["AddChatCommand"].Parameters.CreateInstances(mParameterFactory);
		}

		/// <summary>
		/// Fires whenever a new chat command is added
		/// </summary>
		public event Action<PluginCallback> ChatCommandAdded;

		/// <summary>
		/// All really avilable hooks present in hooks.xml and the PLUGIN table of this plugin.
		/// </summary>
		public ICollection<PluginCallback> Hooks
		{
			get { return mHooks; }
		}
	}
}