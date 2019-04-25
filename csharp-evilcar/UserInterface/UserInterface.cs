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
				Console.Write(Strings.Prompt);
				string[] commandArray = GetInput();

				if (!CheckLength(commandArray, 2))
				{
					return;     // not good yet because new Login necessary
				}

				switch (commandArray[0])
				{
					case "add":
						switch (commandArray[1])
						{
							case "vehicle":
								AddVehicle();
								break;

							case "customer":
								AddCustomer();
								break;

							default:
								Console.WriteLine(Errors.Combine);
								break;
						}
						break;

					case "edit":
						switch (commandArray[1])
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
						break;

					case "remove":
						switch (commandArray[1])
						{
							case "vehicle":
								RemoveVehicle();
								break;

							case "customer":
								RemoveCustomer();
								break;

							default:
								Console.WriteLine(Errors.Combine);
								break;
						}
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
						Console.WriteLine(Errors.Existence);
						break;
				}
			}
		}

		#region commandArray[0] = "remove"

		private static void RemoveCustomer()
		{
			throw new NotImplementedException();
		}

		private static void RemoveVehicle()
		{
			throw new NotImplementedException();
		}

		#endregion commandArray[0] = "remove"

		#region commandArray[0] = "edit"

		private static void EditCustomer()
		{
			throw new NotImplementedException();
		}

		private static void EditVehicle()
		{
			throw new NotImplementedException();
		}

		#endregion commandArray[0] = "edit"

		#region commandArray[0] = "add"

		private static void AddVehicle()
		{
			Console.Write(Strings.Numberplate);
			string numberplate = Console.ReadLine();    // no ErrorHandling yet
			Console.Write(Strings.VehicleParameters);
			string[] vehicleParamArray = GetInput();

			if (CheckLength(vehicleParamArray, 4))
			{
				// InternalLogik.addVehicle(brand, model, plate, class, fleet);
				// IntternalLogic.addVehicle(vehicleParamArray[0], ..., numberplate, ...);
			}
		}

		private static void AddCustomer()
		{
			Console.Write(Strings.AddCustomer);
			string[] customerArray = GetInput();

			if (CheckLength(customerArray, 2))
			{
				string lastName = customerArray[0];
				string firstName = customerArray[1];
				// InternalLogic.addCustomer(lastName, firstName);
				// InternalLogic.addCustomer(customerArray[0], customerArray[1]);
			}
		}

		#endregion commandArray[0] = "add"

		private static string[] GetInput()
		{
			string input = Console.ReadLine();
			return input.Split(' ');
		}

		private static bool CheckLength(string[] inputArray, int length)
		{
			if (inputArray.Length > length)
			{
				Console.WriteLine(Errors.TooLong);
				return false;
			}
			return true;
		}

		private static bool Login()
		{
			Console.Write(Strings.WelcomeUsername);
			string username = Console.ReadLine();
			if (InputAndCheckPassword(username))
			{
				// InternalLogik.Login(UserName, Password);
				return true;
			}
			else
			{
				Console.WriteLine(Errors.LoginFailed);
				return false;
			}
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
					Console.WriteLine();
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