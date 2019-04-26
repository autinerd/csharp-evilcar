using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	
	internal static partial class UserInterface
	{
		// case method
		private static void EditCase(string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Console.Write(OutputStrings.Edit.AskForSelection);
				parameters = GetInput(1, 1);
			}
			string selection = parameters[0].ToLower();
			parameters = parameters.Skip(1).ToArray();

			switch (selection)
			{
				case "vehicle":
					SubCase(OutputStrings.Edit.Vehicle, 2, DummyFunc, parameters);
					break;

				case "customer":
					SubCase(OutputStrings.Edit.Customer, 2, DummyFunc, parameters);
					break;

				case "?":
				case "help":
					// help information
					Console.Write(OutputStrings.Edit.Help);
					break;
				default:
					Console.WriteLine(Errors.Combine);
					break;
			}
			return;
		}

		// tool methods for case method
		private static void EditCustomer()
		{
			throw new NotImplementedException();
		}

		private static void EditVehicle()
		{
			throw new NotImplementedException();
		}
	}
}
