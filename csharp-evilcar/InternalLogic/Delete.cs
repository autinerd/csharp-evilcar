using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
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
		internal static Prompt.ReturnValue.Typ DeleteVehicle(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID))
			{
				return Prompt.ReturnValue.WrongArgument();
			}
			IEnumerable<Fleet> fleets = from f in DatabaseController.Database.MyBranch.Fleets
										from v in f.Vehicles
										where v.VehicleID == vehID
										select f;

			return fleets.Count() == 1
				&& fleets.Single().Vehicles.Count((v) => v.VehicleID == vehID) == 1
				? fleets.Single().Vehicles.Remove(( from ve in fleets.Single().Vehicles where ve.VehicleID == vehID select ve ).Single())
				? Prompt.ReturnValue.Success()
				: Prompt.ReturnValue.DatabaseError()
				: Prompt.ReturnValue.WrongArgument();
		}

		/// <summary>
		/// Deletes a customer.
		/// </summary>
		/// <param name="parameters">Parameters: CustomerID (0)</param>
		/// <returns>Error code</returns>
		internal static Prompt.ReturnValue.Typ DeleteCustomer(IEnumerable<string> parameters) => int.TryParse(parameters.ElementAt(0), out int cusID)
			&& DatabaseController.Database.Customers.Any((c) => c.CustomerID == cusID)
				? DatabaseController.Database.Customers.Remove(( from c in DatabaseController.Database.Customers where cusID == c.CustomerID select c ).Single())
				? Prompt.ReturnValue.Success()
				: Prompt.ReturnValue.DatabaseError()
				: Prompt.ReturnValue.WrongArgument();
	}
}
