using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
using CsharpEvilcar.UserInterface;
using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Deletes a vehicle.
		/// </summary>
		/// <param name="parameters">Parameters: VehicleID (0)</param>
		/// <returns>Error code</returns>
		internal static ErrorCode DeleteVehicle(IEnumerable<string> parameters)
		{
			int vehID;
			if (!int.TryParse(parameters.ElementAt(0), out vehID))
			{
				return ErrorCode.WrongArgument;
			}
			IEnumerable<Fleet> fleets = from f in DatabaseController.Database.MyBranch.Fleets
										from v in f.Vehicles
										where v.VehicleID == vehID
										select f;

			return fleets.Count() == 1 && fleets.Single().Vehicles.Count((v) => v.VehicleID == vehID) == 1
				? fleets.Single().Vehicles.Remove(( from ve in fleets.Single().Vehicles where ve.VehicleID == vehID select ve ).Single())
				? ErrorCode.Success
				: ErrorCode.DatabaseError
				: ErrorCode.WrongArgument;
		}

		/// <summary>
		/// Deletes a customer.
		/// </summary>
		/// <param name="parameters">Parameters: CustomerID (0)</param>
		/// <returns>Error code</returns>
		internal static ErrorCode DeleteCustomer(IEnumerable<string> parameters)
		{
			int cusID;
			return int.TryParse(parameters.ElementAt(0), out cusID) && DatabaseController.Database.Customers.Any((c) => c.CustomerID == cusID)
				? DatabaseController.Database.Customers.Remove(( from c in DatabaseController.Database.Customers where cusID == c.CustomerID select c ).Single())
				? ErrorCode.Success
				: ErrorCode.DatabaseError
				: ErrorCode.WrongArgument;
		}
	}
}
