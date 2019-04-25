using System;

namespace CsharpEvilcar.UserInterface
{
	internal static class Strings
	{
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
		public static string LoginFailed => "Login failed!";

		public static string TooLong => "your command is too long!";

		public static string Combine => "your first command cannot be combined with your second!\n>>";

		public static string Existence => "command doesn't exist, please key in 'help' to see all possible commands";
	}
}