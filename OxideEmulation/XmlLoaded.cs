using System.IO;
using System.Xml.Serialization;

namespace OxideEmulation
{
	/// <summary>
	/// Helper class that can be extended from to load this class from xml
	/// </summary>
	/// <typeparam name="T">The type of object stored in the xml</typeparam>
	public class XmlLoaded<T>
	{
		public static T Load(string path)
		{
			using (var reader = new StreamReader(path))
			{
				return Load(reader);
			}
		}

		public static T Load(TextReader reader)
		{
			var serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(reader);
		}
	}
}