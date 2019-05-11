using CsharpEvilcar.Prompt;
using static CsharpEvilcar.Prompt.InputOutput;
using System;
using System.Linq;

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
					while (code = CaseDescriptor.Execute())
					{
						if (code == ErrorCodeFlags.IsHelpNeeded)
						{
							CaseDescriptor caseDescriptor = code.Case2;
							Print(caseDescriptor.Help);
							var c = from d in Cases.CaseList
									where d.Flags.HasFlag(caseDescriptor.Flags)
									select d;
							if (c.Count() > 1)
							{
								c = from item in c where item.Flags != caseDescriptor.Flags select item;
							}
							foreach (var item in c)
							{
								var tmp = item;
								string Syntax = "";
								while (tmp.Flags != CaseTypeFlags.None)
								{
									Syntax = tmp.Syntax + " " + Syntax;
									tmp = tmp.Flags.BaseTypeOf().ToDescriptor();
								}
								Print(Syntax);
							}
							
						}
						else if (code == ErrorCodeFlags.IsError)
						{
							Print(code.Text);
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
			Console.ReadKey(true);

		}
	}

}
