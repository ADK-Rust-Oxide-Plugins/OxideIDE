using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using OxideEmulation.Parameters;

namespace OxideIde.ViewModels
{
	/// <summary>
	/// Encapsulates an ArrayParameter for display in the gui.
	/// </summary>
	public class ArrayParameterViewModel : AParameterViewModel
	{
		public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register("Values", typeof(ObservableCollection<AParameterViewModel>), typeof(ArrayParameterViewModel), new PropertyMetadata(default(ObservableCollection<AParameterViewModel>)));

		public ObservableCollection<AParameterViewModel> Values
		{
			get { return (ObservableCollection<AParameterViewModel>) GetValue(ValuesProperty); }
			set { SetValue(ValuesProperty, value); }
		}

		readonly ArrayParameter mParameter;
		readonly ParameterFactory mParameterFactory;

		public ArrayParameterViewModel(ArrayParameter parameter, ParameterFactory parameterFactory)
			: base(parameter)
		{
			Values = new ObservableCollection<AParameterViewModel>(parameter.Values.Select(CreateParameterViewModel));
			mParameter = parameter;
			mParameterFactory = parameterFactory;
		}

		/// <summary>
		/// Add a new item from the default item template specified
		/// </summary>
		public void AddItem()
		{
			var parameter = mParameter.ItemTemplate.CreateInstance(mParameterFactory);
			mParameter.Values.Add(parameter);
			Values.Add(CreateParameterViewModel(parameter));
		}

		/// <summary>
		/// Remove the specified parameter from the array
		/// </summary>
		/// <param name="item">The item to remove</param>
		public void Remove(AParameterViewModel item)
		{
			Values.Remove(item);
			mParameter.Values.Remove(item.Model);
		}
	}
}