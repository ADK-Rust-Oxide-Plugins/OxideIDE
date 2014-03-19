using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Search;
using OxideIde.Helpers;
using OxideIde.ViewModels;

namespace OxideIde.Windows
{
	/// <summary>
	/// Interaction logic for SourceEditor.xaml
	/// </summary>
	public partial class SourceEditor
	{
		public static readonly DependencyProperty AutocompletionActiveProperty = DependencyProperty.Register("AutocompletionActive", typeof(bool), typeof(SourceEditor), new PropertyMetadata(OnAutocompletionChanged));

		SourceCompletion mCompletion;

		public SourceEditor()
		{
			InitializeComponent();

			Editor.TextArea.DefaultInputHandler.NestedInputHandlers.Add(new SearchInputHandler(Editor.TextArea));

			DataContextChanged += OnDataContextChanged;
		}

		public bool AutocompletionActive
		{
			get { return (bool) GetValue(AutocompletionActiveProperty); }
			set { SetValue(AutocompletionActiveProperty, value); }
		}

		IdeViewModel Context
		{
			get { return (IdeViewModel) DataContext; }
		}

		void HasPluginLoaded(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = Context != null && Context.CurrentPlugin != null;
			e.Handled = true;
		}

		void OnSave(object sender, ExecutedRoutedEventArgs e)
		{
			Context.CurrentPlugin.Save();
			e.Handled = true;
		}

		void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			DependencyPropertyDescriptor.FromProperty(IdeViewModel.CurrentPluginProperty, typeof(IdeViewModel))
				.AddValueChanged(e.NewValue, OnPluginChanged);
		}

		void OnPluginChanged(object sender, EventArgs e)
		{
			AutocompletionActive = true;
		}

		static void OnAutocompletionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var instance = (SourceEditor) d;
			if ((bool)e.NewValue)
				instance.CreateAutocompletion();
			else
				instance.DisposeCompletion();
		}

		void CreateAutocompletion()
		{
			mCompletion = new SourceCompletion(Editor.TextArea, Context.Oxide, Context.HookDefinitions);
		}

		void DisposeCompletion()
		{
			if (mCompletion != null)
			{
				mCompletion.Dispose();
				mCompletion = null;
			}
		}
	}
}
