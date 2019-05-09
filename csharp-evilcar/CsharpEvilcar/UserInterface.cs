using CsharpEvilcar.Prompt;
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
			InputOutput.Print(UserMessages.General.Logo);
			// login and run the prompt
			if (InputOutput.Login())
			{
				InputOutput.Print(UserMessages.Login.Successful);
				InputOutput.Print(UserMessages.General.RemindHelp);
				ReturnValue code;
				if (( code = Database.DatabaseController.LoadDatabase() ) == ErrorCodeFlags.IsPass)
				{
					Cases.Main.Init();
					while (Cases.Main.Execute()) { };
				}
				else
				{
					InputOutput.Print(code.Text);
				}
			}
			else
			{
				InputOutput.Print(UserMessages.Login.Failed);
			}

			// close the programm
			InputOutput.Print(UserMessages.General.ProgrammEnd);
			InputOutput.Print("", "");
			Console.ReadKey();

		}
	}

}
