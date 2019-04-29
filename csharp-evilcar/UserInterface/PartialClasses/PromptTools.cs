using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace CsharpEvilcar.UserInterface
{
	internal static partial class Prompt
	{
		internal static ReturnValue.Type GetInput(out string[] input)
		{
			Print("", "");
			input = ( from Match m in Regex.Matches(Console.ReadLine(), @"("".*""|[\S]+)+")
						let s = m.Value
						select s.Replace("\"", "") ).ToArray(); // extracts all parameters, single words and quoted ones
			return ReturnValue.Success();
			
		}

	

		internal static ReturnValue.Type Print(string str = "", string end = "\n")
		{
			if (str != null)
			{
				Console.Write(">>> " + Regex.Replace(str, @"\n", "\n    ") + end);
			}
			return ReturnValue.Success();
		}
	}
}
