using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Editing;
using OxideEmulation;
using OxideIde.ViewModels;

namespace OxideIde.Helpers
{
	/// <summary>
	/// Encapsulates the current source completion present in Ox(ide)2
	/// </summary>
	public class SourceCompletion : IDisposable
	{
		/// <summary>
		/// The root name of the table where plugins are specified
		/// </summary>
		const string PLUGIN_TABLE = "PLUGIN";

		readonly TextArea mTextArea;
		readonly OxideViewModel mOxide;
		readonly OxideHooks mHookDefinitions;

		public SourceCompletion(TextArea textArea, OxideViewModel oxide, OxideHooks hookDefinitions)
		{
			mTextArea = textArea;
			mOxide = oxide;
			mHookDefinitions = hookDefinitions;

			mTextArea.TextEntered += OnSourceTextEntered;
		}

		CompletionWindow mCompletionWindow;
		void OnSourceTextEntered(object sender, TextCompositionEventArgs e)
		{
			if (e.Text == ".")
			{
				var identifier = GetIdentifier();
				OpenCompletionFor(identifier);
			}
			else if(e.Text == ":")
			{
				var identifier = GetIdentifier();
				if(identifier == PLUGIN_TABLE)
				{
					var alreadyPresent = mOxide.GetCompletionDataFor(PLUGIN_TABLE);
					OpenCompletionWith(mHookDefinitions.Hooks.Where(h => alreadyPresent.All(c => c.Text != h.FunctionName)).Select(h => new HookCompletionData(h)));
				}
			}
		}

		/// <summary>
		/// Open the completion window for the given global identifier
		/// </summary>
		/// <param name="globalIdentifier">The identifier to find all completions for</param>
		void OpenCompletionFor(string globalIdentifier)
		{
			var completionData = mOxide.GetCompletionDataFor(globalIdentifier);
			if(completionData.Any())
			{
				OpenCompletionWith(completionData);
			}
		}

		/// <summary>
		/// Opens the completion window with the given data.
		/// </summary>
		/// <param name="completionData">The completion data to show</param>
		void OpenCompletionWith(IEnumerable<ICompletionData> completionData)
		{
			mCompletionWindow = new CompletionWindow(mTextArea);
			var completion = mCompletionWindow.CompletionList.CompletionData;

			foreach(var data in completionData)
				completion.Add(data);

			mCompletionWindow.Show();
			mCompletionWindow.Closed += delegate { mCompletionWindow = null; };
		}

		/// <summary>
		/// Search for the identifer before the current cursor position.
		/// Currently this uses as simple search to check for letter/digit/_
		/// </summary>
		/// <returns>The found identifier</returns>
		string GetIdentifier()
		{
			var offset = mTextArea.Caret.Offset - 1;
			var word = string.Empty;
			do
			{
				offset--;
				var text = mTextArea.Document.GetText(offset, 1);
				if(char.IsLetterOrDigit(text[0]) || text[0] == '_')
				{
					word = text + word;
				}
				else
				{
					break;
				}
			} while(offset > 0);
			return word;
		}

		public void Dispose()
		{
			mTextArea.TextEntered -= OnSourceTextEntered;
		}
	}
}