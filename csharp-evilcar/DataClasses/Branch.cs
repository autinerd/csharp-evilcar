using System;
using System.Collections.Generic;

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
		public IEnumerable<Fleet> Fleets { get; set; } = null;

		/// <summary>
		/// The associated fleet manager.
		/// </summary>
		public FleetManager FleetManager { get; set; } = null;
	}
}
