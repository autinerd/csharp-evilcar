using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		private static void RebookCase(string[] parameters)
		{
			string selection = parameters[0];
			parameters = parameters.Skip(1).ToArray();
		}
	}
}
