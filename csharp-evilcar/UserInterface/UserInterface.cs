using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	internal static class UserInterface
	{
		internal static void Main(string[] args)
		{
			if (!Login())
			{
				return;
			}
			int loaded = Database.DatabaseController.LoadDatabase();
			Prompt();
		}

		private static void Prompt()
		{
			while (true)
			{
				// "enter your command (key in 'help' for all possible commands)"
				Console.Write(">> ");
				string commandInput = Console.ReadLine();
				string[] commandArray = commandInput.Split(' ');

				switch (commandArray[1])
				{
					case "add":
						switch (commandArray[2])
						{
							case "vehicle":
								AddVehicle();
								break;

							case "customer":
								AddCustomer();
								break;

							default:
								// "You can't combine 'add' with this command
								break;
						}
						break;

					case "edit":
						break;

					case "remove":
						break;

					case "rebook":
						break;

					case "view":
						break;

					case "help":
						break;

					case "logout":
						break;

					case "exit":
						// perhaps after exit(); a return instead break
						break;

					default:
						// command doesn't exist, please key in "help" to see all possible commands
						break;
				}
			}
		}

		private static void AddVehicle()
		{
			// Please enter the the plate of your new vehicle for example: S-XY 4589 cw(">>> "); =>
			// be aware, that this will be Split[0]

			// ReadLine() && safe/store plate

			// Please enter now the other parameters in the following format: brand model class fleet
			// for example: audi q5 electric 3 cw(">>> "); => be aware, that this will be Split[0]

			// ReadLine() && Split() && brand=Split[1], ...
			// => check Null || to many InternalLogik.addVehicle(brand, model, plate, class, fleet)
		}

		private static void AddCustomer()
		{
			// Please enter the new Customer in the following format LastName PreName cw(">>> "); =>
			// be aware, that this will be Split[0]

			// ReadLine() && Split() && LastName = Split[1], ...
			// => check Null || to many InternalLogic.addCustomer(Lastname, PreName)
		}

		private static bool Login()
		{
			// Welcome Text Please enter UserName Please enter Password (Sidney's ******-function)
			// cw("> "); => be aware, that this will be your Split[0] InternalLogik.Login(UserName,
			// Password) => Hash!? return true/false (get a Error back Handling?)
			return false;
		}

		private static bool InputAndCheckPassword(string username)
		{
			string password = "";
			string passwordPrompt = "Password: ";
			Console.Write(passwordPrompt);
			while (true)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
				{
					break;
				}
				else if (key.Key == ConsoleKey.Backspace)
				{
					password = password.Length == 0 ? "" : password.Remove(password.Length - 1, 1);
				}
				else
				{
					password += key.KeyChar;
				}
			}
			return Database.DatabaseController.CheckUserCredentials(username, password);
		}
	}
}