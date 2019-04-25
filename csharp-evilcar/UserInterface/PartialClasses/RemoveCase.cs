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
		private static void RemoveCase(string[] parameters)
		{
			string selection = parameters[0].ToLower();
			parameters = parameters.Skip(1).ToArray();
			switch (selection)
			{
				case "customer":
					RemoveCustomer();
					break;
				case "vehicle":
					RemoveVehicle();
					break;

			}
		}

		// tool methods for cass method
		private static void RemoveCustomer()
		{
			throw new NotImplementedException();
		}

		private static void RemoveVehicle()
		{
			throw new NotImplementedException();
		}

	}
}
