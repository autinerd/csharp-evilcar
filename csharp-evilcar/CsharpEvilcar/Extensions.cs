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
		public static bool AllOrNullOrEmpty(this string str) => str == "all" || string.IsNullOrEmpty(str);

		public static bool IsValidIndex<T>(this string str, IEnumerable<T> arr, out uint index) => uint.TryParse(str, out index) && index < arr.Count();

		public static T ElementAt<T>(this IEnumerable<T> source, uint index) => source.ElementAt((int)index);

		public static T ElementAtOrDefault<T>(this IEnumerable<T> source, uint index) => source.ElementAtOrDefault((int)index);

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

		internal static CaseTypeFlags BaseTypeOf(this CaseTypeFlags flag) => (CaseTypeFlags)( (uint)flag % (uint)Math.Pow(0x1_0000, flag.LevelOf()) );

		internal static CaseDescriptor ToDescriptor(this CaseTypeFlags flag) => (from c in Cases.CaseList where c.Flags == flag select c).SingleOrDefault();
	}
}
