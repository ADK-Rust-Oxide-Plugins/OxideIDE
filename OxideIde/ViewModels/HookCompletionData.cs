using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using OxideEmulation;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Provides the necessary description for the completion of one entry in the plugin hook completion list.
	/// </summary>
	public class HookCompletionData : ICompletionData
	{
		readonly OxideHook mHook;

		public HookCompletionData(OxideHook hook)
		{
			mHook = hook;
			Text = hook.FunctionName;
			Image = new BitmapImage(new Uri("pack://application:,,,/OxideIde;component/Icons/AutoComplete/function_16x16.png"));
			Description = hook.Description;
			Content = Text;
		}

		/// <summary>
		/// If you request to complete a hook it will be filled with the whole function definition.
		/// </summary>
		public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
		{
			textArea.Document.Replace(completionSegment, string.Format("{0}( {1} ){2}{2}end", mHook.FunctionName, string.Join(", ", mHook.Parameters.Select(p => p.Name)), Environment.NewLine));
		}

		public ImageSource Image { get; private set; }
		public string Text { get; private set; }
		public object Content { get; private set; }
		public object Description { get; private set; }
		public double Priority { get; private set; }
	}
}