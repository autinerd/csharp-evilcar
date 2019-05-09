using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar
{
	/// <summary>
	/// Extensions for <see cref="string"/> and <see cref="System.Collections.Generic.IEnumerable{T}"/>
	/// </summary>
	public static class Extensions
	{
		public static bool AllOrNullOrEmpty(this string str) => str == "all" || string.IsNullOrEmpty(str);

		public static bool IsValidIndex<T>(this string str, IEnumerable<T> arr, out uint index) => uint.TryParse(str, out index) && index < arr.Count();

		public static T ElementAt<T>(this IEnumerable<T> source, uint index) => source.ElementAt((int)index);

		public static T ElementAtOrDefault<T>(this IEnumerable<T> source, uint index) => source.ElementAtOrDefault((int)index);
	}
}
