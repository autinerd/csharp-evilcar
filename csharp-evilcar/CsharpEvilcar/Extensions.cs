using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar
{
	/// <summary>
	/// Extensions for <see cref="string"/> and <see cref="System.Collections.Generic.IEnumerable{T}"/>
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// check if 'all' or null or empty string
		/// </summary>
		public static bool AllOrNullOrEmpty(this string str) => str == "all" || string.IsNullOrEmpty(str);
		
		/// <summary>
		/// checks if the given string is an valid index for an collection.
		/// </summary>
		/// <param name="str">String with index</param>
		/// <param name="arr">Collection to check against</param>
		/// <param name="index">The parsed index, if exists</param>
		/// <returns>Whether check was successful</returns>
		public static bool IsValidIndex<T>(this string str, IEnumerable<T> arr, out uint index) => uint.TryParse(str, out index) && index < arr.Count();

		/// <summary>
		/// ElementAt for unsigned int
		/// </summary>
		public static T ElementAt<T>(this IEnumerable<T> source, uint index) => source.ElementAt((int)index);

		/// <summary>
		/// ElementAtOrDefault for unsigned int
		/// </summary>
		public static T ElementAtOrDefault<T>(this IEnumerable<T> source, uint index) => source.ElementAtOrDefault((int)index);

		/// <summary>
		/// checks th level of an case type. "add" -> 0, "add customer" -> 1 etc.
		/// </summary>
		/// <param name="flag">The case type to check against</param>
		/// <returns>The specific level</returns>
		internal static uint LevelOf(this CaseTypeFlags flag)
		{
			uint number = (uint)flag;
			const uint GROUP = 0x1_0000;
			for (uint i = 0; ; i++)
			{
				if (Math.Floor(number / Math.Pow(GROUP, i + 1)) > 0)
				{
					continue;
				}
				return i;
			}
		}

		/// <summary>
		/// The upper command type of a command. "add customer" -> "add" etc.
		/// </summary>
		/// <param name="flag">The command type to check against.</param>
		/// <returns>The base command type</returns>
		internal static CaseTypeFlags BaseTypeOf(this CaseTypeFlags flag) => (CaseTypeFlags)( (uint)flag % (uint)Math.Pow(0x1_0000, flag.LevelOf()) );

		/// <summary>
		/// Conversion from command type to specific descriptor.
		/// </summary>
		/// <param name="flag">Case type to convert</param>
		/// <returns>Converted case descriptor</returns>
		internal static CaseDescriptor ToDescriptor(this CaseTypeFlags flag) => (from c in Cases.CaseList where c.Flags == flag select c).SingleOrDefault();

		/// <summary>
		/// Checks if a number is between the two limits described by the tuple.
		/// </summary>
		/// <param name="tuple">lower and upper limit (included)</param>
		/// <param name="number">number to check against</param>
		/// <returns>Whether number is in the limits or not.</returns>
		internal static bool NumberIsBetween(this (uint, uint) tuple, uint number) => number >= tuple.Item1 && number <= tuple.Item2;

		internal static int IndexOf<T>(this IEnumerable<T> enumerable, T element)
		{
			var a = enumerable.Select((item, index) => (item, index));
			var b = a.Where((item) => item.item.Equals(element)).SingleOrDefault();
			if (b.item == default)
			{
				return -1;
			}
			else
			{
				return b.index;
			}
		}
	}
}
