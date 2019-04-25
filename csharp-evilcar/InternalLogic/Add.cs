using CsharpEvilcar.Database;
using CsharpEvilcar.DataClasses;
using CsharpEvilcar.UserInterface;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CsharpEvilcar
{
	internal partial class InternalLogic
	{
		/// <summary>
		/// Adds a vehicle.
		/// </summary>
		/// <param name="numberplate">The Numberplate in format "X-X-0" - "XXX-XX-0000"</param>
		/// <param name="brand">The brand of the vehicle</param>
		/// <param name="model">The model of the vehicle</param>
		/// <param name="category">The category of the vehicle: "small", "midsize", "large", "electric"</param>
		/// <param name="fleet">The fleet number</param>
		/// <returns>Error code</returns>
		internal ErrorCode AddVehicle(string numberplate, string brand, string model, string category, string fleet)
		{
			int fleetnum;
			bool success = int.TryParse(fleet, out fleetnum);
			if (!Regex.IsMatch(numberplate, "^[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}$") || !success || DatabaseController.Database.MyBranch.Fleets.Count() >= fleetnum)
			{
				return ErrorCode.WrongArgument;
			}
			switch (category.ToLower())
			{
				case "small":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new DataClasses.SmallVehicle(numberplate, model, brand, false));
					return ErrorCode.Success;
				case "midsize":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new DataClasses.MidsizeVehicle(numberplate, model, brand, false));
					return ErrorCode.Success;
				case "large":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new DataClasses.LargeVehicle(numberplate, model, brand, false));
					return ErrorCode.Success;
				case "electric":
					DatabaseController.Database.MyBranch.Fleets[fleetnum].Vehicles.Add(new DataClasses.ElectricVehicle(numberplate, model, brand, false));
					return ErrorCode.Success;
				default:
					return ErrorCode.WrongArgument;
			}
		}

		internal ErrorCode AddCustomer(string name, string residence)
		{
			DatabaseController.Database.Customers.Add(new DataClasses.Customer(false)
			{
				Name = name,
				Residence = residence
			});
			return ErrorCode.Success;
		}

		internal ErrorCode AddBooking(string custID, string vehID, string startdate, string enddate)
		{
			DateTime start, end;
			int cid, vid;
			if (!DateTime.TryParseExact(startdate, "yyyyMMdd", null, DateTimeStyles.None, out start) || !DateTime.TryParseExact(enddate, "yyyyMMdd", null, DateTimeStyles.None, out end) || !int.TryParse(custID, out cid) || !int.TryParse(vehID, out vid))
			{
				return ErrorCode.WrongArgument;
			}
			DatabaseController.Database.Customers.Find(c => c.CustomerID == cid).Bookings.Add(new Booking(false){
				VehicleID = vid,
				Startdate = start,
				Enddate = end
			});
			return ErrorCode.Success;
		}
	}
}
