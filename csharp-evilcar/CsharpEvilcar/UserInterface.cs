using System;

namespace CsharpEvilcar
{
	internal static class UserInterface
	{
		public static void Main()
		{
			// print programm begin info
			Prompt.InputOutput.Print(UserMessages.General.Logo);
			// login and run the prompt
			if (Prompt.InputOutput.Login())
			{
				Prompt.InputOutput.Print(UserMessages.Login.Successful);
				Prompt.InputOutput.Print(UserMessages.General.RemindHelp);
				if (Prompt.ReturnValue.Execute(out Prompt.ReturnValue.Typ code, Database.DatabaseController.LoadDatabase()).IsPass)
				{
					UserMessages.Main.Init();
					while (UserMessages.Main.Execute()) { };
				}
				else
				{
					Prompt.InputOutput.Print(code.Text);
				}
			}
			else { Prompt.InputOutput.Print(UserMessages.Login.Failed); }

			// close the programm
			Prompt.InputOutput.Print(UserMessages.General.ProgrammEnd);
			Prompt.InputOutput.Print("", "");
			_ = Console.ReadKey();

		}
	}
		
}
