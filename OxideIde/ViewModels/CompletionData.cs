using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using NLua;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Provides the necessary description for the completion of one entry in the completion list.
	/// </summary>
	public class CompletionData : ICompletionData
	{
		public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
		{
			textArea.Document.Replace(completionSegment, Text);
		}

		public ImageSource Image { get; set; }
		public string Text { get; set; }

		public object Content
		{
			get { return Text; }
		}
		public object Description { get; set; }
		public double Priority { get; private set; }

		readonly static IDictionary<Type, string> sIcons = new Dictionary<Type, string>
		{
			{typeof(LuaFunction), "AutoComplete/function_16x16.png"}
		};

		public static ICompletionData CreateFor(object key, object value)
		{
			var image = string.Empty;
			if(value == null || !sIcons.TryGetValue(value.GetType(), out image))
			{
				image = "AutoComplete/field_16x16.png";
			}

			var imageUri = string.Format("pack://application:,,,/OxideIde;component/Icons/{0}", image);
			return new CompletionData
			{
				Text = key.ToString(),
				Image = new BitmapImage(new Uri(imageUri))
			};
		}
	}
}