using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		private static string[] GetInput(int MinLength = 0, int Maxlength = -1)
		{
			string[] input = Console.ReadLine().Split(' ');
			return input;
		}

		private static bool CheckLength(string[] inputArray, int MinLength = 0, int MaxLength = -1)
		{
			if (MaxLength > 0 && inputArray.Length > MaxLength)
			{
				Console.WriteLine(OutputStrings.MainLevel.CommandTooLong);
				return false;
			}
			if (inputArray.Length < MinLength)
			{
				Console.WriteLine(OutputStrings.MainLevel.CommandTooShort);
				return false;

			}
			return true;
		}

	}
}
