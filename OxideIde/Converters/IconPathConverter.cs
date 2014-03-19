using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace OxideIde.Converters
{
	/// <summary>
	/// Converts the icon path given as value into something suitable in the windows subfolder of OxideIde. 
	/// Defined here for easier migration in the future.
	/// </summary>
	[ValueConversion(typeof(string), typeof(string))]
	public class IconPathConverter : MarkupExtension, IValueConverter
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return "../" + ((string) value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}