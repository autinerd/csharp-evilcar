using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
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
				public virtual ReturnValue.Type CheckParameterLenght(string[] inputArray) => ReturnValue.Success;

				
				public virtual ReturnValue.Type Execute(ref string[] parameters)
				{
					ReturnValue.Case = this;
					if (parameters.Length == 0)
					{
						UserInterface.Print(AskForParameters);
						UserInterface.GetInput(out parameters);

						if (CheckParameterLenght(parameters) == ReturnValue.WrongParameterLength)
						{
							return ReturnValue.WrongParameterLength;
						}
						if (CheckParameterLenght(parameters) == ReturnValue.Empty)
						{
							return ReturnValue.Empty;
						}
					}
					return ReturnValue.Success;
				}

			}
			internal class Selection : Base
			{
				public IEnumerable<Base> SubCases = null;
				public override string Syntax =>  string.Join("\n", from c in SubCases select c.Syntax) + "\n" ;
				public override ReturnValue.Type CheckParameterLenght(string[] inputArray) => inputArray.Count() > 0 ? ReturnValue.Success : ReturnValue.Empty;
				public override ReturnValue.Type Execute(ref string[] parameters)
				{
					ReturnValue.Type RV = base.Execute(ref parameters);
					if (RV != ReturnValue.Success)
					{
						return RV;
					}
					string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
					parameters = parameters.Skip(1).ToArray();

					Base selectedCase = ( from s in SubCases
											 where s.CaseName == selection
											 select s ).SingleOrDefault();
					return selectedCase == default
						? Output.General.HelpSymbol.Contains(selection) ? ReturnValue.HelpNeeded : ReturnValue.WrongArgument
						: selectedCase.Execute(ref parameters);
				}

			}
			internal class Command : Base
			{
				
				public int[] ParameterLenght = null;
				public override ReturnValue.Type CheckParameterLenght(string[] inputArray) => ParameterLenght.Contains(inputArray.Count()) ? ReturnValue.Success : ReturnValue.WrongParameterLength;
				public Func<IEnumerable<string>, ReturnValue.Type> SubFunction
																	 = (parameters) => { UserInterface.Print(Output.Error.SubFunctionUndefined); return ReturnValue.Undefined; };

				public override ReturnValue.Type Execute(ref string[] parameters)
				{
					ReturnValue.Type RV = base.Execute(ref parameters);
					return RV != ReturnValue.Success
						? RV
						: parameters.Any(s => Output.General.HelpSymbol.Contains(s))
						   ? ReturnValue.HelpNeeded
						   : CheckParameterLenght(parameters) != ReturnValue.Success
							  ? ReturnValue.CommandAbort
							  : SubFunction(parameters);
				}

			}
			internal class Default : Base
			{
			};
			internal class Logout : Default
			{
				public override ReturnValue.Type Execute(ref string[] parameters) => ReturnValue.RequestedLogout;
			}
			internal class Main : Selection
			{
				public ReturnValue.Type Execute()
				{
					string[] parameters = Array.Empty<string>();
					return Execute(ref parameters);
				}
			}
		}
	}
}


