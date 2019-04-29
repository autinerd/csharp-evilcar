using System;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{	
		/// <summary>
		/// execute the login prozess
		/// </summary>
		/// <returns>returns if the login was successful</returns>
		private static bool Login()
		{
			Prompt.Print(Prompt.Login.AskForUsername,"");
			return InputAndCheckPassword(Console.ReadLine()); // read username and go on with password query and password check
		}

		/// <summary>
		/// written by Sidney
		/// read and checks password
		/// </summary>
		/// <param name="username">the username you want to login</param>
		/// <returns>returns if the password was correct</returns>
		private static bool InputAndCheckPassword(string username)
		{
			string password = ""; // empty password, will filled with the password
			Prompt.Print("Password: ","");
			while (true)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.Enter: // input finished
						Console.WriteLine();
						return Database.DatabaseController.CheckUserCredentials(username, password);
					case ConsoleKey.Backspace:
						if (password.Length > 0) { Console.Write("\b \b"); }
						password = password.Length == 0
							? ""
							: password.Remove(password.Length - 1, 1);
						continue;
					default:
						Console.Write("*");
						password += key.KeyChar;
						continue;
				}
			}
		}
	}
}
