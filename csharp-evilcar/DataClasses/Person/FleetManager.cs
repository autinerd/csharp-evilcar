using System.Linq;

namespace CsharpEvilcar.DataClasses
{
	/// <summary>
	/// Represents a fleet manager.
	/// </summary>
	class FleetManager : Person
	{
		/// <summary>
		/// The branch associated with the fleet manager.
		/// </summary>
		public Branch Branch => ( from b in Database.DatabaseController.Database.Branches
								  where b.FleetManager == this
								  select b ).Single();
	}
}
