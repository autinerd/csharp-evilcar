using System;
using System.Collections.Generic;

#pragma warning disable IDE1006

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		// general
		internal static class Main
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

			internal const string RemindHelp = "If you want more information what parameter are possibly at this point write ?\n";
			public const string ProgrammBegin = "";
			public const string ProgrammEnd = "\nSee you soon!\nYour EPT-EvilProgrammingTeam\n";
			public static string[] HelpStrings => new string[] { "help", "?" };
			public const string Help =
			#region 
@"possible commands are:
add
edit
remove
rebook
view
exit | logout
? | help
"
			#endregion
				;
			public static string SyntaxHead => " MainCase\tSubCase\t\tParameter 1\tParameter 2\t\tParameter 3\tParameter 4\t\tParameter 5\n";
		}
		internal static class Login
		{
			public const string AskForUsername = "please enter username: ";
			public const string Successful = "You are now logged in.\nIf you need help with some command or the prompt as a whole please don't hasitate to use the '?' at any point.\n";
			public const string Failed = "Login failed!";
		}
		internal static class Error
		{
			public static string Combine => "your first command cannot be combined with your second!\n>>";
			public const string CommandTooShort = "not enough parameters have been inserted\n";
			public const string CommandTooLong = "too much parameters have been inserted\n\n";
			public const string CommandNotExisting = "command doesn't exist, please use '?' for help";
			public const string CommandAbort = "The command was not executed.";
			public const string InputIncorrect = "Your input was incorrect\n";
			public const string ExecuteCommandUndefined = "This Command doesn't do anything yet!\n";


		}

		// main cases
		internal static readonly _Add Add = new _Add();
		internal class _Add : FirstLevelCase
		{
			public override string CaseName => "add";
			public override IEnumerable<SecondLevelCase> SubCases => new List<SecondLevelCase> { Vehicle, Customer };
			public override string AskForParameters => "do you want to add a 'vehicle' or a 'costumer':\n>>>";
			public override string Help =>
				"help for 'add'\n" +
				"you can use 'add vehicle' or 'add customer'\n";


			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle : SecondLevelCase
			{
				public override string CaseName => "vehicle";
				public override string AskForParameters =>
					"Please enter now the parameters of the new vehicle in the following format:\n" +
					"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 electric 3\n>>> ";
				public override string Help =>
					"help for 'add vehicle'\n" +
					"full command: add vehicle [ numberplate brand model class fleetNr]\n" +
					"In brand, model and class none space allowed\n" +
					"If you only use 'add vehicle' you will be ask for more information about the vehicle.\n" +
					"If you want to do this at ones use the full command\n";
				public override string Syntax(bool withHead=false) => " add\t\tvehicle\t\t<numberplate>\t<brand>\t\t\t<model>\t\t<category>\t\t<fleet_ID>\n";
				public override int MinParamterLength => 5;
				public override int MaxParamterLength => 5;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.AddVehicle;
			}

			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SecondLevelCase
			{
				public override string CaseName => "customer";
				public override string AskForParameters => "Please enter your Name Residence:\n>>>";
				public override string Help =>
					"help for 'add customer'\n" +
					"full command: add customer [ Name Residence]\n" +
					"In Lastname and Firstname none space allowed.\n" +
					"If you only use 'add customer' you will be ask for more information about the coustomer.\n" +
					"If you want to do this at ones use the full command\n";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " add\t\tcustomer\t<name>\t\t<residence>\n";
				public override int MinParamterLength => 2;
				public override int MaxParamterLength => 2;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.AddCustomer;
			}
		}
		internal static readonly _Edit Edit = new _Edit();
		internal class _Edit : FirstLevelCase
		{
			public override string CaseName => "edit";
			public override IEnumerable<SecondLevelCase> SubCases => new List<SecondLevelCase> { Vehicle, Customer };
			public override string AskForParameters => "edit-AskForSelection\n";
			public override string Help => "edit-Help\n";

			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle : SecondLevelCase
			{
				public override string CaseName => "vehicle";
				public override string AskForParameters => "edit-vehicle-AskForParameters\n";
				public override string Help => "edit-vehicle-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " edit\t\tvehicle\t\t<vehicle_ID>\t{numberplate|fleet_ID}\t<new_value>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.EditVehicle;
			}

			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SecondLevelCase
			{
				public override string CaseName => "customer";
				public override string AskForParameters => "edit-customer-AskForParameters\n";
				public override string Help => "edit-customer-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " edit\t\tcustomer\t<customer_ID>\t{name|residence}\t<new_value>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.EditCustomer;
			}
		}
		internal static readonly _Delete Delete = new _Delete();
		internal class _Delete : FirstLevelCase
		{
			public override string CaseName => "delete";
			public override IEnumerable<SecondLevelCase> SubCases => new List<SecondLevelCase> { Vehicle, Customer };
			public override string AskForParameters => "delete-AskForSelection\n";
			public override string Help => "delete-Help\n";

			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle : SecondLevelCase
			{
				public override string CaseName => "vehicle";
				public override string AskForParameters => "delete-vehicle-AskForParameters\n";
				public override string Help => "delete-vehicle-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " delete\t\tvehicle\t\t<vehicle_ID>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.DeleteVehicle;
			}

			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SecondLevelCase
			{
				public override string CaseName => "customer";
				public override string AskForParameters => "delete-customer-AskForParameters\n";
				public override string Help => "delete-customer-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " delete\t\tcustomer\t<customer_ID>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.DeleteCustomer;
			}

		}
		internal static readonly _View View = new _View();
		internal class _View : FirstLevelCase
		{
			public override string CaseName => "view";
			public override IEnumerable<SecondLevelCase> SubCases => new List<SecondLevelCase> { Branch, Fleet, Vehicle, Customer, Booking };
			public override string AskForParameters => "View-AskForSelection\n";
			public override string Help => "View-Help\n";

			internal static readonly _Branch Branch = new _Branch();
			internal class _Branch : SecondLevelCase
			{
				public override string CaseName => "branch";
				public override string AskForParameters => "View-Branch-AskForParameters\n";
				public override string Help => "View-Branch-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " view\t\tbranch\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
			internal static readonly _Fleet Fleet = new _Fleet();
			internal class _Fleet : SecondLevelCase
			{
				public override string CaseName => "fleet";
				public override string AskForParameters => "View-Fleet-AskForParameters\n";
				public override string Help => "View-Fleet-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " view\t\tfleet\n\t\t\t\t<branch_ID>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle : SecondLevelCase
			{
				public override string CaseName => "vehicle";
				public override string AskForParameters => "View-vehicle-AskForParameters\n";
				public override string Help => "View-vehicle-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " view\t\tvehicle\n\t\t\t\t<branch_ID>\t[<fleet_ID>]\n\t\tsingle\t\t<vehicle_ID>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SecondLevelCase
			{
				public override string CaseName => "customer";
				public override string AskForParameters => "View-customer-AskForParameters\n";
				public override string Help => "View-customer-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " view\t\tcustomer\n\t\t\t\t<customer_ID>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
			internal static readonly _Booking Booking = new _Booking();
			internal class _Booking : SecondLevelCase
			{
				public override string CaseName => "booking";
				public override string AskForParameters => "View-booking-AskForParameters\n";
				public override string Help => "View-booking-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " view\t\tbookings\n\t\t\t\t<branch_ID>\t[< fleet_ID >]\n\t\tvehicle\t\t<vehicle_ID>\n\t\tcustomer\t<customer_ID>\n\t\tbooking\t\t<booking_ID>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}

		}
		internal static readonly _Booking Booking = new _Booking();
		internal class _Booking : FirstLevelCase
		{
			public override string CaseName => "booking";
			public override IEnumerable<SecondLevelCase> SubCases => new List<SecondLevelCase> { Rent, Return };
			public override string AskForParameters => "booking-AskForSelection\n";
			public override string Help => "booking-Help\n";

			internal static readonly _Rent Rent = new _Rent();
			internal class _Rent : SecondLevelCase
			{
				public override string CaseName => "rent";
				public override string AskForParameters => "booking-rent-AskForParameters\n";
				public override string Help => "booking-rent-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " booking\trent\t\t<vehicle_ID>\t<customer_ID>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.BookingRent;
			}

			internal static readonly _Return Return = new _Return();
			internal class _Return : SecondLevelCase
			{
				public override string CaseName => "return";
				public override string AskForParameters => "booking-return-AskForParameters\n";
				public override string Help => "booking-return-help";
				public override string Error => "Your input wasn't correct\n" + Main.RemindHelp;
				public override string Syntax(bool withHead=false) => " booking\treturn\t\t<vehicle_ID>\n";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => InternalLogic.BookingReturn;
			}

		}
		internal static IEnumerable<FirstLevelCase> MainCases = new List<FirstLevelCase> { Add, Edit, Delete, View, Booking };
		// OLD
		public static string Prompt => "> ";
		public static string WelcomeUsername => "Welcome to EvilCar!\nlogin as: ";
		public static string OfferHelp => "Enter your commands (type 'help' for all possible commands)\n";
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
