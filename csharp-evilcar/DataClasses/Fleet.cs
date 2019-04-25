using System.Collections.Generic;
using System.Linq;

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

		public override string ToString() => ToString(false);

		public string ToString(bool fullDetails)
		{
			if (fullDetails)
			{
				return "";
			}
			else
			{
				return $"Fleet {Location}: {Vehicles.Count} cars ({string.Join(", ", (from v in Vehicles group v by v.Category into newGroup select newGroup.Count()+" "+System.Enum.GetName(typeof(Vehicle.CategoryEnum), newGroup.Key)))})";
			}
		}
	}
}
