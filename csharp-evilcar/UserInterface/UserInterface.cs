using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		internal static void Main(string[] args)
		{
			// print programm begin info
			Console.Write(OutputStrings.EvilCarLogo);
			Console.Write(OutputStrings.MainLevel.ProgrammBegin);

			// login and run the prompt
			if (Login())
			{
				ErrorCode loaded = Database.DatabaseController.LoadDatabase();
				Prompt();
			}
			else { Console.WriteLine(OutputStrings.Login.Failed); }

			// close the programm
			Console.Write(OutputStrings.MainLevel.ProgrammEnd);
			Console.ReadKey();
		}


		private static void Prompt()
		{
			while (true)
			{
				// read in command and separate selection from parameters
				Console.Write(OutputStrings.Prompt);
				string[] parameters = GetInput();
				string selection = parameters[0].ToLower();
				parameters = parameters.Skip(1).ToArray();

			
				// execute command
				switch (selection)
				{
					case "add":
						AddCase(parameters);
						break;

					case "edit":
						EditCase(parameters);
						break;

					case "remove":
						RemoveCase(parameters);
						#region
						#endregion
						break;

					case "rebook":
						RebookCase(parameters);
						break;

					case "view":
						ViewCase(parameters);
						break;

					case "?":
					case "help":
						break;
					case "logout":
					case "exit":
						return;

					default:
						Console.WriteLine(Errors.Existence);
						continue;
				}
			}
		}
	}
}