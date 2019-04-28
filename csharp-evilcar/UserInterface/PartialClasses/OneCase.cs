using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		internal abstract class OneCase
		{
			public virtual string CaseName => "CaseName-undefined";
			public virtual string AskForParameters => string.Concat(CaseName, "-AskFor-undefinde");
			public virtual string Help => string.Concat(CaseName, "-help-undefinde");
			public virtual int MinParamterLength => 1;
			public virtual int MaxParamterLength => -1;
			public virtual string Error => string.Concat(CaseName, "-error-undefinde");
			public string GetSyntax => ( SubCases == null ) ? Syntax : (string.Join("\n", from c in SubCases select c.GetSyntax)+"\n");
			public virtual string Syntax => string.Concat(CaseName, "-syntax-undefinde");
			public virtual IEnumerable<OneCase> SubCases => null;
			public virtual Func<IEnumerable<string>, ErrorCode> ExecuteCommand
																=> (parameters) => { Console.Write(Output.Error.ExecuteCommandUndefined); return ErrorCode.Undefined; };
		}
	}

	internal static partial class UserInterface
	{
		private static (ErrorCode, Output.OneCase) OneCase(Output.OneCase Case, string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Print(Case.AskForParameters);
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

				Output.OneCase selectedCase = ( from s in Case.SubCases
												where s.CaseName == selection
												select s ).SingleOrDefault();
				return selectedCase == default
					? Output.General.HelpSymbol.Contains(selection) ? (ErrorCode.HelpNeeded, Case) : (ErrorCode.WrongArgument, Case)
					: OneCase(selectedCase, parameters);
			}
			else
			{
				return parameters.Any(s => Output.General.HelpSymbol.Contains(s))
					? (ErrorCode.HelpNeeded, Case)
					: CheckLength(parameters, Case.MinParamterLength, Case.MaxParamterLength) != ErrorCode.Success
					   ? (ErrorCode.CommandAbort, Case)
					   : (Case.ExecuteCommand(parameters), Case);

			}
		}
	}
}
