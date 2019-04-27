﻿using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
using CsharpEvilcar.UserInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Edits a vehicle.
		/// </summary>
		/// <param name="parameters">Parameters: VehicleID (0), Key (1), Value(2)</param>
		/// <returns>Error code</returns>
		internal static ErrorCode EditVehicle(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID))
			{
				return ErrorCode.WrongArgument;
			}
			IEnumerable<Fleet> fleets = from f in DatabaseController.Database.MyBranch.Fleets
										from v in f.Vehicles
										where v.VehicleID == vehID
										select f;

			if (!( fleets.Count() == 1 && fleets.Single().Vehicles.Count((v) => v.VehicleID == vehID) == 1 ))
			{
				return ErrorCode.WrongArgument;
			}

			Vehicle veh = ( from ve in fleets.Single().Vehicles where ve.VehicleID == vehID select ve ).Single();

			switch (parameters.ElementAt(1))
			{
				case "numberplate":
					if (Regex.IsMatch(parameters.ElementAt(2), "^[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}$"))
					{
						veh.Numberplate = parameters.ElementAt(2);
						return ErrorCode.Success;
					}
					return ErrorCode.WrongArgument;
				case "fleet":
					int fleetnum;
					if (!int.TryParse(parameters.ElementAt(2), out fleetnum) || DatabaseController.Database.MyBranch.Fleets.Count() <= fleetnum)
					{
						return ErrorCode.WrongArgument;
					}
					if (fleets.Single() == DatabaseController.Database.MyBranch.Fleets.ElementAt(fleetnum))
					{
						return ErrorCode.Success;
					}
					else
					{
						DatabaseController.Database.MyBranch.Fleets.ElementAt(fleetnum).Vehicles.Add(veh);
						return fleets.Single().Vehicles.Remove(veh) ? ErrorCode.Success : ErrorCode.DatabaseError;

					}
				default:
					return ErrorCode.WrongArgument;
			}
		}

		/// <summary>
		/// Edits a customer.
		/// </summary>
		/// <param name="parameters">Parameters: CustomerID (0), Key (1), Value(2)</param>
		/// <returns>Error code</returns>
		internal static ErrorCode EditCustomer(IEnumerable<string> parameters)
		{
			if (!( int.TryParse(parameters.ElementAt(0), out int cusID)
				&& DatabaseController.Database.Customers.Any((c) => c.CustomerID == cusID) ))
			{
				return ErrorCode.WrongArgument;
			}
			switch (parameters.ElementAt(1))
			{
				case "name":
					( from c in DatabaseController.Database.Customers where c.CustomerID == cusID select c ).Single().Name = parameters.ElementAt(2);
					return ErrorCode.Success;
				case "residence":
					( from c in DatabaseController.Database.Customers where c.CustomerID == cusID select c ).Single().Residence = parameters.ElementAt(2);
					return ErrorCode.Success;
				default:
					return ErrorCode.WrongArgument;
			}
		}
	}
}