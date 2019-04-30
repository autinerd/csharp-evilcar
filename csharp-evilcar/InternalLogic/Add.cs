using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
using CsharpEvilcar.UserInterface;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Adds a vehicle.
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Vehicle.Numberplate"/> (0), <see cref="Vehicle.Brand"/> (1), <see cref="Vehicle.Model"/> (2), <see cref="Vehicle.Category"/> (3), Number of <see cref="Fleet"/> (4)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue.Typ AddVehicle(IEnumerable<string> parameters)
		{
			string numberplate = parameters.ElementAt(0), brand = parameters.ElementAt(1), model = parameters.ElementAt(2), category = parameters.ElementAt(3), fleet = parameters.ElementAt(4);
			if (!( Regex.IsMatch(numberplate, "[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}")
				&& int.TryParse(fleet, out int fleetnum)
				&& DatabaseController.Database.MyBranch.Fleets.Count() >= fleetnum ))
			{
				return ReturnValue.WrongArgument();
			}
			switch (category.ToLower(CultureInfo.CurrentCulture))
			{
				case "small":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new SmallVehicle(numberplate, model, brand, false));
					return ReturnValue.Success();
				case "midsize":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new MidsizeVehicle(numberplate, model, brand, false));
					return ReturnValue.Success();
				case "large":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new LargeVehicle(numberplate, model, brand, false));
					return ReturnValue.Success();
				case "electric":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new ElectricVehicle(numberplate, model, brand, false));
					return ReturnValue.Success();
				default:
					return ReturnValue.WrongArgument();
			}
		}

		/// <summary>
		/// Adds a customer.
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Person.Name"/> (0), <see cref="Person.Residence"/> (1)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue.Typ AddCustomer(IEnumerable<string> parameters)
		{
			DatabaseController.Database.Customers.Add(new Customer(false)
			{
				Name = parameters.ElementAt(0),
				Residence = parameters.ElementAt(1)
			});

			return ReturnValue.Success();
		}
	}
}
