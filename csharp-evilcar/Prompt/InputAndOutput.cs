using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CsharpEvilcar.Prompt
{

	internal static class InputOutput
	{
		internal static ReturnValue.Typ GetInput(out string[] input)
		{
			Print("", "");
			input = ( from Match m in Regex.Matches(Console.ReadLine(), @"("".*""|[\S]+)+")
					  let s = m.Value
					  select s.Replace("\"", "") ).ToArray(); // extracts all parameters, single words and quoted ones
			return ReturnValue.Success();

		}
			   
		internal static ReturnValue.Typ Print(string str = "", string end = "\n")
		{
			if (str != null)
			{
				Console.Write(">>> " + Regex.Replace(str, @"\n", "\n    ") + end);
			}
			return ReturnValue.Success();
		}

		internal static bool Login()
		{
			Print(UserMessages.Login.AskForUsername, "");
			return InputAndCheckPassword(Console.ReadLine()); // read username and go on with password query and password check
		}

		private static bool InputAndCheckPassword(string username)
		{
			string password = ""; // empty password, will filled with the password
			Print("Password: ", "");
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
