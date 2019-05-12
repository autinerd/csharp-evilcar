using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar.DataClasses
{
	/// <summary>
	/// Represents a branch.
	/// </summary>
	class Branch : GuidObject
	{
		/// <summary>
		/// Whether the branch is allowed to edit or not.
		/// </summary>
		public bool Editable => FleetManager != null &&
			FleetManager.GUID == Database.DatabaseController.CurrentUser &&
			Database.DatabaseController.CurrentUser != Guid.Empty;

		/// <summary>
		/// List of fleets.
		/// </summary>
		public List<Fleet> Fleets { get; set; } = null;

		/// <summary>
		/// The associated fleet manager.
		/// </summary>
		public FleetManager FleetManager { get; set; } = null;
		/// <summary>
		/// The Location of the branch
		/// </summary>
		public string Location { get; set; } = null;

		public override string ToString() => ToString(false);

		public string ToString(bool fullDetails) => fullDetails
				? $@"Branch {Database.DatabaseController.DatabaseObject.Branches.IndexOf(this)} {Location}:
	Fleet manager: {FleetManager.ToString()}
	Mode: {(Editable ? "(editable)" : "(readonly)")}
	Fleets:
		{string.Join("\n\t\t", from f in Fleets select f.ToString())}"
				: $@"Branch {Database.DatabaseController.DatabaseObject.Branches.IndexOf(this)} {Location}: Fleet manager {FleetManager.Name} {(Editable ? "(editable)" : "(readonly)")}, {Fleets.Count} fleets";
	}
}
