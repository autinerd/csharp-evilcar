﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		internal abstract class SubCase
		{
			public virtual string CaseName { get; }
			public virtual string AskForParameters { get; }
			public virtual string Help { get; }
			public virtual string Error { get; }
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
		private static void SubCase(Output.SubCase LocalOutput, IEnumerable<string> parameters)
		{
			if (parameters.Count() == 0) // if sub case was called without any parameters, then ask for more information
			{
				Console.Write(LocalOutput.AskForParameters);
				try { parameters = GetInput(LocalOutput.MinParamterLength, LocalOutput.MaxParamterLength); }
				catch (AbortCommandExecution) { }
			}

			if (CheckLength(parameters, LocalOutput.MinParamterLength, LocalOutput.MaxParamterLength)) // if parameteres number is correct than execute
			{ _ = LocalOutput.ExecuteCommand(parameters); }
			else
			{
				switch (parameters.Last()) // check if user asked for help
				{
					case "?":
					case "help":
						Console.Write(LocalOutput.Help);
						break;
					default:
						Console.Write(LocalOutput.Error);
						throw new AbortCommandExecution();
				}

			}

		}

	}
}
