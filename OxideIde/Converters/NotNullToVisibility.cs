using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace OxideIde.Converters
{
	/// <summary>
	/// Converts a not null check to a visibility enum.
	/// Parameter can be used to determine if Visible.Hidden or Visible.Collapsed is wanted in case of a null value.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(Visibility), ParameterType = typeof(Visibility))]
	public class NotNullToVisibility : MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != DependencyProperty.UnsetValue && value != null 
					? Visibility.Visible 
					: (Visibility)parameter;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}