using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace OxideIde.Converters
{
	/// <summary>
	/// Simple passthrough converter to set a breakpoint in for debugging.
	/// </summary>
	[ValueConversion(typeof(object), typeof(object))]
	public class DebugConverter : MarkupExtension, IValueConverter
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}