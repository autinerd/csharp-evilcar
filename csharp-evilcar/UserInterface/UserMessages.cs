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
		internal static class MainLevel
		{
			public const string ProgrammBegin = "";
			public const string ProgrammEnd = "\nSee you soon!\nYour EPT-EvilProgrammingTeam\n";
			public const string CommandTooShort = "To Short\n";
			public const string CommandTooLong = "To Long\n";
		}
		internal static class Login
		{
			public const string AskForUsername = "please enter username: ";
			public const string Failed = "Login failed!";
		}
		internal static class Add
		{
			internal static class Vehicle
			{
				public const string AskForNumberplate = "Please enter the numberplate of the new vehicle for example: S-XY 4589\n>>> ";
				public const string AskForVehicleParameters = "Please enter now the other parameters in the following format: brand model class fleetNr for example: audi q5 electric 3\n>>> ";
				public const string Help =
					"help for 'add vehicle'\n" +
					"full comand: add vehicle [ numberplate brand model class fleetNr]\n" +
					"if you only use 'add vehicle' you will be ask for more information about the car." +
					"if you want to do this at ones use the full command\n";
				public const string Error = "Your input wasn't correct\n" + OutputStrings.RemindHelp;
			}
			internal const string Help =
				"helb for 'add'\n" +
				"you can use 'add vehicle' or 'add customer'\n";
		}

		

		// OLD

		public const string RemindHelp = "If you want more information what parameter as possibly at this point write ?\n";
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

		public static string Existence => "command doesn't exist, please key in 'help' to see all possible commands";
	}
}