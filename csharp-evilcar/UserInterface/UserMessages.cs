using System;

namespace CsharpEvilcar.UserInterface
{
	internal static class OutputStrings
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

		public const string RemindHelp = "If you want more information what parameter are possibly at this point write ?\n";
		internal static class MainLevel
		{
			public const string ProgrammBegin = "";
			public const string ProgrammEnd = "\nSee you soon!\nYour EPT-EvilProgrammingTeam\n";
			public const string CommandTooShort = "not enough parameters have been inserted\n";
			public const string CommandTooLong =  "too much parameters have been inserted\n\n";

			public const string CommandNotExisting = "command doesn't exist, please use '?' for help";
			public const string CommandAbort = "your inserted command was not executet.\n";
			public const string Help =
				# region 
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
		internal static class Add
		{
			public const string AskForSelection = "do you want to add a 'vehicle' or a 'costumer': ";
			internal const string Help =
				"help for 'add'\n" +
				"you can use 'add vehicle' or 'add customer'\n";

			internal static class Vehicle
			{
				public const string AskForVehicleParameters = "Please enter now the parameters of the new vehicle in the following format:  brand model class fleetNr for example: S-XY-4589 audi q5 electric 3\n>>> ";
				public const string Help =
					"help for 'add vehicle'\n" +
					"full command: add vehicle [ numberplate brand model class fleetNr]\n" +
					"In brand, model and class none space allowed\n" +
					"If you only use 'add vehicle' you will be ask for more information about the vehicle.\n" +
					"If you want to do this at ones use the full command\n";
				public const string Error = "Your input wasn't correct\n" + OutputStrings.RemindHelp;
			}
			internal static class Customer
			{
				public const string AskForCustomerParameters = "Please enter your Lastname Firstname: ";
				public const string Help =
					"help for 'add customer'\n" +
					"full command: add customer [ Lastname Firstname]\n" +
					"In Lastname and Firstname none space allowed.\n"+
					"If you only use 'add customer' you will be ask for more information about the coustomer.\n"+
					"If you want to do this at ones use the full command\n";
				public const string Error = "Your input wasn't correct\n" + OutputStrings.RemindHelp;
			}
		}

		

		// OLD

		public static string Prompt => "> ";
		public static string WelcomeUsername => "Welcome to EvilCar!\nlogin as: ";

		public static string PasswordQuestion => "password: ";

		public static string OfferHelp => "Enter your commands (type 'help' for all possible commands)\n";

		
		
		
		public static string Numberplate => "Please enter the numberplate of the new vehicle for example: S-XY 4589\n>>> ";

		public static string VehicleParameters => "Please enter now the other parameters in the following format: brand model class fleet for example: audi q5 electric 3\n>>> ";

		public static string AddCustomer => "Please enter the new Customer in the following format:\nLastName PreName \n>>> ";
	}

	internal static class Errors
	{

		public static string TooLong => "your command is too long!";

		public static string Combine => "your first command cannot be combined with your second!\n>>";
	}
}
