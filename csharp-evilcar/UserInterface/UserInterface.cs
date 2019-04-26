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
			Console.ReadKey();
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

					string selection = parameters[0].ToLower();
					parameters = parameters.Skip(1).ToArray();

					// serach and select main case
					IEnumerable<Output.MainCase> cases = from s in Output.MainCases
														 where s.CaseName == selection
														 select s;
					if (cases.Count() == 1)
					{	// if one main case was detected execute this main case
						MainCase(cases.Single(), parameters);
						continue;
					}
					else if (selection == "?" || selection == "help")
					{	//asked for help
						Console.Write(Output.Main.Help);
						Console.Write(Output.Add.Syntax);
						Console.Write(Output.Edit.Syntax);
						Console.Write(Output.Delete.Syntax);
						Console.Write(Output.Booking.Syntax);
						Console.Write(Output.View.Syntax);
						continue;
					}
					else if (selection == "")
					{	// skip empty new line
						continue;
					}
					else if (selection == "logout" || selection == "exit")
					{	// logout
						return;
					}
					else
					{	// error case
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
