using System;
using NLua;
using OxideEmulation.Parameters;

namespace OxideEmulation
{
	/// <summary>
	/// Describes a lua function that can be called from the emulation layer at runtime. e.g. Oxide hooks and chat commands
	/// </summary>
	public class PluginCallback
	{
		readonly string mName;
		readonly LuaFunction mCallback;
		readonly LuaTable mSelf;
		readonly Action<string> mInvokeError;
		readonly AParameter[] mParameters;

		public PluginCallback(string name, LuaFunction callback, LuaTable self, Action<string> invokeError, AParameter[] parameters)
		{
			mName = name;
			mCallback = callback;
			mSelf = self;
			mInvokeError = invokeError;
			mParameters = parameters;
		}

		/// <summary>
		/// The name of the callback
		/// </summary>
		public string Name
		{
			get { return mName; }
		}

		/// <summary>
		/// Required parameters for this callback
		/// </summary>
		public AParameter[] Parameters
		{
			get { return mParameters; }
		}

		/// <summary>
		/// A description shown to the user
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Executes this lua function with the given parameter
		/// </summary>
		/// <returns>The results returned from this function or null if there was an error</returns>
		public object[] Call()
		{
			try
			{
				var args = new object[Parameters.Length + 1];
				args[0] = mSelf;
				for(var i = 0; i < Parameters.Length;i++)
					args[i + 1] = Parameters[i].GetParameterValue();
				return mCallback.Call(args);
			}
			catch(Exception e)
			{
				mInvokeError(e.Message);
				return null;
			}
		}
	}
}