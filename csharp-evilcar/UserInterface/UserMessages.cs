using System;
using System.Collections.Generic;

#pragma warning disable IDE1006

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		// general
		internal static class General
		{

			public const string EvilCarLogo =
			#region Logo
@"
	    ______      _ ________          
	   / ____/   __(_) / ____/___ ______
	  / __/ | | / / / / /   / __ `/ ___/
	 / /___ | |/ / / / /___/ /_/ / /    
	/_____/ |___/_/_/\____/\__,_/_/     

"
			#endregion Logo
			;

			internal const string RemindHelp = "If you need help with some command or the prompt as a whole please don't hasitate to use the '?' at any point.";
			public const string ProgrammEnd = "See you soon!\nYour EPT-EvilProgrammingTeam";
			public static string HelpSymbol => "?" ;
			public static string SyntaxHead => "Syntax\nMainCase\tSubCase\t\tParameter 1\tParameter 2\t\tParameter 3\tParameter 4\t\tParameter 5";
		}
		internal static class Login
		{
			public const string AskForUsername = "please enter username: ";
			public const string Successful = "You are now logged in.";
			public const string Failed = "Login failed!";
		}
		internal static class Error
		{
			public static string Combine => "your first command cannot be combined with your second.";
			public const string CommandTooShort = "not enough parameters have been inserted.";
			public const string CommandTooLong = "too much parameters have been inserted.";
			public const string CommandNotExisting = "command doesn't exist, please use '?' for help.";
			public const string CommandAbort = "The command was not executed.";
			public const string InputIncorrect = "Your input was incorrect";
			public const string ExecuteCommandUndefined = "This Command doesn't do anything yet!";


		}

		// cases

		internal static readonly _Main Main = new _Main();
		internal class _Main : OneCase
		{
			public override string CaseName => "main";
			public override string AskForParameters => null;
			internal static readonly _Add Add = new _Add();
			internal class _Add : OneCase
			{
				public override string CaseName => "add";
				public override IEnumerable<OneCase> SubCases => new List<OneCase> { Vehicle, Customer };
				public override string AskForParameters => "do you want to add a 'vehicle' or a 'costumer':";
				public override string Help =>
					"help for 'add'\n" +
					"you can use 'add vehicle' or 'add customer'";


				internal static readonly _Vehicle Vehicle = new _Vehicle();
				internal class _Vehicle : OneCase
				{
					public override string CaseName => "vehicle";
					public override string AskForParameters =>
						"Please enter now the parameters of the new vehicle in the following format:\n" +
						"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 electric 3";
					public override string Help =>
						"help for 'add vehicle'\n" +
						"full command: add vehicle [ numberplate brand model class fleetNr]\n" +
						"In brand, model and class none space allowed\n" +
						"If you only use 'add vehicle' you will be ask for more information about the vehicle.\n" +
						"If you want to do this at ones use the full command";
					public override string Syntax => "add\t\tvehicle\t\t<numberplate>\t<brand>\t\t\t<model>\t\t<category>\t\t<fleet_ID>";
					public override int MinParamterLength => 5;
					public override int MaxParamterLength => 5;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.AddVehicle;
				}

				internal static readonly _Customer Customer = new _Customer();
				internal class _Customer : OneCase
				{
					public override string CaseName => "customer";
					public override string AskForParameters => "Please enter your Name Residence:";
					public override string Help =>
						"help for 'add customer'\n" +
						"full command: add customer [ Name Residence]\n" +
						"In Lastname and Firstname none space allowed.\n" +
						"If you only use 'add customer' you will be ask for more information about the coustomer." +
						"If you want to do this at ones use the full command";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "add\t\tcustomer\t<name>\t\t<residence>";
					public override int MinParamterLength => 2;
					public override int MaxParamterLength => 2;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.AddCustomer;
				}
			}
			internal static readonly _Edit Edit = new _Edit();
			internal class _Edit : OneCase
			{
				public override string CaseName => "edit";
				public override IEnumerable<OneCase> SubCases => new List<OneCase> { Vehicle, Customer };
				public override string AskForParameters => "edit-AskForSelection";
				public override string Help => "edit-Help";

				internal static readonly _Vehicle Vehicle = new _Vehicle();
				internal class _Vehicle : OneCase
				{
					public override string CaseName => "vehicle";
					public override string AskForParameters => "edit-vehicle-AskForParameters";
					public override string Help => "edit-vehicle-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "edit\tvehicle\t\t<vehicle_ID>\t{numberplate|fleet_ID}\t<new_value>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.EditVehicle;
				}

				internal static readonly _Customer Customer = new _Customer();
				internal class _Customer : OneCase
				{
					public override string CaseName => "customer";
					public override string AskForParameters => "edit-customer-AskForParameters";
					public override string Help => "edit-customer-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "edit\tcustomer\t<customer_ID>\t{name|residence}\t<new_value>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.EditCustomer;
				}
			}
			internal static readonly _Delete Delete = new _Delete();
			internal class _Delete : OneCase
			{
				public override string CaseName => "delete";
				public override IEnumerable<OneCase> SubCases => new List<OneCase> { Vehicle, Customer };
				public override string AskForParameters => "delete-AskForSelection";
				public override string Help => "delete-Help";

				internal static readonly _Vehicle Vehicle = new _Vehicle();
				internal class _Vehicle : OneCase
				{
					public override string CaseName => "vehicle";
					public override string AskForParameters => "delete-vehicle-AskForParameters";
					public override string Help => "delete-vehicle-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "delete\tvehicle\t\t<vehicle_ID>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.DeleteVehicle;
				}

				internal static readonly _Customer Customer = new _Customer();
				internal class _Customer : OneCase
				{
					public override string CaseName => "customer";
					public override string AskForParameters => "delete-customer-AskForParameters";
					public override string Help => "delete-customer-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "delete\tcustomer\t<customer_ID>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.DeleteCustomer;
				}

			}
			internal static readonly _View View = new _View();
			internal class _View : OneCase
			{
				public override string CaseName => "view";
				public override IEnumerable<OneCase> SubCases => new List<OneCase> { Branch, Fleet, Vehicle, Customer, Booking };
				public override string AskForParameters => "View-AskForSelection";
				public override string Help => "View-Help";

				internal static readonly _Branch Branch = new _Branch();
				internal class _Branch : OneCase
				{
					public override string CaseName => "branch";
					public override string AskForParameters => "View-Branch-AskForParameters";
					public override string Help => "View-Branch-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "view\tbranch";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
				}
				internal static readonly _Fleet Fleet = new _Fleet();
				internal class _Fleet : OneCase
				{
					public override string CaseName => "fleet";
					public override string AskForParameters => "View-Fleet-AskForParameters";
					public override string Help => "View-Fleet-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "view\tfleet\n\t\t\t\t<branch_ID>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
				}
				internal static readonly _Vehicle Vehicle = new _Vehicle();
				internal class _Vehicle : OneCase
				{
					public override string CaseName => "vehicle";
					public override string AskForParameters => "View-vehicle-AskForParameters";
					public override string Help => "View-vehicle-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "view\tvehicle\n\t\t\t\t<branch_ID>\t[<fleet_ID>]\n\t\tsingle\t\t<vehicle_ID>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
				}
				internal static readonly _Customer Customer = new _Customer();
				internal class _Customer : OneCase
				{
					public override string CaseName => "customer";
					public override string AskForParameters => "View-customer-AskForParameters";
					public override string Help => "View-customer-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "view\tcustomer\n\t\t\t\t<customer_ID>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
				}
				internal static readonly _Booking Booking = new _Booking();
				internal class _Booking : OneCase
				{
					public override string CaseName => "booking";
					public override string AskForParameters => "View-booking-AskForParameters";
					public override string Help => "View-booking-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "view\tbookings\n\t\t\t\t<branch_ID>\t[< fleet_ID >]\n\t\tvehicle\t\t<vehicle_ID>\n\t\tcustomer\t<customer_ID>\n\t\tbooking\t\t<booking_ID>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
				}

			}
			internal static readonly _Booking Booking = new _Booking();
			internal class _Booking : OneCase
			{
				public override string CaseName => "booking";
				public override IEnumerable<OneCase> SubCases => new List<OneCase> { Rent, Return };
				public override string AskForParameters => "booking-AskForSelection";
				public override string Help => "booking-Help";

				internal static readonly _Rent Rent = new _Rent();
				internal class _Rent : OneCase
				{
					public override string CaseName => "rent";
					public override string AskForParameters => "booking-rent-AskForParameters";
					public override string Help => "booking-rent-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "booking\trent\t\t<vehicle_ID>\t<customer_ID>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.BookingRent;
				}

				internal static readonly _Return Return = new _Return();
				internal class _Return : OneCase
				{
					public override string CaseName => "return";
					public override string AskForParameters => "booking-return-AskForParameters";
					public override string Help => "booking-return-help";
					public override string Error => "Your input wasn't correct\n" + General.RemindHelp;
					public override string Syntax => "booking\treturn\t\t<vehicle_ID>";
					public override int MinParamterLength => 0;
					public override int MaxParamterLength => -1;
					public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.BookingReturn;
				}

			}
			internal static readonly _Logout Logout = new _Logout();
			internal class _Logout : OneCase
			{
				public override string CaseName => "logout";
				public override string AskForParameters => null;
				public override int MaxParamterLength => 0;
				public override int MinParamterLength => 0;
				public override string Syntax => "logout";
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand
																=> (parameters) => { return ErrorCode.RequestedLogout; };
			}
			public override IEnumerable<OneCase> SubCases => new List<OneCase> { Add, Edit, Delete, View, Booking, Logout };
		}
		// OLD
		public static string Prompt => "> ";
		public static string WelcomeUsername => "Welcome to EvilCar!\nlogin as: ";
		public static string OfferHelp => "Enter your commands (type 'help' for all possible commands)";
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
 */
