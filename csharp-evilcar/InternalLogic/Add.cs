using System.Linq;
using System.Text.RegularExpressions;
using CsharpEvilcar.Database;

namespace CsharpEvilcar
{
	partial class InternalLogic
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
		UserInterface.ErrorCode AddVehicle(string numberplate, string brand, string model, string category, int fleet)
		{
			if (!Regex.IsMatch(numberplate, "^[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}$"))
			{
				return UserInterface.ErrorCode.WrongArgument;
			}
			if (DatabaseController.Database.MyBranch.Fleets.Count() >= fleet)
			{
				return UserInterface.ErrorCode.WrongArgument;
			}
			switch (category.ToLower())
			{
				case "small":
					DatabaseController.Database.MyBranch.Fleets[fleet].Vehicles.Add(new DataClasses.SmallVehicle(numberplate, model, brand));
					return UserInterface.ErrorCode.Success;
				case "midsize":
					DatabaseController.Database.MyBranch.Fleets[fleet].Vehicles.Add(new DataClasses.MidsizeVehicle(numberplate, model, brand));
					return UserInterface.ErrorCode.Success;
				case "large":
					DatabaseController.Database.MyBranch.Fleets[fleet].Vehicles.Add(new DataClasses.LargeVehicle(numberplate, model, brand));
					return UserInterface.ErrorCode.Success;
				case "electric":
					DatabaseController.Database.MyBranch.Fleets[fleet].Vehicles.Add(new DataClasses.ElectricVehicle(numberplate, model, brand));
					return UserInterface.ErrorCode.Success;
				default:
					return UserInterface.ErrorCode.WrongArgument;
			}
		}
	}
}
