using System;

namespace CsharpEvilcar.DataClasses
{
	/// <summary>
	/// The GuidObject represents all classes which have a GUID.
	/// </summary>
	abstract class GuidObject
	{
		/// <summary>
		/// The GUID of the object.
		/// </summary>
		public Guid GUID { get; set; } = Guid.Empty;
	}
}
