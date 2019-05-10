using System;
using System.Collections.Generic;
using System.Linq;
using CsharpEvilcar.Prompt;
using static CsharpEvilcar.CaseTypeFlags;
using static CsharpEvilcar.InternalLogic;
namespace CsharpEvilcar
{
    internal class CaseDescriptor
    {
        internal string CaseName;
        internal CaseTypeFlags Flags;
        internal CaseDescriptor[] SubCases;
        internal string Help;
        internal string AskForParameters;
        internal string Syntax;
        internal int[] ParameterLength;
        internal Func<IEnumerable<string>, Prompt.ReturnValue> SubFunction;

		internal static ReturnValue Execute()
		{
			(string[] parameters, _) = InputOutput.GetInput();
			CaseTypeFlags currentFlags = None;
			foreach ((string param, int i) in parameters.Select((item, i) => (item, i)))
			{
				// Console.WriteLine($"Param {i}: {param}");
				if (param == "?")
				{
					var a = ( from c in Cases.CaseList
							where c.Flags == currentFlags
							select c ).SingleOrDefault();
					return ReturnValue.GetValue(ErrorCodeFlags.IsHelpNeeded, a);
				}
				if (Enum.TryParse(param, true, out CaseTypeFlags flag))
				{
					currentFlags |= flag;
					var a = ( from c in Cases.CaseList
							where c.Flags == currentFlags
							select c ).SingleOrDefault();
					if (a == default(CaseDescriptor))
					{
						return ReturnValue.GetValue(ErrorCodeFlags.IsCommandFunctionUndefined);
					}
					if (a.SubFunction is null)
					{
						continue;
					}
					else
					{
						return a.SubFunction(parameters.Skip(i + 1));
					}
				}
				else
				{
					return ReturnValue.GetValue(ErrorCodeFlags.IsCommandFunctionUndefined);
				}
			}
			return ReturnValue.GetValue(ErrorCodeFlags.IsCommandFunctionUndefined);
		}

    }


    internal static class Cases
    {
        internal static CaseDescriptor[] CaseList = new CaseDescriptor[]
        {
            new CaseDescriptor
            {
                CaseName = "Main",
                Flags = None
            },
            new CaseDescriptor
            {
                CaseName = "add",
                Help = "Use this if you want to add a vehicle or a customer to the database.",
                AskForParameters = "Do you want to add a 'vehicle' or a 'costumer':",
                Flags = Add
            },
            new CaseDescriptor
            {
            	Flags = Add | Vehicle,
                CaseName = "vehicle",
                AskForParameters =
					"Please enter now the parameters of the new vehicle in the following format:\n" +
					"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 large 3",
				Help = "Use this if you want to add a new vehicle.",
				Syntax = "add\t\tvehicle\t\t<numberplate>\t<brand>\t\t\t<model>\t\t<category>\t\t<fleet_ID>",
				ParameterLength = new int[]{5},
				SubFunction = AddVehicle
            },
			new CaseDescriptor
			{
				Flags = Add | Customer,
				CaseName = "customer",
				AskForParameters = "Please enter your <name> and <residence>",
				Help = "Use this if you want to add a new customer.",
				Syntax = "add\t\tcustomer\t<name>\t\t<residence>",
				ParameterLength = new int[]{2},
				SubFunction = AddCustomer
			},
			new CaseDescriptor
			{
				CaseName = "edit",
				AskForParameters="Do you want to edit a 'vehicle' or a 'customer'?",
				Help="Use this if you want to edit a vehicle or a customer.",
				Flags = Edit
			},
			new CaseDescriptor
			{
				CaseName = "vehicle",
				AskForParameters="Please enter the <vehicle_ID>  'numberplate' or 'fleet_ID' and <new_value>.",
				Help="Use this if you want to edit a vehicle.",
				Syntax = "edit\tvehicle\t\t<vehicle_ID>\t{numberplate|fleet_ID}\t<new_value>",
				ParameterLength = new int[]{3},
				Flags = Edit | Vehicle,
				SubFunction = EditVehicle
			},
			new CaseDescriptor
			{
				CaseName = "customer",
				AskForParameters="Please enter the <customer_Id> 'name' or 'residence' and <new_value>.",
				Help="Use this if you want to edit a customer.",
				Syntax = "edit\tcustomer\t<customer_ID>\t{name|residence}\t<new_value>",
				ParameterLength = new int[]{3},
				SubFunction = EditCustomer,
				Flags = Edit | Vehicle
			},
			new CaseDescriptor
			{
				CaseName = "delete",
				AskForParameters = "Do you want to delete a 'vehicle' or a 'customer'?",
				Help="Use this if you want something to be deleted from the database",
				Flags = Delete
			},
			new CaseDescriptor
			{
				CaseName = "vehicle",
				AskForParameters="Please enter the <vehicle_ID> of the vehicle you want do delete.",
				Help="Use this command if you want to delete a vehicle from the database.",
                Syntax = "delete\tvehicle\t\t<vehicle_ID>",
				ParameterLength = new int[]{1},
				SubFunction = DeleteVehicle,
				Flags = Delete | Vehicle
			},
			new CaseDescriptor
			{
				CaseName = "customer",
				AskForParameters="Please enter the <customer_id> of the customer you want to delete.",
				Help="Use this command if you want to delete a customer from the database.",
				Syntax = "delete\tcustomer\t<customer_ID>",
				ParameterLength = new int[]{1},
				SubFunction = DeleteCustomer,
				Flags = Delete | Customer
			},
			new CaseDescriptor
			{
				CaseName = "view",
                AskForParameters="Please enter if you want to view 'branch', 'fleet', 'vehicle', 'customer' or 'bookings'.",
                Help="Use this if you want to view data from the database.",
				Flags = View
			},
			new CaseDescriptor
			{
				CaseName = "branch",
				AskForParameters="Please enter 'all' if you want to see all or enter <banch_ID> if you only want so see one branch.",
				Help="Use this command if you want to view all ",
				Syntax = "view\tbranch\t\tall\n\t\t\t\t<branch_ID>",
				ParameterLength = new int[]{1},
				SubFunction=ViewBranch,
				Flags = View | Branch
			},
			new CaseDescriptor
			{
				CaseName = "fleet",
				AskForParameters="Please enter nothing if you want to view all fleets or enter the <branch_ID> for which to see all fleets.",
				Help="Use this command if you want do view all all fleets or the fleets of one branch.",
				Syntax = "view\tfleet\t\tall\n\t\t\t\t<branch_ID>\tall\n\t\t\t\t<branch_ID>\t<fleet_ID>",
				ParameterLength = new int[]{1,2},
				SubFunction=ViewFleet,
				Flags = View | Fleet
			},
			new CaseDescriptor
			{
				CaseName = "vehicle",
				AskForParameters="Plase enter <branch_ID> and optional <fleet_ID> if you want to view all vehicle of a branch or a fleet in a branch.\n"+
				"If you want to see a single car, please enter 'single' and than the <vehicle_ID>.",
				Help="Use this command if you want to view one or many vehicles.",
				Syntax = "view\tvehicle\t\tall\n\t\t\t\tsingle\t\t<vehicle_ID>\n\t\t\t\t<branch_ID>\t<fleet_ID>\n\t\t\t\t<branch_ID>\tall",
				ParameterLength = new int[]{1,2},
				SubFunction= ViewVehicle,
				Flags = View | Vehicle
			},
			new CaseDescriptor
			{
				CaseName = "booking",
				AskForParameters="Fehlt noch.",
#warning view-booking AskForParameters falsche Übergabeparameter
				Help="",
#warning view-booking Help fehlt noch
				Syntax = "view\tbooking\n\t\t\t\t<branch_ID>\n\t\t\t\tfleet\t\t<fleet_ID>\n\t\t\t\tcustomer\t<customer_ID>\n\t\t\t\tsingle\t\t<booking_ID>",
				ParameterLength = new int[]{1,2},
				SubFunction = ViewBooking,
				Flags = View | Booking
			},
			new CaseDescriptor
			{
				// just for set up our database
				CaseName = "password",
				AskForParameters = "Password to hash",
				ParameterLength = new int[]{1},
				SubFunction = ViewPassword,
				Flags = View | Password
			},
			new CaseDescriptor
			{
				CaseName = "rent",
            	AskForParameters="Please enter the <vehicle_ID> and the <customer_ID> for a new booking.",
                Help="",
                Syntax = "rent\t\t<vehicle_ID>\t<customer_ID>",
                ParameterLength = new int[]{1,2},
                SubFunction = BookingRent,
				Flags = Rent
			},
			new CaseDescriptor
			{
				CaseName = "return",
				Syntax = "return\t\t<vehicle_ID>",
				ParameterLength = new int[]{1},
				SubFunction = BookingReturn,
				Flags = Return
			},
			new CaseDescriptor
			{
				CaseName = "logout",
				Flags = Logout,
				Syntax = "logout",
				SubFunction = LogoutFromProgram
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
