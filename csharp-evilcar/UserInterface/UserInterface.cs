using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		internal static void Main()
		{
			// print programm begin info
			Console.Write(Output.Main.EvilCarLogo);
			Console.Write(Output.Main.ProgrammBegin);

			// login and run the prompt
			if (Login())
			{
				Console.Write(Output.Login.Successful);
				if (Database.DatabaseController.LoadDatabase() == ErrorCode.Success)
				{
					Prompt();
				}
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

					string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
					parameters = parameters.Skip(1).ToArray();

					// serach and select main case
					IEnumerable<Output.MainCase> cases = from s in Output.MainCases
														 where s.CaseName == selection
														 select s;

					Output.MainCase test = Output.MainCases.Where(Case => Case.CaseName == selection).Single();
					Console.WriteLine(test.CaseName);
					Console.WriteLine(Output.MainCases.First().CaseName);
					switch (selection)
					{
						case string s when cases.Count() == 1:
							MainCase(cases.Single(), parameters);
							continue;
						case "?":
						case "help":
							Console.Write(Output.Main.Help);
							continue;
						case "syntax":
							bool first = true;
							foreach (Output.MainCase Case in Output.MainCases)
							{
								Console.Write(Case.Syntax(first));
								first = false;

							}
							break;
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
