using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace OxideIde.Converters
{
	/// <summary>
	/// Converts the inputs (string filename, bool hasChanges) to a usable window title string.
	/// </summary>
	public class WindowTitleFromPlugin : MarkupExtension, IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			string additional = string.Empty;
			if(values[0] is string)
			{
				additional = "- " + values[0];
			}

			if(values[1] is bool && (bool)values[1])
			{
				additional += "*";
			}
			
			return string.Format("Ox(ide)2 {0}", additional);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}