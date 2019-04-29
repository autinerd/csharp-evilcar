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
			internal abstract class _Default
			{

				public string CaseName = null;
				public string AskForParameters = null;
				public string Help = null;
				public int MinParamterLength = -1;
				public int MaxParamterLength = -1;
				public int[] ParameterLenght = null;
				public string Error = null;
				public string Syntax = null;
				public IEnumerable<Default> SubCases = null;
				public Func<IEnumerable<string>, ReturnValue.Type> SubFunction
																	 = (parameters) => { UserInterface.Print(Output.Error.SubFunctionUndefined); return ReturnValue.Undefined; };


				public string GetCaseName => CaseName ?? "CaseName-undefined";
				public string GetAskForParameters => AskForParameters ?? string.Concat(CaseName, "-AskFor-undefinde");
				public string GetHelp => Help ?? string.Concat(CaseName, "-help-undefinde");
				public string GetError => Error ?? string.Concat(CaseName, "-error-undefinde");
				public string GetSyntax => IsLastCase ? Syntax : ( string.Join("\n", from c in SubCases select c.GetSyntax) + "\n" );
				public int[] GetParameterLength => ParameterLenght ?? Array.Empty<int>();
				public int GetMinParamterLength => IsLastCase ? SubCases.Min(x => x.GetMinParamterLength) : MinParamterLength;
				public int GetMaxParamterLength => IsLastCase ? SubCases.Max(x => x.GetMaxParamterLength) : MaxParamterLength;

				private bool IsLastCase => SubCases == null;


				public ReturnValue.Type CheckParameterLenght(string[] inputArray) => IsLastCase
						? ParameterLenght.Contains(inputArray.Count()) ? ReturnValue.Success : ReturnValue.WrongParameterLength
						: inputArray.Count() > 0 ? ReturnValue.Success : ReturnValue.Empty;

				public ReturnValue.Type Execute(string[] parameters)
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

					if (SubCases != null)
					{
						string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
						parameters = parameters.Skip(1).ToArray();

						Default selectedCase = ( from s in SubCases
														where s.CaseName == selection
														select s ).SingleOrDefault();
						return selectedCase == default
							? Output.General.HelpSymbol.Contains(selection) ? ReturnValue.HelpNeeded : ReturnValue.WrongArgument
							: selectedCase.Execute(parameters);
					}
					else
					{
						return parameters.Any(s => Output.General.HelpSymbol.Contains(s))
							? ReturnValue.HelpNeeded
							: CheckParameterLenght(parameters) != ReturnValue.Success
							   ? ReturnValue.CommandAbort
							   : SubFunction(parameters);

					}
				}

			}
			internal class Default : _Default { };
			internal class Logout : Default
			{
				public new ReturnValue.Type Execute(string[] parameters) {
					Console.WriteLine("hier");
					return ReturnValue.RequestedLogout; }
			}
			internal class Main : Default
			{
				public ReturnValue.Type Execute() => Execute(Array.Empty<string>());
			}
		}
	}
}


