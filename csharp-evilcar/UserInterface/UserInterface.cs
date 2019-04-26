using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		internal static void Main(string[] args)
		{
			// print programm begin info
			Console.Write(Output.Main.EvilCarLogo);
			Console.Write(Output.Main.ProgrammBegin);

			// login and run the prompt
			if (Login())
			{
				Console.Write(Output.Login.Successful);
				ErrorCode loaded = Database.DatabaseController.LoadDatabase();
				Prompt();
			}
			else { Console.WriteLine(Output.Login.Failed); }

			// close the programm
			Console.Write(Output.Main.ProgrammEnd);
			_ = Console.ReadKey();
		}


		private static void Prompt()
		{
			while (true)
			{
				try
				{
					// read in command and separate selection from parameters
					Console.Write(Output.Prompt);
					string[] parameters = GetInput();
					parameters = parameters.Skip(1).ToArray();


					string selection = parameters[0].ToLower();
					// serach and select main case
					IEnumerable<Output.MainCase> cases = from s in Output.MainCases
														 where s.CaseName == selection
														 select s;
					switch (selection)
					{
						case string s when cases.Count() == 1:
							MainCase(cases.Single(), parameters);
							continue;
						case "?":
						case "help":
							Console.Write(string.Join("",
								Output.Main.Help,
								from m in Output.MainCases
								let s = m.Syntax
								select s));
							continue;
						case "":
							continue;
						case "logout":
						case "exit":
							return;
						default:
							Console.WriteLine(Output.Error.CommandNotExisting);
							continue;
					}
				}
				catch (AbortCommandExecution)
				{
					Console.Write(Output.Error.CommandAbort);
					continue;
				}
			}
		}
	}
}
