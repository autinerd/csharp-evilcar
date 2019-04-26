using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
using CsharpEvilcar.UserInterface;
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
		internal static ErrorCode BookingRent(IEnumerable<string> parameters)
		{
			int vehID, cusID;
			if (!int.TryParse(parameters.ElementAt(0), out vehID) || !int.TryParse(parameters.ElementAt(1), out cusID))
			{
				return ErrorCode.WrongArgument;
			}
			IEnumerable<Customer> cc = from c in DatabaseController.Database.Customers
					 where c.CustomerID == cusID
					 select c;
			if (( from f in DatabaseController.Database.MyBranch.Fleets
				  from v in f.Vehicles
				  where v.VehicleID == vehID
				  select v ).Count() != 1 || cc.Count() != 1)
			{
				return ErrorCode.WrongArgument;
			}
			cc.Single().Bookings.Add(new Booking(false)
			{
				Startdate = DateTime.Today,
				VehicleID = vehID
			});
			return ErrorCode.Success;
		}

		/// <summary>
		/// Places a booking
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Vehicle.VehicleID"/> (0)</param>
		/// <returns>Error code</returns>
		internal static ErrorCode BookingReturn(IEnumerable<string> parameters)
		{
			int vehID;
			if (!int.TryParse(parameters.ElementAt(0), out vehID))
			{
				return ErrorCode.WrongArgument;
			}
			IEnumerable<Booking> vc = from c in DatabaseController.Database.Customers
									  from b in c.Bookings
									  where b.Vehicle.VehicleID == vehID
									  select b;
			if (vc.Count() != 1)
			{
				return ErrorCode.WrongArgument;
			}
			vc.Single().Enddate = DateTime.Today;
			return ErrorCode.Success;
		}
	}
}
