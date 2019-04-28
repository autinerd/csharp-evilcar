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
			Console.Write(Output.General.EvilCarLogo);

			// login and run the prompt
			if (Login())
			{
				Print(Output.Login.Successful);
				Print(Output.General.RemindHelp);
				if (Database.DatabaseController.LoadDatabase() == ErrorCode.Success)
				{
					Prompt();
				}
			}
			else { Print(Output.Login.Failed); }

			// close the programm
			Print(Output.General.ProgrammEnd);
			Print("", "");
			_ = Console.ReadKey();
		}


		private static void Prompt()
		{
			while (true)
			{
				(ErrorCode code, Output.OneCase returncase) = OneCase(Output.Main, Array.Empty<string>());
				switch (code)
				{
					case ErrorCode.Success:
						_ = Database.DatabaseController.SaveDatabase();
						continue;
					case ErrorCode.WrongArgument:
						Print(returncase.Error);
						continue;
					case ErrorCode.CommandAbort:
						Print(returncase.Error);
						Print(Output.Error.CommandAbort);
						continue;
					case ErrorCode.HelpNeeded:
						Print(returncase.Help +"\n"+ Output.General.SyntaxHead + "\n" + returncase.GetSyntax);
						continue;
					case ErrorCode.RequestedLogout:
						break;
					default:
						continue;
				}
				break;
			}
		}
	}
}
