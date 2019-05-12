using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace CsharpEvilcar.Prompt
{
	internal static class InputOutput
	{
		internal static string[] GetInput(string message = null)
		{
			if (message != null)
			{
				Print(message);
			}
			Print("", end: "", withPrompt: true);
			return ( from Match m in Regex.Matches(Console.ReadLine(), @"("".*""|[\S]+)+") // extracts all parameters, single words and quoted ones
					  let s = m.Value
					  select s.Replace("\"", "") ).ToArray();

		}

		/// <summary>
		/// Prints to console
		/// </summary>
		/// <param name="str">The string to be printed</param>
		/// <param name="end">When nothing is given, end with newline</param>
		/// <param name="returns">when nothing is given, retuen Success</param>
		/// <param name="withPrompt">if the ">>>" is printed</param>
		/// <returns>Success or <paramref name="returns"/></returns>
		internal static ReturnValue Print([Optional] string str, [Optional] string end, [Optional] ReturnValue returns, [Optional] bool withPrompt)
		{
			if (str != null)
			{
				Console.Write(( withPrompt ? ">>> " : "    " ) +
					Regex.Replace(str, @"\n", "\n    ") +
					( end ?? "\n" ));
			}
			return returns ?? ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
		}

		internal static bool Login((string, string) userpass = default)
		{
			if (userpass.Item1 != null && userpass.Item2 != null)
			{
				return Database.DatabaseController.CheckUserCredentials(userpass.Item1, userpass.Item2);
			}
			_ = Print("");
			_ = Print(UserMessages.Login.AskForUsername, "");
			return InputAndCheckPassword(Console.ReadLine()); // read username and go on with password query and password check
		}

		private static bool InputAndCheckPassword(string username)
		{
			string password = ""; // empty password, will filled with the password
			_ = Print("Password: ", "");
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
