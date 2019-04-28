using System;
using System.Collections.Generic;

using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		internal abstract class SecondLevelCase : HelpErrorCase
		{
			public virtual string CaseName => "undefined";
			public virtual string AskForParameters => "undefined";
			public virtual int MinParamterLength => 0;
			public virtual int MaxParamterLength => -1;
			public override string Error => Output.Error.Combine;
			public virtual string Syntax(bool withHead=false) => 
				(SubCases == null && ExecuteCommand == null) ? "undefined" :
				string.Join("", withHead ? Output.Main.SyntaxHead : "", ( from c in SubCases select c.Syntax() ).Aggregate((str, item) => str + item));
			public virtual IEnumerable<SecondLevelCase> SubCases => null;
			public virtual Func<IEnumerable<string>, ErrorCode> ExecuteCommand => (parameters) => { Console.Write(Output.Error.ExecuteCommandUndefined); return ErrorCode.Undefined; };
		}
	}
	internal static partial class UserInterface
	{
		/// <summary>
		/// Automatically execute a sub case.
		/// If enough parameters where passed the command will be execute immedialy.
		/// Else the user will be asked for more information
		/// </summary>
		/// <param name="LocalOutput">path to the output information for this subcase</param>
		/// <param name="parameters">parameters which were entered from the user</param>
		private static (ErrorCode, Output.SecondLevelCase) SubCase(Output.SecondLevelCase Case, string[] parameters)
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

			return CheckLength(parameters, Case.MinParamterLength, Case.MaxParamterLength) != ErrorCode.Success
				? !parameters.Any(s => Output.Main.HelpStrings.Contains(s))
				? (ErrorCode.CommandAbort, Case) // no success, no help
				: (ErrorCode.HelpNeeded, Case) // no success, but help
				: (Case.ExecuteCommand(parameters), Case); // success
		}

	}
}
