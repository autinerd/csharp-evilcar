using System.Collections.Generic;
using System.Linq;
using CsharpEvilcar.Prompt;
using CsharpEvilcar.DataClasses;
using static CsharpEvilcar.Database.DatabaseController;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		/// <summary>
		/// Views branch(es)
		/// </summary>
		/// <param name="parameters">(0): "all" | <see cref="int"/> branchNumber</param>
		/// <returns><see cref="ReturnValue.Undefined(CaseTyps.Base)"/></returns>
		internal static ReturnValue ViewBranch(IEnumerable<string> parameters)
		{
			InputOutput.Print("    ", "");

			return parameters.ElementAtOrDefault(0).AllOrNullOrEmpty()
				? InputOutput.Print(string.Join("\n", from b in DatabaseObject.Branches
													  select b.ToString(false)))
				: parameters.ElementAtOrDefault(0).IsValidIndex(DatabaseObject.Branches, out uint branchID)
					? InputOutput.Print(DatabaseObject.Branches.ElementAt(branchID).ToString(true))
					: ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
		}

		/// <summary>
		/// Views fleet(s)
		/// </summary>
		/// <param name="parameters">(0): "all" | <see cref="int"/> branchNumber, (1): "all" | <see cref="int"/> fleetNumber</param>
		/// <returns></returns>
		internal static ReturnValue ViewFleet(IEnumerable<string> parameters)
		{
			InputOutput.Print("    ", "");

			if (parameters.ElementAtOrDefault(0).AllOrNullOrEmpty())
			{
				return InputOutput.Print(string.Join("\n", from b in DatabaseObject.Branches
														   from f in b.Fleets
														   select f.ToString(false)));
			}
			else if (parameters.ElementAtOrDefault(0).IsValidIndex(DatabaseObject.Branches, out uint branchID))
			{
				Branch branch = DatabaseObject.Branches.ElementAt(branchID);
				return parameters.ElementAtOrDefault(1).AllOrNullOrEmpty()
					? InputOutput.Print(string.Join("\n", from f in branch.Fleets
														  select f.ToString(false)))
					: parameters.ElementAtOrDefault(1).IsValidIndex(branch.Fleets, out uint fleetID)
						? InputOutput.Print(branch.Fleets.ElementAt(fleetID).ToString(true))
						: ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 1);

			}
			return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
		}

		/// <summary>
		/// Views vehicle(s).
		/// </summary>
		/// <param name="parameters">(0): "all" | branchID | "single", (1): (if branchID: "all" | fleetID) | (if "single": vehicleID)</param>
		/// <returns></returns>
		internal static ReturnValue ViewVehicle(IEnumerable<string> parameters)
		{
			InputOutput.Print("    ", "");

			if (parameters.ElementAt(0) == "single")
			{
				return int.TryParse(parameters.ElementAt(1), out int vehicleID)
					? InputOutput.Print(( from b in DatabaseObject.Branches
										  from f in b.Fleets
										  from v in f.Vehicles
										  where v.VehicleID == vehicleID
										  select v.ToString() ).SingleOrDefault())
					: ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 1);
			}
			else if (parameters.ElementAt(0).AllOrNullOrEmpty())
			{
				return InputOutput.Print(string.Join("\n", from b in DatabaseObject.Branches
														   from f in b.Fleets
														   from v in f.Vehicles
														   select v.ToString()));
			}
			else if (parameters.ElementAt(0).IsValidIndex(DatabaseObject.Branches, out uint branchID))
			{
				Branch branch = DatabaseObject.Branches.ElementAt(branchID);
				if (parameters.ElementAt(1).AllOrNullOrEmpty())
				{
					return InputOutput.Print(string.Join("\n", from f in branch.Fleets
															   from v in f.Vehicles
															   select v.ToString()));
				}
				else if (parameters.ElementAtOrDefault(1).IsValidIndex(branch.Fleets, out uint fleetID))
				{
					return InputOutput.Print(string.Join("\n", from v in branch.Fleets.ElementAt(fleetID).Vehicles
															   select v.ToString()));
				}
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 1);
			}
			return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
		}

		/// <summary>
		/// View customer(s).
		/// </summary>
		/// <param name="parameters">(0): "all" | customerID</param>
		/// <returns></returns>
		internal static ReturnValue ViewCustomer(IEnumerable<string> parameters)
		{
			InputOutput.Print("    ", "");

			return parameters.ElementAtOrDefault(0).AllOrNullOrEmpty()
					? InputOutput.Print(string.Join("\n", from c in DatabaseObject.Customers
														  select c.ToString()))
					: int.TryParse(parameters.ElementAt(0), out int i) && DatabaseObject.Customers.Any(item => item.CustomerID == i)
						? InputOutput.Print(( from c in DatabaseObject.Customers
											  where c.CustomerID == i
											  select c.ToString(true) ).SingleOrDefault())
						: ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
		}

		/// <summary>
		/// View booking(s)
		/// </summary>
		/// <param name="parameters">(0): branchID | "vehicle" | "customer" | "booking", </param>
		/// <returns></returns>
		internal static ReturnValue ViewBooking(IEnumerable<string> parameters)
		{
			InputOutput.Print("    ", "");
			
			if (parameters.ElementAt(0).IsValidIndex(DatabaseObject.Branches, out uint branchID))
			{
				return InputOutput.Print(string.Join("\n", from c in DatabaseObject.Customers
														   from book in c.Bookings
														   where DatabaseObject.Branches.ElementAt(branchID).Fleets.Any((f) => f.Vehicles.Contains(book.Vehicle))
														   select book));
			}
			if (parameters.Count() == 1)
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
			}
			if (int.TryParse(parameters.ElementAt(1), out int ID))
			{
				switch (parameters.ElementAt(0))
				{
					case "vehicle":
						return InputOutput.Print(string.Join("\n", from c in DatabaseObject.Customers
																   from book in c.Bookings
																   where book.VehicleID == ID
																   select book));
					case "customer":
						return InputOutput.Print(string.Join("\n", ( from c in DatabaseObject.Customers
																	 from b in c.Bookings
																	 where c.CustomerID == ID
																	 select b.ToString(true) ).Append("Total price of bookings:\n" +
																	 ( from c in DatabaseObject.Customers
																	   from b in c.Bookings
																	   where c.CustomerID == ID
																	   select b.Price ).Sum() + " EUR")));
					case "single":
						return InputOutput.Print(( from c in DatabaseObject.Customers
												   from b in c.Bookings
												   where b.BookingID == ID
												   select b ).SingleOrDefault().ToString(true));
					default:
						return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 0);
				}
			}
			else
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, null, 1);
			}
		}

		internal static ReturnValue ViewPassword(IEnumerable<string> parameters) => InputOutput.Print(encoder.Encode(parameters.First()));
	}
}
