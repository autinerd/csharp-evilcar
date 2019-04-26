using System;
using System.Collections.Generic;

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
			public const string CommandAbort = "your inserted command was not executet.\n";

		}

		// main cases
		internal static readonly _Add Add = new _Add();
		internal class _Add : MainCase
		{
			public override string CaseName => "add";
			public override IEnumerable<SubCase> SubCases => new List<SubCase> { Vehicle, Customer };
			public override string AskForSelection => "do you want to add a 'vehicle' or a 'costumer':\n>>>";
			public override string Help =>
				"help for 'add'\n" +
				"you can use 'add vehicle' or 'add customer'\n";


			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle: SubCase
			{
				public override string CaseName => "vehicle";
				public override string AskForParameters =>
					"Please enter now the parameters of the new vehicle in the following format:\n"+
					"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 electric 3\n>>> ";
				public override string Help =>
					"help for 'add vehicle'\n" +
					"full command: add vehicle [ numberplate brand model class fleetNr]\n" +
					"In brand, model and class none space allowed\n" +
					"If you only use 'add vehicle' you will be ask for more information about the vehicle.\n" +
					"If you want to do this at ones use the full command\n";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " add\t\tvehicle\t\t<numberplate>\t<brand>\t\t<model>\t\t<category>\t<fleet_ID>";
				public override int MinParamterLength => 5;
				public override int MaxParamterLength => 5;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}

			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SubCase
			{
				public override string CaseName => "customer";
				public override string AskForParameters => "Please enter your Name Residence:\n>>>";
				public override string Help =>
					"help for 'add customer'\n" +
					"full command: add customer [ Name Residence]\n" +
					"In Lastname and Firstname none space allowed.\n"+
					"If you only use 'add customer' you will be ask for more information about the coustomer.\n"+
					"If you want to do this at ones use the full command\n";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " add\t\tcustomer\t<name>\t\t<residence>>";
				public override int MinParamterLength => 2;
				public override int MaxParamterLength => 2;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
		}
		internal static readonly _Edit Edit = new _Edit();
		internal class _Edit : MainCase
		{
			public override string CaseName => "edit";
			public override IEnumerable<SubCase> SubCases => new List<SubCase> { Vehicle, Customer };
			public override string AskForSelection => "edit-AskForSelection\n";
			public override string Help => "edit-Help\n";

			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle : SubCase
			{
				public override string CaseName => "vehicle";
				public override string AskForParameters =>"edit-vehicle-AskForParameters\n";
				public override string Help => "edit-vehicle-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " edit\t\tvehicle\t\t<vehicle_ID>\t{numberplate|fleet_ID}\t<new_value>";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}

			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SubCase
			{
				public override string CaseName => "customer";
				public override string AskForParameters => "edit-customer-AskForParameters\n";
				public override string Help => "edit-customer-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " edit\t\tcustomer\t<customer_ID>\t{name|residence}\t<new_value>";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
		}
		internal static readonly _Delete Delete = new _Delete();
		internal class _Delete : MainCase
		{
			public override string CaseName => "delete";
			public override IEnumerable<SubCase> SubCases => new List<SubCase> { Vehicle, Customer };
			public override string AskForSelection => "delete-AskForSelection\n";
			public override string Help => "delete-Help\n";

			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle : SubCase
			{
				public override string CaseName => "vehicle";
				public override string AskForParameters => "delete-vehicle-AskForParameters\n";
				public override string Help => "delete-vehicle-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " delete\tvehicle\t<vehicle_ID>";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}

			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SubCase
			{
				public override string CaseName => "customer";
				public override string AskForParameters => "delete-customer-AskForParameters\n";
				public override string Help => "delete-customer-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " delete\tcustomer\t<customer_ID>";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}

		}
		internal static readonly _View View = new _View();
		internal class _View : MainCase
		{
			public override string CaseName => "view";
			public override IEnumerable<SubCase> SubCases => new List<SubCase> {Branch,Fleet, Vehicle, Customer ,Booking};
			public override string AskForSelection => "View-AskForSelection\n";
			public override string Help => "View-Help\n";

			internal static readonly _Branch Branch = new _Branch();
			internal class _Branch : SubCase
			{
				public override string CaseName => "branch";
				public override string AskForParameters => "View-Branch-AskForParameters\n";
				public override string Help => "View-Branch-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => "view		branch";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
			internal static readonly _Fleet Fleet = new _Fleet();
			internal class _Fleet : SubCase
			{
				public override string CaseName => "fleet";
				public override string AskForParameters => "View-Fleet-AskForParameters\n";
				public override string Help => "View-Fleet-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " view		fleet\n                     < branch_ID > ";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle : SubCase
			{
				public override string CaseName => "vehicle";
				public override string AskForParameters => "View-vehicle-AskForParameters\n";
				public override string Help => "View-vehicle-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => "view		vehicle\n						<branch_ID>		[<fleet_ID>]\n						single			<vehicle_ID>";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SubCase
			{
				public override string CaseName => "customer";
				public override string AskForParameters => "View-customer-AskForParameters\n";
				public override string Help => "View-customer-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " view		customer\n                      < customer_ID >";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}
			internal static readonly _Booking Booking = new _Booking();
			internal class _Booking : SubCase
			{
				public override string CaseName => "booking";
				public override string AskForParameters => "View-booking-AskForParameters\n";
				public override string Help => "View-booking-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " view		bookings\n						<branch_ID>     [< fleet_ID >]\n						vehicle < vehicle_ID >\n						customer < customer_ID >\n						booking < booking_ID > ";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}

		}
		internal static readonly _Booking Booking = new _Booking();
		internal class _Booking : MainCase
		{
			public override string CaseName => "booking";
			public override IEnumerable<SubCase> SubCases => new List<SubCase> { Rent, Return };
			public override string AskForSelection => "booking-AskForSelection\n";
			public override string Help => "booking-Help\n";

			internal static readonly _Rent Rent = new _Rent();
			internal class _Rent : SubCase
			{
				public override string CaseName => "rent";
				public override string AskForParameters => "booking-rent-AskForParameters\n";
				public override string Help => "booking-rent-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => " booking	rent		<vehicle_ID>	<customer_ID>";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}

			internal static readonly _Return Return = new _Return();
			internal class _Return : SubCase
			{
				public override string CaseName => "return";
				public override string AskForParameters => "booking-return-AskForParameters\n";
				public override string Help => "booking-return-help";
				public override string Error => "Your input wasn't correct\n" + Output.Main.RemindHelp;
				public override string Syntax => "booking	return		<vehicle_ID>";
				public override int MinParamterLength => 0;
				public override int MaxParamterLength => -1;
				public override Func<IEnumerable<string>, ErrorCode> ExecuteCommand => UserInterface.DummyFunc;
			}

		}
		internal static IEnumerable<MainCase> MainCases = new List<MainCase> { Add, Edit, Delete, View, Booking };
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
