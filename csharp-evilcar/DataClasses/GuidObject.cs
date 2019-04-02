using System;

namespace CsharpEvilcar.DataClasses
{
	abstract class GuidObject
	{
		public Guid GUID { get; set; } = Guid.Empty;
	}
}
