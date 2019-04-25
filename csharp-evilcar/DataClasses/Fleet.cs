using System.Collections.Generic;

namespace CsharpEvilcar.DataClasses
{
	/// <summary>
	/// Represents a fleet
	/// </summary>
	class Fleet : GuidObject
	{
		/// <summary>
		/// List of vehicles.
		/// </summary>
		public List<Vehicle> Vehicles { get; set; } = null;
		/// <summary>
		/// Location of the fleet.
		/// </summary>
		public string Location { get; set; }
	}
}
