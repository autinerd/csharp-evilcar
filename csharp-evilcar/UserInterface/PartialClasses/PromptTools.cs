using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		private static string[] GetInput(int MinLength = 0, int Maxlength = -1)
		{
			string input_str = Console.ReadLine();
			input_str = Regex.Replace(input_str, @"(\A\s+)|(\s+\z)", ""); // removing spaces at the begin and end
			string[] input = Regex.Split(input_str, @"\s+"); // split by every occurence of one or more spaces
															 // Erkennung auf Hochkomma wenn Leerzeichen
			if (!CheckLength(input, MinLength, Maxlength))
			{ throw new AbortCommandExecution(); }
			return input;
		}

		private static bool CheckLength(string[] inputArray, int MinLength = 0, int MaxLength = -1)
		{
			if (MaxLength > 0 && inputArray.Length > MaxLength)
			{
				Console.WriteLine(Output.Error.CommandTooLong);
				return false;
			}
			else if (inputArray.Length < MinLength)
			{
				Console.WriteLine(Output.Error.CommandTooShort);
				return false;
			}
			return true;
		}
		private static bool CheckLength(IEnumerable<string> inputArray, int MinLength = 0, int MaxLength = -1)
		{
			if (MaxLength > 0 && inputArray.Count() > MaxLength)
			{
				Console.WriteLine(Output.Error.CommandTooLong);
				return false;
			}
			else if (inputArray.Count() < MinLength)
			{
				Console.WriteLine(Output.Error.CommandTooShort);
				return false;
			}
			return true;
		}

	}
}
