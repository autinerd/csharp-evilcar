using System;
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
				// read in command and separate selection from parameters
				Console.Write(Output.Prompt);

				switch (GetInput(out string[] parameters))
				{
					case ErrorCode.Success:
						break;
					case ErrorCode.CommandTooShort:
						Console.WriteLine(Output.Error.CommandTooShort);
						goto default;
					case ErrorCode.CommandTooLong:
						Console.WriteLine(Output.Error.CommandTooLong);
						goto default;
					case ErrorCode.HelpNeeded:
						break;
					default:
						Console.WriteLine(Output.Error.CommandAbort);
						break;
				}
				string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
				#warning @sidney hier gibt es bei mir nen Fehler wenn als Eingabe nur \n kamm
				parameters = parameters.Skip(1).ToArray();

				// search and select main case
				Output.FirstLevelCase maincase = ( from s in Output.MainCases
											 where s.CaseName == selection
											 select s ).SingleOrDefault();
				switch (selection)
				{
					case string s when maincase != default:
						(ErrorCode code, Output.HelpErrorCase returncase) = MainCase(maincase, parameters);
						switch (code)
						{
							case ErrorCode.Success:
								_ = Database.DatabaseController.SaveDatabase();
								break;
							case ErrorCode.WrongArgument:
								Console.Write(returncase.Error);
								break;
							case ErrorCode.CommandAbort:
								Console.Write(returncase.Error);
								Console.WriteLine(Output.Error.CommandAbort);
								break;
							case ErrorCode.HelpNeeded:
								Console.Write(returncase.Help);
								break;
							default:
								break;
						}
						continue;
					case "?":
					case "help":
						continue;
					case "syntax":
						Console.Write(Output.Main.Help);
						Console.Write(Output.Add.Syntax(true));
						Console.Write(Output.Edit.Syntax(false));
						Console.Write(Output.Delete.Syntax(false));
						Console.Write(Output.Booking.Syntax(false));
						Console.Write(Output.View.Syntax(false));
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
		}
	}
}
