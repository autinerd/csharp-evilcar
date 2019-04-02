using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class Branch : GuidObject
	{
		public bool Editable => FleetManager != null && 
			FleetManager.GUID == Database.DatabaseController.CurrentUser &&
			Database.DatabaseController.CurrentUser != Guid.Empty;

		public IEnumerable<Fleet> Fleets { get; set; } = null;
		public FleetManager FleetManager { get; set; } = null;
	}
}
