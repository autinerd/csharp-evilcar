using CsharpEvilcar.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using CsharpEvilcar.Prompt;
using static CsharpEvilcar.Database.DatabaseController;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Places a booking
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Vehicle.VehicleID"/> (0), <see cref="Customer.CustomerID"/> (1)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue BookingRent(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			if (!int.TryParse(parameters.ElementAt(1), out int cusID))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 1);
			}
			IEnumerable<Customer> cc = from c in DatabaseObject.Customers
									   where c.CustomerID == cusID
									   select c;
			if (( from f in DatabaseObject.MyBranch.Fleets
				   from v in f.Vehicles
				   where v.VehicleID == vehID
				   select v ).Count() != 1)
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			if (cc.Count() != 1)
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 1);
			}
			cc.Single().Bookings.Add(new Booking(false)
			{
				Startdate = DateTime.Today,
				VehicleID = vehID
			});
			return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
		}

		/// <summary>
		/// Places a booking
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Vehicle.VehicleID"/> (0)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue BookingReturn(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			IEnumerable<Booking> vc = from c in DatabaseObject.Customers
									  from b in c.Bookings
									  where b.Vehicle.VehicleID == vehID
									  select b;
			if (vc.Count() != 1)
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			vc.Single().Enddate = DateTime.Today;
			return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
		}
	}
}
