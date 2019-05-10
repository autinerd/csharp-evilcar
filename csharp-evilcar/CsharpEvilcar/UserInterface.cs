using CsharpEvilcar.Prompt;
using static CsharpEvilcar.Prompt.InputOutput;
using System;

namespace CsharpEvilcar
{
	internal static class UserInterface
	{
		public static void Main()
		{
			#if NET472
			ConsoleStuff.EnableQuickEdit();
			#endif
			// print programm begin info
			Print(UserMessages.General.Logo);
			// login and run the prompt
			if (Login())
			{
				Print(UserMessages.Login.Successful);
				Print(UserMessages.General.RemindHelp);
				ReturnValue code;
				if (( code = Database.DatabaseController.LoadDatabase() ) == ErrorCodeFlags.IsPass)
				{
					ReturnValue value;
					while (value = CaseDescriptor.Execute())
					{
						if (value == ErrorCodeFlags.IsHelpNeeded)
						{
							Print(value.Case2.Help);
						}
						else if (value == ErrorCodeFlags.IsError)
						{
							Print(value.Text);
						}
					}
					//Cases.Main.Init();
					//while (Cases.Main.Execute()) { };
				}
				else
				{
					Print(code.Text);
				}
			}
			else
			{
				Print(UserMessages.Login.Failed);
			}

			// close the programm
			Print(UserMessages.General.ProgrammEnd);
			Print("", "");
			Console.ReadKey();

		}
	}

}
