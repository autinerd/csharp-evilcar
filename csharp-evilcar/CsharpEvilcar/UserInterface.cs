using CsharpEvilcar.Prompt;
using static CsharpEvilcar.Prompt.InputOutput;
using System;
using System.Linq;

namespace CsharpEvilcar
{
	internal static class UserInterface
	{
		public static int Main(string[] args)
		{
			bool have_args = args.Count() > 2;
			#if NET472
			ConsoleStuff.EnableQuickEdit();
			#endif
			// print programm begin info
			if (!have_args)
			{
				Print(UserMessages.General.Logo);
			}
			// login and run the prompt
			if (Login(have_args ? (args[0], args[1]) : default))
			{
				if (!have_args)
				{
					Print(UserMessages.Login.Successful);
					Print(UserMessages.General.RemindHelp);
				}
				ReturnValue code;
				if (( code = Database.DatabaseController.LoadDatabase() ) == ErrorCodeFlags.IsPass)
				{
					while (code = CaseDescriptor.Execute(have_args ? args.Skip(2).ToArray() : null))
					{
						if (code == ErrorCodeFlags.IsHelpNeeded)
						{
							CaseDescriptor caseDescriptor = code.Case;
							Print(caseDescriptor.Help);
							System.Collections.Generic.IEnumerable<CaseDescriptor> c = from d in Cases.CaseList
									where d.Flags.HasFlag(caseDescriptor.Flags)
									select d;
							if (c.Count() > 1)
							{
								c = from item in c where item.Flags != caseDescriptor.Flags select item;
							}
							foreach (CaseDescriptor item in c)
							{
								CaseDescriptor tmp = item;
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
							if (code.Case != null)
							{
								Print("Error in case " + code.Case.Flags.ToString().ToLower().Replace(",", ""));
							}
							Print(code.Text + ( ( code == ErrorCodeFlags.IsWrongArgument && code.Options.Count() > 0 ) ? ( (int)code.Options.ElementAt(0) ).ToString() : "" ));
							if (have_args)
							{
								return (int)code.Flags;
							}
						}
						if (have_args)
						{
							break;
						}
					}
				}
				else
				{
					Print(code.Text);
				}
			}
			else
			{
				Print(UserMessages.Login.Failed);
				return 1;
			}

			// close the programm
			if (!have_args)
			{
				Print(UserMessages.General.ProgrammEnd);
				Print("", "");
				Console.ReadKey(true);
			}
			return 0;
		}
	}

}
