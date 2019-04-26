
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
#if q
	internal static partial class UserInterface
	{
		private static void AddCase(string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Console.Write(Output.Add.AskForSelection);
				parameters = GetInput(1, 1);
			}
			string selection = parameters[0].ToLower();
			parameters = parameters.Skip(1).ToArray();

			switch (selection)
			{
				case "vehicle":
					SubCase(Output.Add.Vehicle, parameters);
					break;

				case "customer":
					SubCase(Output.Add.Customer, parameters);
					break;

				case "?":
				case "help":
					// help information
					Console.Write(Output.Add.Help);
					break;
				default:
					Console.WriteLine(Output.Error.Combine);
					break;
			}
			return;
		}
	}
#endif
}
