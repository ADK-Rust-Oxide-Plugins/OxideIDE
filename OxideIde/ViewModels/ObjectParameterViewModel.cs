using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using OxideEmulation.Parameters;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Encapsulates an ObjectParameter for display in the gui
	/// </summary>
	public class ObjectParameterViewModel : AParameterViewModel
	{
		public static readonly DependencyProperty FieldsProperty = DependencyProperty.Register("Fields", typeof(ObservableCollection<AParameterViewModel>), typeof(ObjectParameterViewModel), new PropertyMetadata(default(ObservableCollection<AParameterViewModel>)));

		public ObservableCollection<AParameterViewModel> Fields
		{
			get { return (ObservableCollection<AParameterViewModel>) GetValue(FieldsProperty); }
			set { SetValue(FieldsProperty, value); }
		}

		ObjectParameter mParameter;

		public ObjectParameterViewModel(ObjectParameter parameter) 
			: base(parameter)
		{
			mParameter = parameter;
			Fields = new ObservableCollection<AParameterViewModel>(parameter.Fields.Select(CreateParameterViewModel));
		}
	}
}