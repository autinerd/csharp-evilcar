using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		// interface
		internal abstract class MainCase : HelpErrorCase
		{
			public static string SyntaxHead => " MainCase\tSubCase\t\tParameter 1\tParameter 2\t\tParameter 3\tParameter 4\t\tParameter 5\n";
			public virtual string AskForSelection { get; }
			public virtual string CaseName { get; }
			public override string Error => Output.Error.Combine;
			public virtual IEnumerable<SubCase> SubCases { get; }
			public virtual string Syntax(bool withHead) => string.Join("",
				withHead
				? SyntaxHead
				: "",
				( from c in SubCases
				  select c.Syntax ).Aggregate((str, item) => str + item));
		}
	}
	internal static partial class UserInterface
	{
		private static (ErrorCode, Output.HelpErrorCase) MainCase(Output.MainCase Case, string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Console.Write(Case.AskForSelection);
				switch (GetInput(out parameters, 1, 1))
				{
					case ErrorCode.CommandTooShort:
					case ErrorCode.CommandTooLong:
						return (ErrorCode.CommandAbort, Case);
					default:
						break;
				}
			}

			string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
			parameters = parameters.Skip(1).ToArray();

			Output.SubCase subCase = ( from s in Case.SubCases
									   where s.CaseName == selection
									   select s ).SingleOrDefault();

			return subCase == default                                                   // default if no subcase was found
				? Output.Main.HelpStrings.Contains(selection)                           // help needed
				? ((ErrorCode, Output.HelpErrorCase))(ErrorCode.HelpNeeded, Case)       // no subcase as first argument, but help
				: ((ErrorCode, Output.HelpErrorCase))(ErrorCode.WrongArgument, Case)    // neither subcase nor help as first argument
				: ((ErrorCode, Output.HelpErrorCase))SubCase(subCase, parameters);      // subcase as first argument
		}
	}
}
