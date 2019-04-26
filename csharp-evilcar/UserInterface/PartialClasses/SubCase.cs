using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{

	internal static partial class UserInterface
	{
		/// <summary>
		/// Automaticly execute a sub case.
		/// If enough parameters where passed the command will be execute immedialy.
		/// Else the user will be asked for more information
 		/// </summary>
		/// <param name="LocalOutput">path to the output strings for this sub case</param>
		/// <param name="ParametersLength">number of parameters required for this command</param>
		/// <param name="ExecuteCommand">coammnd function that will be execute with this command</param>
		/// <param name="parameters">parameteres who were entered by user</param>
		private static void SubCase(OutputStrings.SubCase LocalOutput,int ParametersLength,Func<IEnumerable<string>, ErrorCode> ExecuteCommand,IEnumerable<string> parameters)
		{

			if (parameters.Count() == 0) // if sub case was call without any parameters, than ask for more information
			{
				Console.Write(LocalOutput.AskForParameters);
				try { parameters = GetInput(ParametersLength, ParametersLength); }
				catch (AbortCommandExecution) { }
			}

			if (parameters.Count() == ParametersLength) // if parameteres number is correct than execut
			{ExecuteCommand(parameters);}
			else
			{
				if (parameters.Last() == "?" || parameters.Last() == "help") // check if user asked for help
				{ Console.Write(LocalOutput.Help); }
				else
				{
					Console.Write(LocalOutput.Error);
					throw new AbortCommandExecution();
				}

			}

		}

	}
}
