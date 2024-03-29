﻿using CsharpEvilcar.DataClasses;
using CsharpEvilcar.Prompt;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using static CsharpEvilcar.Database.DatabaseController;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Adds a vehicle.
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Vehicle.Numberplate"/> (0), <see cref="Vehicle.Brand"/> (1), <see cref="Vehicle.Model"/> (2), <see cref="Vehicle.Category"/> (3), Number of <see cref="Fleet"/> (4)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue AddVehicle(IEnumerable<string> parameters)
		{
			string numberplate = parameters.ElementAt(0), brand = parameters.ElementAt(1), model = parameters.ElementAt(2), category = parameters.ElementAt(3), fleet = parameters.ElementAt(4);

			if (!Regex.IsMatch(numberplate, "[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}"))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			if (!fleet.IsValidIndex(DatabaseObject.MyBranch.Fleets, out uint fleetnum))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 4);
			}
			switch (category.ToLower(CultureInfo.CurrentCulture))
			{
				case "small":
					DatabaseObject.MyBranch.Fleets.ElementAt(fleetnum).Vehicles.Add(new SmallVehicle(numberplate, model, brand, false));
					return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
				case "midsize":
					DatabaseObject.MyBranch.Fleets.ElementAt(fleetnum).Vehicles.Add(new MidsizeVehicle(numberplate, model, brand, false));
					return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
				case "large":
					DatabaseObject.MyBranch.Fleets.ElementAt(fleetnum).Vehicles.Add(new LargeVehicle(numberplate, model, brand, false));
					return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
				case "electric":
					DatabaseObject.MyBranch.Fleets.ElementAt(fleetnum).Vehicles.Add(new ElectricVehicle(numberplate, model, brand, false));
					return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
				default:
					return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 3);
			}
		}

		/// <summary>
		/// Adds a customer.
		/// </summary>
		/// <param name="parameters">Parameters: <see cref="Person.Name"/> (0), <see cref="Person.Residence"/> (1)</param>
		/// <returns>Error code</returns>
		internal static ReturnValue AddCustomer(IEnumerable<string> parameters)
		{
			DatabaseObject.Customers.Add(new Customer(false)
			{
				Name = parameters.ElementAt(0),
				Residence = parameters.ElementAt(1)
			});

			return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
		}
	}
}
