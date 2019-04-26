
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		private static void AddCase(string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Console.Write(OutputStrings.Add.AskForSelection);
				parameters = GetInput(1, 1);
			}
			string selection = parameters[0].ToLower();
			parameters = parameters.Skip(1).ToArray();

			switch (selection)
			{
				case "vehicle":
					SubCase(OutputStrings.Add.Vehicle, 5, DummyFunc, parameters);
					break;

				case "customer":
					SubCase(OutputStrings.Add.Customer, 2, DummyFunc, parameters);
					break;

				case "?":
				case "help":
					// help information
					Console.Write(OutputStrings.Add.Help);
					break;
				default:
					Console.WriteLine(Errors.Combine);
					break;
			}
			return;
		}
	}
}
