using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		// interface
		internal abstract class MainCase
		{
			public static string SyntaxHead => " MainCase\tSubCase\t\tParameter 1\tParameter 2\t\tParameter 3\tParameter 4\t\tParameter 5\n";
			public virtual string AskForSelection { get; }
			public virtual string Help { get; }
			public virtual string CaseName { get; }
			public virtual IEnumerable<SubCase> SubCases { get; }
			public virtual string Syntax(bool withHead) => string.Join("",
				withHead
				? SyntaxHead
				: "",
				(from c in SubCases
				select c.Syntax).Aggregate((str, item) => str + item));
		}
	}
	internal static partial class UserInterface
	{
		private static void MainCase(Output.MainCase Case, string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Console.Write(Case.AskForSelection);
				parameters = GetInput(1, 1);
			}
			string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
			parameters = parameters.Skip(1).ToArray();

			IEnumerable<Output.SubCase> cases = from s in Case.SubCases
												where s.CaseName == selection
												select s;
			if (cases.Count() == 1)
			{
				SubCase(cases.Single(), parameters);
			}
			else if (selection == "?" || selection == "help")
			{
				// help information
				Console.Write(Case.Help);
			}
			else
			{
				Console.WriteLine(Output.Error.Combine);
			}
			return;
		}
	}
}
