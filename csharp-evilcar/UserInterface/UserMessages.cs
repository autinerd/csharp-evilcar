using System;

namespace CsharpEvilcar.UserInterface
{
	internal static class OutputStrings
	{
		internal interface SubCase
		{
			string AskForParameters { get; }
			string Help { get; }
			string Error { get; }
		}

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
			internal const string AskForSelection = "do you want to add a 'vehicle' or a 'costumer':\n>>>";
			internal const string Help =
				"help for 'add'\n" +
				"you can use 'add vehicle' or 'add customer'\n";


			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle: SubCase
			{
				public string AskForParameters =>
					"Please enter now the parameters of the new vehicle in the following format:\n"+
					"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 electric 3\n>>> ";
				public string Help =>
					"help for 'add vehicle'\n" +
					"full command: add vehicle [ numberplate brand model class fleetNr]\n" +
					"In brand, model and class none space allowed\n" +
					"If you only use 'add vehicle' you will be ask for more information about the vehicle.\n" +
					"If you want to do this at ones use the full command\n";
				public string Error => "Your input wasn't correct\n" + OutputStrings.RemindHelp;
			}

			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SubCase
			{
				public string AskForParameters => "Please enter your Name Residence:\n>>>";
				public string Help =>
					"help for 'add customer'\n" +
					"full command: add customer [ Name Residence]\n" +
					"In Lastname and Firstname none space allowed.\n"+
					"If you only use 'add customer' you will be ask for more information about the coustomer.\n"+
					"If you want to do this at ones use the full command\n";
				public string Error => "Your input wasn't correct\n" + OutputStrings.RemindHelp;
			}
		}

		internal static class Edit
		{
			internal const string AskForSelection = "edit-AskForSelection\n";
			internal const string Help = "edit-Help\n";

			internal static readonly _Vehicle Vehicle = new _Vehicle();
			internal class _Vehicle : SubCase
			{
				public string AskForParameters =>"edit-vehicle-AskForParameters\n";
				public string Help => "edit-vehicle-help";
				public string Error => "Your input wasn't correct\n" + OutputStrings.RemindHelp;
			}

			internal static readonly _Customer Customer = new _Customer();
			internal class _Customer : SubCase
			{
				public string AskForParameters => "edit-customer-AskForParameters\n";
				public string Help => "edit-customer-help";
				public string Error => "Your input wasn't correct\n" + OutputStrings.RemindHelp;
			}
		}


		// OLD
		public static string Prompt => "> ";
		public static string WelcomeUsername => "Welcome to EvilCar!\nlogin as: ";
		public static string OfferHelp => "Enter your commands (type 'help' for all possible commands)\n";	
	}

	internal static class Errors
	{
		public static string Combine => "your first command cannot be combined with your second!\n>>";
	}
	
}
