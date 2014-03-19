using System.Windows;
using System.Windows.Media;

namespace OxideIde.Helpers
{
	/// <summary>
	/// Defines useful functions for the gui code
	/// </summary>
	public static class Helper
	{
		/// <summary>
		/// Returns the first child of the specified typefound.
		/// </summary>
		/// <typeparam name="T">Type of object to find</typeparam>
		/// <param name="depObj">The object for which to find a child object</param>
		/// <returns>The child object if found or null.</returns>
		public static T GetChildOfType<T>(this DependencyObject depObj) where T : DependencyObject
		{
			if (depObj == null) return null;

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
			{
				var child = VisualTreeHelper.GetChild(depObj, i);

				var result = (child as T) ?? GetChildOfType<T>(child);
				if (result != null) return result;
			}
			return null;
		} 
	}
}