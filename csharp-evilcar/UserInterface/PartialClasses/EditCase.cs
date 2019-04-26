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
		// case method
		private static void EditCase(string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Console.Write(Output.Edit.AskForSelection);
				parameters = GetInput(1, 1);
			}
			string selection = parameters[0].ToLower();
			parameters = parameters.Skip(1).ToArray();

			switch (selection)
			{
				case "vehicle":
					SubCase(Output.Edit.Vehicle, parameters);
					break;

				case "customer":
					SubCase(Output.Edit.Customer,parameters);
					break;

				case "?":
				case "help":
					// help information
					Console.Write(Output.Edit.Help);
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
