using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		internal abstract class OneCase : HelpErrorCase
		{
			public virtual string CaseName						=> "undefined";
			public virtual string AskForParameters				=> "undefined";
			public virtual int MinParamterLength				=> 1;
			public virtual int MaxParamterLength				=> 1;
			public override string Error						=> Output.Error.Combine;
			public virtual string Syntax(bool withHead = false)	=> (SubCases == null && ExecuteCommand == null ) ? "undefined" : string.Join("", withHead ? Output.Main.SyntaxHead : "", ( from c in SubCases select c.Syntax() ).Aggregate((str, item) => str + item));
			public virtual IEnumerable<OneCase> SubCases		=> null;
			public virtual Func<IEnumerable<string>, ErrorCode> ExecuteCommand 
																=> (parameters) => { Console.Write(Output.Error.ExecuteCommandUndefined); return ErrorCode.Undefined; };
		}
	}

	internal static partial class UserInterface
	{
		private static (ErrorCode, Output.HelpErrorCase) OneCase(Output.FirstLevelCase Case, string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Console.Write(Case.AskForParameters);
				switch (GetInput(out parameters, Case.MinParamterLength, Case.MaxParamterLength))
				{
					case ErrorCode.CommandTooShort:
					case ErrorCode.CommandTooLong:
						return (ErrorCode.CommandAbort, Case);
					default:
						break;
				}
			}
			if (Case.SubCases != null)
			{
				string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
				parameters = parameters.Skip(1).ToArray();

				Output.SecondLevelCase selectedCase = ( from s in Case.SubCases
												   where s.CaseName == selection
												   select s ).SingleOrDefault();

				return selectedCase == default                                              // default if no subcase was found
					? Output.Main.HelpStrings.Contains(selection)                           // help needed
					? ((ErrorCode, Output.HelpErrorCase))(ErrorCode.HelpNeeded, Case)       // no subcase as first argument, but help
					: ((ErrorCode, Output.HelpErrorCase))(ErrorCode.WrongArgument, Case)    // neither subcase nor help as first argument
					: ((ErrorCode, Output.HelpErrorCase))SubCase(selectedCase, parameters); // subcase as first argument
			}
			else
			{
				return CheckLength(parameters, Case.MinParamterLength, Case.MaxParamterLength) != ErrorCode.Success
				   ? !parameters.Any(s => Output.Main.HelpStrings.Contains(s))
				   ? (ErrorCode.CommandAbort, Case) // no success, no help
				   : (ErrorCode.HelpNeeded, Case) // no success, but help
				   : (Case.ExecuteCommand(parameters), Case); // success

			}
		}
	}
}
