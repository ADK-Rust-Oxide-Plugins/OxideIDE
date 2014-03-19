using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace OxideIde.Converters
{
	/// <summary>
	/// Returns if the given object was not null.
	/// </summary>
	[ValueConversion(typeof(object), typeof(bool))]
	public class NotNullToBoolean : MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != null;
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