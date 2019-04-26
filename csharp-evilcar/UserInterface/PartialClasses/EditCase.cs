using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{

	using Local = OutputStrings.Add.Vehicle;
	internal static partial class UserInterface
	{
		// case method
		private static void EditCase(string[] parameters)
		{
			string selection = parameters[0].ToLower();
			parameters = parameters.Skip(1).ToArray();

			switch (selection)
			{
				case "vehicle":
					EditVehicle();
					break;

				case "customer":
					EditCustomer();
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
