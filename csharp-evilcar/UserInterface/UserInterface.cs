using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	static class UserInterface
	{
		internal static void Main(string[] args)
		{
			if (Login() == true)
			{
				// LoadDatabase();
				Prompt();
			}
		}

		private static void Prompt()
		{
			while (true)
			{
				Console.Write(">>> ");
				string commands = Console.ReadLine();
				string command = commands.Split(' ')[0];
				int returnvalue;
				switch (command)
				{
					case "add":
					case "help":
					case "exit":

					default:
						break;
				}
			}
		}

		private static bool Login()
		{
			return false;
		}
	}
}
