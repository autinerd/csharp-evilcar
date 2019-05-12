using CsharpEvilcar.Prompt;
using System;
using System.Collections.Generic;
using System.Linq;
using static CsharpEvilcar.CaseTypeFlags;
using static CsharpEvilcar.InternalLogic;
using static CsharpEvilcar.Prompt.ErrorCodeFlags;
using static CsharpEvilcar.Prompt.InputOutput;

namespace CsharpEvilcar
{
	internal class CaseDescriptor
	{
		internal CaseTypeFlags Flags { get; set; }
		internal string Help { get; set; }
		internal string AskForParameters { get; set; }
		internal string Syntax { get; set; }
		internal (uint, uint) ParameterLength { get; set; }
		internal Func<IEnumerable<string>, ReturnValue> SubFunction { get; set; }

		public override string ToString() => $"Case {Flags.ToString().Replace(",", "").ToLower()}";

		internal static ReturnValue Execute()
		{
			string[] parameters = GetInput();

			// when empty input, just do nothing
			if (parameters.Count() == 0)
			{
				return ReturnValue.GetValue(ErrorCodeFlags.None);
			}

			CaseTypeFlags currentFlags = CaseTypeFlags.None;

			// iterate through all parameters
			for (int i = 0; i < parameters.Count(); i++)
			{
				string param = parameters.ElementAt(i);
				// check if we have a '?' or 'help'
				if (UserMessages.General.HelpSymbols.Contains(param))
				{
					return ReturnValue.GetValue(IsHelpNeeded, currentFlags.ToDescriptor());
				}

				// check if we recognize the command name
				if (Enum.TryParse(param, true, out CaseTypeFlags flag))
				{
					currentFlags |= flag;
					CaseDescriptor command = currentFlags.ToDescriptor();

					// command not found
					if (command == default)
					{
						return ReturnValue.GetValue(IsCommandFunctionUndefined);
					}

					// if user specified no parameters, ask for them
					if (parameters.Skip(i + 1).Count() == 0 && command.ParameterLength.Item1 > 0)
					{
						string[] newparams = GetInput(command.AskForParameters);
						if (command.SubFunction is null)
						{
							parameters = parameters.Concat(newparams).ToArray();
							continue;
						}
						return command.SubFunction(newparams) + command;
					}
					// user wrote '?' or 'help' somewhere
					else if (parameters.Skip(i + 1).Any((item) => UserMessages.General.HelpSymbols.Contains(item)))
					{
						return ReturnValue.GetValue(IsHelpNeeded, currentFlags.ToDescriptor());
					}
					// amount of parameters doesn't match
					else if (!command.ParameterLength.NumberIsBetween((uint)parameters.Skip(i + 1).Count()))
					{
						return ReturnValue.GetValue(IsWrongParameterLength);
					}

					// command not fully recognized (just 'add' in 'add customer ...'), go to next parameter
					if (command.SubFunction is null)
					{
						continue;
					}
					// command recognized, run function with parameters
					else
					{
						return command.SubFunction(parameters.Skip(i + 1)) + command;
					}
				}
				return ReturnValue.GetValue(IsCommandFunctionUndefined);
			}
			return ReturnValue.GetValue(IsCommandFunctionUndefined);
		}

	}


	internal static class Cases
	{
		internal static CaseDescriptor[] CaseList => new CaseDescriptor[]
		{
			new CaseDescriptor
			{
				Flags = CaseTypeFlags.None
			},
			new CaseDescriptor
			{
				Help = "Use this if you want to add a vehicle or a customer to the database.",
				AskForParameters = "Do you want to add a 'vehicle' or a 'costumer':",
				Flags = Add,
				Syntax = "add",
				ParameterLength = (1, int.MaxValue)
			},
			new CaseDescriptor
			{
				Flags = Add | Vehicle,
				AskForParameters =
					"Please enter now the parameters of the new vehicle in the following format:\n" +
					"<numberplate> <brand> <model> <category> <fleetNr> for example: S-XY-4589 audi q5 large 3",
				Help = "Use this if you want to add a new vehicle.",
				Syntax = "vehicle [<numberplate> <brand> <model> <category> <fleet_ID>]",
				ParameterLength = (5, 5),
				SubFunction = AddVehicle
			},
			new CaseDescriptor
			{
				Flags = Add | Customer,
				AskForParameters = "Please enter your <name> and <residence>",
				Help = "Use this if you want to add a new customer.",
				Syntax = "customer [<name> <residence>]",
				ParameterLength = (2, 2),
				SubFunction = AddCustomer
			},
			new CaseDescriptor
			{
				AskForParameters = "Do you want to edit a 'vehicle' or a 'customer'?",
				Help = "Use this if you want to edit a vehicle or a customer.",
				Flags = Edit,
				Syntax = "edit",
				ParameterLength = (1, int.MaxValue)
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter the <vehicle_ID>  'numberplate' or 'fleet_ID' and <new_value>.",
				Help = "Use this if you want to edit a vehicle.",
				Syntax = "vehicle [<vehicle_ID> {numberplate|fleet_ID} <new_value>]",
				ParameterLength = (3, 3),
				Flags = Edit | Vehicle,
				SubFunction = EditVehicle
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter the <customer_Id> 'name' or 'residence' and <new_value>.",
				Help = "Use this if you want to edit a customer.",
				Syntax = "customer [<customer_ID> {name|residence} <new_value>]",
				ParameterLength = (3, 3),
				SubFunction = EditCustomer,
				Flags = Edit | Vehicle
			},
			new CaseDescriptor
			{
				Syntax = "delete",
				AskForParameters = "Do you want to delete a 'vehicle' or a 'customer'?",
				Help = "Use this if you want something to be deleted from the database",
				Flags = Delete,
				ParameterLength = (1, int.MaxValue)
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter the <vehicle_ID> of the vehicle you want do delete.",
				Help = "Use this command if you want to delete a vehicle from the database.",
				Syntax = "vehicle [<vehicle_ID>]",
				ParameterLength = (1, 1),
				SubFunction = DeleteVehicle,
				Flags = Delete | Vehicle
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter the <customer_id> of the customer you want to delete.",
				Help = "Use this command if you want to delete a customer from the database.",
				Syntax = "customer [<customer_ID>]",
				ParameterLength = (1, 1),
				SubFunction = DeleteCustomer,
				Flags = Delete | Customer
			},
			new CaseDescriptor
			{
				Syntax = "view",
				AskForParameters = "Please enter if you want to view 'branch', 'fleet', 'vehicle', 'customer' or 'bookings'.",
				Help = "Displays database data.",
				Flags = View,
				ParameterLength = (1, int.MaxValue)
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter 'all' if you want to see all or enter <banch_ID> if you only want so see one branch.",
				Help = "Displays one or all branches",
				Syntax = "branch [all | <branch_ID>]",
				ParameterLength = (1, 1),
				SubFunction = ViewBranch,
				Flags = View | Branch
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter nothing if you want to view all fleets or enter the <branch_ID> for which to see all fleets.",
				Help = "Displays one or multiple fleets.",
				Syntax = "fleet [all | <branch_ID> all | <branch_ID> <fleet_ID>]",
				ParameterLength = (1, 2),
				SubFunction = ViewFleet,
				Flags = View | Fleet
			},
			new CaseDescriptor
			{
				AskForParameters = "Plase enter <branch_ID> and optional <fleet_ID> if you want to view all vehicle of a branch or a fleet in a branch.\n"+
				"If you want to see a single car, please enter 'single' and than the <vehicle_ID>.",
				Help = "Displays one or multiple vehicles.",
				Syntax = "vehicle [all | single <vehicle_ID> | <branch_ID> <fleet_ID> | <branch_ID> all]",
				ParameterLength = (1, 2),
				SubFunction= ViewVehicle,
				Flags = View | Vehicle
			},
			new CaseDescriptor
			{
				Flags = View | Customer,
				AskForParameters = "Please enter the <customer_ID> of the customer you want to see or 'all' for all customers.",
				Help = "Displays one or all customers.",
				Syntax = "customer [all | <customer_ID>]",
				SubFunction = ViewCustomer,
				ParameterLength = (1, 1)
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter the <branch_ID> or one of ('fleet', 'customer') with their respective IDs or 'single' with the <booking_ID>",
				Help = "Displays one or multiple bookings.",
				Syntax = "booking [<branch_ID> | fleet <fleet_ID> | customer <customer_ID> | single <booking_ID>]",
				ParameterLength = (1, 2),
				SubFunction = ViewBooking,
				Flags = View | Booking
			},
			new CaseDescriptor
			{
				// just for set up our database
				AskForParameters = "Enter Password to hash",
				Help = "",
				ParameterLength = (1, 1),
				Syntax = "password <password>",
				SubFunction = ViewPassword,
				Flags = View | Password
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter the <vehicle_ID> and the <customer_ID> for a new booking.",
				Help = "Adds a new booking.",
				Syntax = "rent [<vehicle_ID> <customer_ID>]",
				ParameterLength = (1, 2),
				SubFunction = BookingRent,
				Flags = Rent
			},
			new CaseDescriptor
			{
				AskForParameters = "Please enter the <vehicle_ID> to return the vehicle and close the specific booking.",
				Help = "Returns a vehicle and closes the booking.",
				Syntax = "return [<vehicle_ID>]",
				ParameterLength = (1, 1),
				SubFunction = BookingReturn,
				Flags = Return
			},
			new CaseDescriptor
			{
				Flags = Logout,
				Syntax = "logout",
				SubFunction = LogoutFromProgram,
				ParameterLength = (0, 0)
			}
		};
	}
}



/*
 * MainCase	SubCase		Parameter 1		Parameter 2				Parameter 3		Parameter 4		Parameter 5
 * 
 * add		vehicle		<numberplate>	<brand>					<model>			<category>		<fleet_ID>
 * add		customer	<name>			<residence>
 *			
 * edit		vehicle		<vehicle_ID>	{numberplate|fleet_ID}	<new_value>
 * edit		customer	<customer_ID>	{name|residence}		<new_value>
 *			
 * delete	vehicle		<vehicle_ID>
 * delete	customer	<customer_ID>
 *			
 * view		branch
 * view		fleet
 *						<branch_ID>
 * view		vehicle
 *						<branch_ID>		[<fleet_ID>]
 *						single			<vehicle_ID>
 * view		customer
 *						<customer_ID>
 * view		bookings
 *						<branch_ID>		[<fleet_ID>]
 *						vehicle			<vehicle_ID>
 *						customer		<customer_ID>
 *						booking			<booking_ID>
 *
 * booking	rent		<vehicle_ID>	<customer_ID>
 * booking	return		<vehicle_ID>
 * 
 * logout
 */
