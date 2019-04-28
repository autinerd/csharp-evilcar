using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		private static ErrorCode GetInput(out string[] input, int MinLength = 0, int Maxlength = -1)
		{
			if (MinLength == 0 && Maxlength == 0)
			{
				input = Array.Empty<string>();
				return ErrorCode.Success;
			}
			else
			{
				Print("","");
				input = ( from Match m in Regex.Matches(Console.ReadLine(), @"("".*""|[\S]+)+")
						  let s = m.Value
						  select s.Replace("\"", "") ).ToArray(); // extracts all parameters, single words and quoted ones
				return CheckLength(input, MinLength, Maxlength);
			}
		}

		private static ErrorCode CheckLength(string[] inputArray, int MinLength = 0, int MaxLength = -1) => 
			MaxLength <= 0 || inputArray.Length <= MaxLength
				? inputArray.Length >= MinLength
				? ErrorCode.Success // between min and max
				: ErrorCode.CommandTooShort // less than min
				: ErrorCode.CommandTooLong; // more than max

		private static ErrorCode Print(string str="",string end = "\n"){
			if (str != null)
			{
				Console.Write(">>> " + Regex.Replace(str, @"\n", "\n    ") + end);
			}
			return ErrorCode.Success;
		}
	}
}
