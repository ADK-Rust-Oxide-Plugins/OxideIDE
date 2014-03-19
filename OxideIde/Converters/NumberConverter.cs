using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace OxideIde.Converters
{
	/// <summary>
	/// Converts a float to string and back.
	/// </summary>
	[ValueConversion(typeof(float), typeof(string))] 
	public class NumberConverter : MarkupExtension, IValueConverter 
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((float)value).ToString(CultureInfo.InvariantCulture);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return float.Parse((string)value, CultureInfo.InvariantCulture);
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}