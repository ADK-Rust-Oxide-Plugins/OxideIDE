using System;
using System.Collections.Generic;
using System.IO;
using NLua;
using OxideEmulation.Parameters;

namespace OxideEmulation
{
	/// <summary>
	/// Main class for emulating oxide. Holds the reference to the lua interpreter
	/// </summary>
	public class Oxide
	{
		readonly OxideHooks mHookDefinitions;
		readonly Lua mLua = new Lua();
		readonly ParameterFactory mParameterFactory;

		public Oxide(Action<string, string> log, OxideHooks hookDefinitions, IDictionary<string, Delegate> additionalFunctions, IDictionary<string, object> additionalObjects)
		{
			mHookDefinitions = hookDefinitions;

			ExecuteScriptsFromFolder(log, Constants.SCRIPTS_SETUP_FIRST_PASS);

			foreach(var entry in additionalFunctions)
				RegisterFunction(entry.Key, entry.Value);

			foreach(var entry in additionalObjects)
				RegisterObject(entry.Key, entry.Value);

			ExecuteScriptsFromFolder(log, Constants.SCRIPTS_SETUP);
			
			mParameterFactory = new ParameterFactory(mLua);
		}

		/// <summary>
		/// Executes all scripts from the given folder within PATH_SCRIPTS with a .lua extension
		/// </summary>
		/// <param name="log">The function called when something needs to be logged. First parameter is the icon, second is the message.</param>
		/// <param name="scriptFolder">The folder within PATH_SCRIPTS</param>
		void ExecuteScriptsFromFolder(Action<string, string> log, string scriptFolder)
		{
			var scriptsSetupDirectory = Path.Combine(Constants.PATH_SCRIPTS, scriptFolder);
			foreach(var file in Directory.EnumerateFiles(scriptsSetupDirectory, "*.lua", SearchOption.AllDirectories))
			{
				try
				{
					mLua.DoFile(file);
				}
				catch(Exception e)
				{
					log("Icons\\error.png", e.ToString());
				}
			}
		}

		/// <summary>
		/// Registers an object into the specified field of _OxideIde_ inside lua
		/// </summary>
		/// <param name="field">The name of the new field</param>
		/// <param name="obj">The object to register with lua</param>
		public void RegisterObject(string field, object obj)
		{
			((LuaTable) mLua["_OxideIde_"])[field] = obj;
		}

		/// <summary>
		/// Registers a function into the specified field of _OxideIde_ inside lua
		/// </summary>
		/// <param name="name">The name of the field</param>
		/// <param name="function">The function to register</param>
		public void RegisterFunction(string name, Delegate function)
		{
			mLua.RegisterFunction("_OxideIde_." + name, function.Target, function.Method);
		}

		/// <summary>
		/// Loads the given plugin text into the lua instance.
		/// </summary>
		/// <param name="name">The name of the plugin</param>
		/// <param name="pluginText">The source code of the plugin</param>
		/// <returns>Data about the plugin</returns>
		public PluginData LoadPlugin(string name, string pluginText)
		{
			var pluginData = new PluginData(InvokeError, ParameterFactory, mHookDefinitions);
			try
			{
				mLua.RegisterFunction("PLUGIN.AddChatCommand", pluginData, typeof(PluginData).GetMethod("AddChatCommand"));
				mLua.DoString(pluginText, name);
			}
			catch(Exception e)
			{
				InvokeError(e.Message);
			}
			FillPluginData(pluginData);
			return pluginData;
		}

		/// <summary>
		/// Fill PluginData with data from the executed lua plugin
		/// </summary>
		/// <param name="pluginData">The plugin data object to fill</param>
		void FillPluginData(PluginData pluginData)
		{
			pluginData.Title = GetPluginField<string>("Title");
			pluginData.Description = GetPluginField<string>("Description");
			var plugin = Plugin;
			foreach(var hook in mHookDefinitions.Hooks)
			{
				var field = plugin[hook.FunctionName];
				var callback = field as LuaFunction;
				if(callback != null)
				{
					pluginData.Hooks.Add(new PluginCallback(hook.FunctionName, callback, plugin, InvokeError, hook.Parameters.CreateInstances(mParameterFactory))
					{
						Description = hook.Description,
					});
				}
			}
		}

		/// <summary>
		/// Fast access to the PLUGIN table inside lua
		/// </summary>
		LuaTable Plugin
		{
			get { return ((LuaTable) mLua["PLUGIN"]); }
		}

		public ParameterFactory ParameterFactory
		{
			get { return mParameterFactory; }
		}

		/// <summary>
		/// Gets the specified field from PLUGIN and tries to cast it to the given type
		/// </summary>
		/// <typeparam name="T">The target type of the field</typeparam>
		/// <param name="fieldName">The name of the field</param>
		/// <returns>The given field or the default value for that type if the field is not present or of an incorrect type</returns>
		T GetPluginField<T>(string fieldName)
		{
			try
			{
				return (T)Plugin[fieldName];
			}
			catch(Exception e)
			{
				InvokeError(e.Message);
				return default(T);
			}
		}

		void InvokeError(string message)
		{
			if(Error != null)
				Error(message);
		}

		public event Action<string> Error;

		/// <summary>
		/// Gets variable from the global scope in lua
		/// </summary>
		/// <param name="identifier">The name of the variable</param>
		/// <returns>The value of the variable0</returns>
		public object GetGlobal(string identifier)
		{
			return mLua[identifier];
		}
	}
}
