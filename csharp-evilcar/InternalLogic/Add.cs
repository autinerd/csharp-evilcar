using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
using CsharpEvilcar.UserInterface;
using System;
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
		/// <param name="parameters">Parameters: Numberplate (0), Brand (1), Model (2), Category (3), Fleet (4)</param>
		/// <returns>Error code</returns>
		internal static ErrorCode AddVehicle(IEnumerable<string> parameters)
		{
			string numberplate = parameters.ElementAt(0), brand = parameters.ElementAt(1), model = parameters.ElementAt(2), category = parameters.ElementAt(3), fleet = parameters.ElementAt(4);
			int fleetnum;
			if (!Regex.IsMatch(numberplate, "^[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}$") || !int.TryParse(fleet, out fleetnum) || DatabaseController.Database.MyBranch.Fleets.Count() >= fleetnum)
			{
				return ErrorCode.WrongArgument;
			}
			switch (category.ToLower())
			{
				case "small":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new SmallVehicle(numberplate, model, brand, false));
					return ErrorCode.Success;
				case "midsize":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new MidsizeVehicle(numberplate, model, brand, false));
					return ErrorCode.Success;
				case "large":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new LargeVehicle(numberplate, model, brand, false));
					return ErrorCode.Success;
				case "electric":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new ElectricVehicle(numberplate, model, brand, false));
					return ErrorCode.Success;
				default:
					return ErrorCode.WrongArgument;
			}
		}

		/// <summary>
		/// Adds a customer.
		/// </summary>
		/// <param name="parameters">Parameters: Name (0), Residence (1)</param>
		/// <returns>Error code</returns>
		internal static ErrorCode AddCustomer(IEnumerable<string> parameters)
		{
			DatabaseController.Database.Customers.Add(new Customer(false)
			{
				Name = parameters.ElementAt(0),
				Residence = parameters.ElementAt(1)
			});
			return ErrorCode.Success;
		}

		/// <summary>
		/// Adds a booking
		/// </summary>
		/// <param name="parameters">Parameters: Customer ID (0), Vehicle ID (1), Startdate (2), Enddate (3)</param>
		/// <returns>Error code</returns>
		internal static ErrorCode AddBooking(IEnumerable<string> parameters)
		{
			string custID = parameters.ElementAt(0), vehID = parameters.ElementAt(1), startdate = parameters.ElementAt(2), enddate = parameters.ElementAt(3);
			DateTime start, end;
			int cid, vid;
			if (!DateTime.TryParseExact(startdate, "yyyyMMdd", null, DateTimeStyles.None, out start) || !DateTime.TryParseExact(enddate, "yyyyMMdd", null, DateTimeStyles.None, out end) || !int.TryParse(custID, out cid) || !int.TryParse(vehID, out vid))
			{
				return ErrorCode.WrongArgument;
			}
			DatabaseController.Database.Customers.Find(c => c.CustomerID == cid).Bookings.Add(new Booking(false)
			{
				VehicleID = vid,
				Startdate = start,
				Enddate = end
			});
			return ErrorCode.Success;
		}
	}
}
