using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class Booking : GuidObject
	{
		public Customer Customer => ( from c in Database.DatabaseController.Database.Customers
									  from b in c.Bookings where b.GUID == GUID
									  select c ).Single();
		public Vehicle Vehicle => ( from b in Database.DatabaseController.Database.Branches
									from f in b.Fleets
									from v in f.Vehicles
									where v.GUID == VehicleGuid
									select v ).Single();
		internal Guid VehicleGuid { get; set; }
		public DateTime Startdate { get; set; } = default(DateTime);
		public DateTime Enddate { get; set; } = default(DateTime);
	}
}
