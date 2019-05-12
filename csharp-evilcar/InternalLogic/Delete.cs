using CsharpEvilcar.DataClasses;
using System.Collections.Generic;
using System.Linq;
using CsharpEvilcar.Prompt;
using static CsharpEvilcar.Database.DatabaseController;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Deletes a vehicle.
		/// </summary>
		/// <param name="parameters">Parameters: VehicleID (0)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue DeleteVehicle(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			IEnumerable<Fleet> fleets = from f in DatabaseObject.MyBranch.Fleets
										from v in f.Vehicles
										where v.VehicleID == vehID
										select f;

			return fleets.Count() == 1
				&& fleets.Single().Vehicles.Count((v) => v.VehicleID == vehID) == 1
					? fleets.Single().Vehicles.Remove(( from ve in fleets.Single().Vehicles where ve.VehicleID == vehID select ve ).Single())
						? ReturnValue.GetValue(ErrorCodeFlags.IsSuccess)
						: ReturnValue.GetValue(ErrorCodeFlags.IsDatabaseError)
					: ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
		}

		/// <summary>
		/// Deletes a customer.
		/// </summary>
		/// <param name="parameters">Parameters: CustomerID (0)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue DeleteCustomer(IEnumerable<string> parameters) => 
			int.TryParse(parameters.ElementAt(0), out int cusID)
			&& DatabaseObject.Customers.Any((c) => c.CustomerID == cusID)
				? DatabaseObject.Customers.Remove(( from c in DatabaseObject.Customers where cusID == c.CustomerID select c ).Single())
				? ReturnValue.GetValue(ErrorCodeFlags.IsSuccess)
				: ReturnValue.GetValue(ErrorCodeFlags.IsDatabaseError)
				: ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
	}
}
