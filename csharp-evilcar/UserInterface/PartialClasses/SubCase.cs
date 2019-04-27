using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		internal abstract class SubCase : HelpErrorCase
		{
			public virtual string CaseName { get; }
			public virtual string AskForParameters { get; }
			public virtual string Syntax { get; }
			public virtual int MinParamterLength { get; }
			public virtual int MaxParamterLength { get; }
			public virtual Func<IEnumerable<string>, ErrorCode> ExecuteCommand { get; }
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
		private static (ErrorCode, Output.SubCase) SubCase(Output.SubCase LocalOutput, string[] parameters)
		{
			if (parameters.Count() == 0) // if sub case was called without any parameters, then ask for more information
			{
				Console.Write(LocalOutput.AskForParameters);
				_ = GetInput(out parameters, LocalOutput.MinParamterLength, LocalOutput.MaxParamterLength);
			}

			return CheckLength(parameters, LocalOutput.MinParamterLength, LocalOutput.MaxParamterLength) != ErrorCode.Success
				? !parameters.Any(s => Output.Main.HelpStrings.Contains(s))
				? (ErrorCode.CommandAbort, LocalOutput) // no success, no help
				: (ErrorCode.HelpNeeded, LocalOutput) // no success, but help
				: (LocalOutput.ExecuteCommand(parameters), LocalOutput); // success
		}

	}
}
