using CsharpEvilcar.Database;
using System.Linq;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Views branch(es)
		/// </summary>
		/// <param name="parameters">(0): "all" | <see cref="int"/> branchNumber</param>
		/// <returns><see cref="ReturnValue.Undefined(Prompt.CaseTyps.Base)"/></returns>
		internal static Prompt.ReturnValue.Typ ViewBranch(string[] parameters) => Prompt.InputOutput.Print(( parameters[0] == "all" )
				? string.Join("", from b in DatabaseController.Database.Branches select b.ToString(false))
				: int.TryParse(parameters[0], out int i) && DatabaseController.Database.Branches.Count() > i
				? DatabaseController.Database.Branches.ElementAt(i).ToString(true)
				: "");

		/// <summary>
		/// Views fleet(s)
		/// </summary>
		/// <param name="parameters">(0): "all" | <see cref="int"/> branchNumber, (1): "all" | <see cref="int"/> fleetNumber</param>
		/// <returns><see cref="ReturnValue.Undefined(Prompt.CaseTyps.Base)"/></returns>
		internal static Prompt.ReturnValue.Typ ViewFleet(string[] parameters) => parameters[0] == "all"
				? ( from b in DatabaseController.Database.Branches
					from f in b.Fleets
					select Prompt.InputOutput.Print(f.ToString(false)) ).First()
				: int.TryParse(parameters[1], out int i) && DatabaseController.Database.Branches.Count() > i
					? parameters[1] == "all"
						? ( from f in DatabaseController.Database.Branches.ElementAt(i).Fleets
							select Prompt.InputOutput.Print(f.ToString(false)) ).First()
						: int.TryParse(parameters[1], out int i2) && DatabaseController.Database.Branches.ElementAt(i).Fleets.Count() > i2
							? Prompt.InputOutput.Print(DatabaseController.Database.Branches.ElementAt(i).Fleets.ElementAt(i2).ToString(true))
							: Prompt.ReturnValue.CommandFunctionUndefined()
					: Prompt.ReturnValue.CommandFunctionUndefined();

		/// <summary>
		/// Views vehicle(s).
		/// </summary>
		/// <param name="parameters">(0): "all" | branchID | "single", (1): (if branchID: "all" | fleetID) | (if "single": vehicleID)</param>
		/// <returns></returns>
		internal static Prompt.ReturnValue.Typ ViewVehicle(string[] parameters) => parameters[0] == "single" && int.TryParse(parameters[1], out int vid)
				? Prompt.InputOutput.Print(( from b in DatabaseController.Database.Branches
								 from f in b.Fleets
								 from v in f.Vehicles
								 where v.VehicleID == vid
								 select v.ToString() ).SingleOrDefault())
				: parameters[0] == "all"
					? Prompt.InputOutput.Print(string.Join("", from b in DatabaseController.Database.Branches
												   from f in b.Fleets
												   from v in f.Vehicles
												   select v.ToString()))
					: int.TryParse(parameters[0], out int bid) && DatabaseController.Database.Branches.Count() > bid
									? parameters[1] == "all"
										? Prompt.InputOutput.Print(string.Join("", from f in DatabaseController.Database.Branches.ElementAt(bid).Fleets
																	   from v in f.Vehicles
																	   select v.ToString()))
										: int.TryParse(parameters[1], out int fid) && DatabaseController.Database.Branches.ElementAt(bid).Fleets.Count > fid
											? Prompt.InputOutput.Print(string.Join("", from v in DatabaseController.Database.Branches.ElementAt(bid).Fleets.ElementAt(fid).Vehicles
																		   select v.ToString()))
											: Prompt.ReturnValue.CommandFunctionUndefined()
									: Prompt.ReturnValue.CommandFunctionUndefined();

		/// <summary>
		/// View customer(s).
		/// </summary>
		/// <param name="parameters">(0): "all" | customerID</param>
		/// <returns></returns>
		internal static Prompt.ReturnValue.Typ ViewCustomer(string[] parameters) => parameters[0] == "all"
				? Prompt.InputOutput.Print(string.Join("", from c in DatabaseController.Database.Customers
											   select c.ToString()))
				: int.TryParse(parameters[0], out int i) && DatabaseController.Database.Customers.Any(item => item.CustomerID == i)
					? Prompt.InputOutput.Print(( from c in DatabaseController.Database.Customers
									 where c.CustomerID == i
									 select c.ToString(true) ).SingleOrDefault())
					: Prompt.ReturnValue.CommandFunctionUndefined();

		/// <summary>
		/// View booking(s)
		/// </summary>
		/// <param name="parameters">(0): branchID | "vehicle" | "customer" | "booking" </param>
		/// <returns></returns>
		internal static Prompt.ReturnValue.Typ ViewBooking(string[] parameters)
		{
			return Prompt.ReturnValue.CommandFunctionUndefined();
		}
	}
}
