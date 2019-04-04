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
				// "enter your command (key in 'help' for all possible commands)"
				Console.Write(">> ");
				string commandInput = Console.ReadLine();
				string[] commandArray = new String[2];

				try
				{
					commandArray = commandInput.Split(' ');
				}
				catch (OverflowException)
				{
					// "command has too many arguments"
				}
				catch (Exception)
				{
					// "unidentified Exception"
				}

				switch (commandArray[0])
				{
					case "add":
						switch (commandArray[1])
						{
							case "vehicle":
								addVehicle();
								break;

							case "customer":
								addCustomer();
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

		private static void addVehicle()
		{
			// Please enter the the plate of your new vehicle for example: S-XY 4589 cw(">>> ");

			// ReadLine() && safe/store plate

			// Please enter now the other parameters in the following format: brand model class fleet
			// for example: audi q5 electric 3 cw(">>> ");

			// ReadLine() && Split() && brand=Split[0], ...
			// => check Null || to many InternalLogik.addVehicle(brand, model, plate, class, fleet)
		}

		private static void addCustomer()
		{
			Info.addCustomer();
			string customerInput = Console.ReadLine();
			string[] customerArray = new String[2];
			try
			{
				customerArray = customerInput.Split(' ');
			}
			catch (ArgumentNullException)
			{
				// ErrorMessage: Input necessary
			}
			catch (OverflowException)
			{
				// ErrorMessage: to long Input
			}
			string lastName = customerArray[0];
			string firstName = customerArray[1];
			// InternalLogic.addCustomer(lastName, firstName)
		}

		private static bool Login()
		{
			Info.WelcomeUsername();
			string username = Console.ReadLine();
			Info.PasswordQuestion();
			string password = Console.ReadLine();
			// (Sidney's ******-function) InternalLogik.Login(UserName, Password) => Hash!? return
			// true/false (get a Error back Handling?)
			return false;
		}
	}
}