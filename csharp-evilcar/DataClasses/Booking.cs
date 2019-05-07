using System;
using System.Linq;

namespace CsharpEvilcar.DataClasses
{
	class Booking : GuidObject
	{
		public Customer Customer => ( from c in Database.DatabaseController.Database.Customers
									  from b in c.Bookings
									  where b.GUID == GUID
									  select c ).Single();
		public Vehicle Vehicle => ( from b in Database.DatabaseController.Database.Branches
									from f in b.Fleets
									from v in f.Vehicles
									where v.VehicleID == VehicleID
									select v ).Single();
		internal int VehicleID { get; set; }
		public DateTime Startdate { get; set; } = default;
		public DateTime Enddate { get; set; } = default;

		public decimal Price => Vehicle.TotalDayPrice * (decimal)Math.Floor(( (Enddate == default ? DateTime.Today : Enddate) - Startdate ).Add(new TimeSpan(1, 0, 0, 0)).TotalDays);
		public int BookingID { get; set; }

		public Booking(bool hasBookID)
		{
			if (!hasBookID)
			{
				BookingID = ( from c in Database.DatabaseController.Database.Customers
							  from b in c.Bookings
							  select b.BookingID ).Max() + 1;
			}
		}

		public override string ToString() => ToString(false);

		public string ToString(bool fullDetails) => fullDetails
		? $@"Booking {BookingID}:
	Vehicle {Vehicle}
	from {Startdate.ToShortDateString()} {((Enddate == default) ? "" : "to " + Enddate.ToShortDateString())}
	{(Enddate == default ? "Current" : "Total")} cost: {Price} EUR"
		: $"Booking {BookingID}: Vehicle {VehicleID}, from {Startdate.ToShortDateString()} {(Enddate == default ? "" : "to " + Enddate.ToShortDateString())}";
	}
}
