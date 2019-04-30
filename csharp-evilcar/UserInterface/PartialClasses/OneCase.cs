using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Prompt
	{
		internal class CaseTyps
		{
			internal abstract class Base
			{

				public string CaseName = null;
				public string AskForParameters = null;
				public string Help = null;
				public virtual string Syntax { get; set; }


				public string GetCaseName => CaseName ?? "CaseName-undefined";
				public string GetAskForParameters => AskForParameters ?? string.Concat(CaseName, "-AskFor-undefinde");
				public string GetHelp => Help ?? string.Concat(CaseName, "-help-undefinde");


				public virtual string GetSyntax => null;
				public virtual ReturnValue.Type CheckParameterLenght(string[] inputArray) => ReturnValue.Success();

				
				public virtual ReturnValue.Type Execute(ref string[] parameters)
				{
					if (parameters.Length == 0)
					{
						Print(AskForParameters);
						GetInput(out parameters);
						ReturnValue.Type code = CheckParameterLenght(parameters);
						if (code.IsWrongParameterLength | code.IsEmpty)
						{
							return code;
						}
					}
					return ReturnValue.Success();
				}

			}
			internal class Selection : Base
			{
				public IEnumerable<Base> SubCases = null;
				public override string Syntax =>  string.Join("\n", from c in SubCases select c.Syntax) + "\n" ;
				public override ReturnValue.Type CheckParameterLenght(string[] inputArray) => inputArray.Count() > 0 ? ReturnValue.Success() : ReturnValue.Empty();
				public override ReturnValue.Type Execute(ref string[] parameters)
				{
					ReturnValue.Type code = base.Execute(ref parameters);
					if (code.IsError)
					{
						return code;
					}
					string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
					parameters = parameters.Skip(1).ToArray();

					Base selectedCase = ( from s in SubCases
											 where s.CaseName == selection
											 select s ).SingleOrDefault();

					return selectedCase == default
						? General.HelpSymbol.Contains(selection) ? ReturnValue.HelpNeeded(this) : ReturnValue.WrongArgument(this)
						: selectedCase.Execute(ref parameters);
				}

			}
			internal class Command : Base
			{
				
				public int[] ParameterLength = null;
				public override ReturnValue.Type CheckParameterLenght(string[] inputArray) => ParameterLength.Contains(inputArray.Count()) ? ReturnValue.Success() : ReturnValue.WrongParameterLength();
				public Func<IEnumerable<string>, ReturnValue.Type> SubFunction = (parameters) =>  ReturnValue.Undefined();

				public override ReturnValue.Type Execute(ref string[] parameters)
				{
					ReturnValue.Type RV = base.Execute(ref parameters);
					return RV.IsError
						? RV
						: parameters.Any(s => General.HelpSymbol.Contains(s))
						   ? ReturnValue.HelpNeeded(this)
						   : CheckParameterLenght(parameters).IsPass
							  ? ReturnValue.CommandAbort(this)
							  : SubFunction(parameters);
				}

			}
			internal class Default : Base
			{
			};
			internal sealed class Logout : Default
			{
				public override ReturnValue.Type Execute(ref string[] parameters) => ReturnValue.RequestedLogout(this);
			}
			internal sealed class Main : Selection
			{
				string[] parameters = Array.Empty<string>();
				public bool Execute()
				{
					ReturnValue.Type code = Execute(ref parameters);
					if (code.IsSuccess)
					{
						Database.DatabaseController.SaveDatabase();
					}
					else if (code.IsEmpty)
					{
					}
					else if (code.IsHelpNeeded)
					{
						Print(code.Case.Help + "\n" + General.SyntaxHead + "\n" + code.Case.Syntax);
					}
					else if (code.IsRequestedLogout)
					{
						return false;
					}
					else if (code.IsError)
					{
						Print("Error");
						Print(code.GetText);
					}
					return true;
				}
			}
		}
	}
}


