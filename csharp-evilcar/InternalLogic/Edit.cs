using CsharpEvilcar.DataClasses;
using CsharpEvilcar.Prompt;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static CsharpEvilcar.Database.DatabaseController;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Edits a vehicle.
		/// </summary>
		/// <param name="parameters">Parameters: VehicleID (0), Key (1), Value(2)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue EditVehicle(IEnumerable<string> parameters)
		{
			if (!int.TryParse(parameters.ElementAt(0), out int vehID))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			IEnumerable<Fleet> fleets = from f in DatabaseObject.MyBranch.Fleets
										from v in f.Vehicles
										where v.VehicleID == vehID
										select f;

			if (!( fleets.Count() == 1 && fleets.Single().Vehicles.Count((v) => v.VehicleID == vehID) == 1 ))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}

			Vehicle veh = ( from ve in fleets.Single().Vehicles where ve.VehicleID == vehID select ve ).Single();

			switch (parameters.ElementAt(1))
			{
				case "numberplate":
					if (Regex.IsMatch(parameters.ElementAt(2), "^[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}$"))
					{
						veh.Numberplate = parameters.ElementAt(2);
						return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
					}
					return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 2);
				case "fleet":
					int fleetnum;
					if (!int.TryParse(parameters.ElementAt(2), out fleetnum) || DatabaseObject.MyBranch.Fleets.Count() <= fleetnum)
					{
						return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 2);
					}
					if (fleets.Single() == DatabaseObject.MyBranch.Fleets.ElementAt(fleetnum))
					{
						return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
					}
					else
					{
						DatabaseObject.MyBranch.Fleets.ElementAt(fleetnum).Vehicles.Add(veh);
						return fleets.Single().Vehicles.Remove(veh) 
							? ReturnValue.GetValue(ErrorCodeFlags.IsSuccess) 
							: ReturnValue.GetValue(ErrorCodeFlags.IsDatabaseError);

					}
				default:
					return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 1);
			}
		}

		/// <summary>
		/// Edits a customer.
		/// </summary>
		/// <param name="parameters">Parameters: CustomerID (0), Key (1), Value (2)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue EditCustomer(IEnumerable<string> parameters)
		{
			if (!( int.TryParse(parameters.ElementAt(0), out int cusID)
				&& DatabaseObject.Customers.Any((c) => c.CustomerID == cusID) ))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			Customer customer = ( from c in DatabaseObject.Customers where c.CustomerID == cusID select c ).Single();
			switch (parameters.ElementAt(1))
			{
				case "name":
					customer.Name = parameters.ElementAt(2);
					return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
				case "residence":
					customer.Residence = parameters.ElementAt(2);
					return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
				default:
					return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 1);
			}
		}
	}
}
