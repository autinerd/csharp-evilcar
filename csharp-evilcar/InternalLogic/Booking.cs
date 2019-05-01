using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Places a booking
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Vehicle.VehicleID"/> (0), <see cref="Customer.CustomerID"/> (1)</param>
		/// <returns>Error code</returns>
		internal static Prompt.ReturnValue.Typ BookingRent(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID)
				|| !int.TryParse(parameters.ElementAt(1), out int cusID))
			{
				return Prompt.ReturnValue.WrongArgument();
			}
			IEnumerable<Customer> cc = from c in DatabaseController.Database.Customers
					 where c.CustomerID == cusID
					 select c;
			if (!( ( from f in DatabaseController.Database.MyBranch.Fleets
				  from v in f.Vehicles
				  where v.VehicleID == vehID
				  select v ).Count() == 1 
				  && cc.Count() == 1 ))
			{
				return Prompt.ReturnValue.WrongArgument();
			}
			cc.Single().Bookings.Add(new Booking(false)
			{
				Startdate = DateTime.Today,
				VehicleID = vehID
			});
			return Prompt.ReturnValue.Success();
		}

		/// <summary>
		/// Places a booking
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Vehicle.VehicleID"/> (0)</param>
		/// <returns>Error code</returns>
		internal static Prompt.ReturnValue.Typ BookingReturn(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID))
			{
				return Prompt.ReturnValue.WrongArgument();
			}
			IEnumerable<Booking> vc = from c in DatabaseController.Database.Customers
									  from b in c.Bookings
									  where b.Vehicle.VehicleID == vehID
									  select b;
			if (vc.Count() != 1)
			{
				return Prompt.ReturnValue.WrongArgument();
			}
			vc.Single().Enddate = DateTime.Today;
			return Prompt.ReturnValue.Success();
		}
	}
}
