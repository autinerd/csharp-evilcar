using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			Console.Write(OutputStrings.Login.AskForUsername);
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
			string passwordPrompt = "Password: ";
			Console.Write(passwordPrompt);
			while (true)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
				{
					Console.WriteLine();
					break;
				}
				else if (key.Key == ConsoleKey.Backspace)
				{
					if (password.Length > 0) { Console.Write("\b \b"); }
					password = password.Length == 0 ? "" : password.Remove(password.Length - 1, 1);
				}
				else
				{
					Console.Write("*");
					password += key.KeyChar;
				}
			}

			return Database.DatabaseController.CheckUserCredentials(username, password);
		}
	}
}
