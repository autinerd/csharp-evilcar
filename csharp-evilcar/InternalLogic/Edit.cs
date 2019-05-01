using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
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
		internal static Prompt.ReturnValue.Typ EditVehicle(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID))
			{
				return Prompt.ReturnValue.WrongArgument();
			}
			IEnumerable<Fleet> fleets = from f in DatabaseController.Database.MyBranch.Fleets
										from v in f.Vehicles
										where v.VehicleID == vehID
										select f;

			if (!( fleets.Count() == 1 && fleets.Single().Vehicles.Count((v) => v.VehicleID == vehID) == 1 ))
			{
				return Prompt.ReturnValue.WrongArgument();
			}

			Vehicle veh = ( from ve in fleets.Single().Vehicles where ve.VehicleID == vehID select ve ).Single();

			switch (parameters.ElementAt(1))
			{
				case "numberplate":
					if (Regex.IsMatch(parameters.ElementAt(2), "^[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}$"))
					{
						veh.Numberplate = parameters.ElementAt(2);
						return Prompt.ReturnValue.Success();
					}
					return Prompt.ReturnValue.WrongArgument();
				case "fleet":
					int fleetnum;
					if (!int.TryParse(parameters.ElementAt(2), out fleetnum) || DatabaseController.Database.MyBranch.Fleets.Count() <= fleetnum)
					{
						return Prompt.ReturnValue.WrongArgument();
					}
					if (fleets.Single() == DatabaseController.Database.MyBranch.Fleets.ElementAt(fleetnum))
					{
						return Prompt.ReturnValue.Success();
					}
					else
					{
						DatabaseController.Database.MyBranch.Fleets.ElementAt(fleetnum).Vehicles.Add(veh);
						return fleets.Single().Vehicles.Remove(veh) ? Prompt.ReturnValue.Success() : Prompt.ReturnValue.DatabaseError();

					}
				default:
					return Prompt.ReturnValue.WrongArgument();
			}
		}

		/// <summary>
		/// Edits a customer.
		/// </summary>
		/// <param name="parameters">Parameters: CustomerID (0), Key (1), Value(2)</param>
		/// <returns>Error code</returns>
		internal static Prompt.ReturnValue.Typ EditCustomer(IEnumerable<string> parameters)
		{
			if (!( int.TryParse(parameters.ElementAt(0), out int cusID)
				&& DatabaseController.Database.Customers.Any((c) => c.CustomerID == cusID) ))
			{
				return Prompt.ReturnValue.WrongArgument();
			}
			switch (parameters.ElementAt(1))
			{
				case "name":
					( from c in DatabaseController.Database.Customers where c.CustomerID == cusID select c ).Single().Name = parameters.ElementAt(2);
					return Prompt.ReturnValue.Success();
				case "residence":
					( from c in DatabaseController.Database.Customers where c.CustomerID == cusID select c ).Single().Residence = parameters.ElementAt(2);
					return Prompt.ReturnValue.Success();
				default:
					return Prompt.ReturnValue.WrongArgument();
			}
		}
	}
}
