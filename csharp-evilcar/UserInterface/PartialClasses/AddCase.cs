﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		// case method
		private static void AddCase(string[] parameters)
		{
			if (parameters.Length == 0)
			{
				Console.Write(OutputStrings.Add.AskForSelection);
				parameters = GetInput(1, 1);
			}
			string selection = parameters[0].ToLower();
			parameters = parameters.Skip(1).ToArray();

			switch (selection)
			{
				case "vehicle":
					AddVehicle(parameters);
					break;

				case "customer":
					AddCustomer(parameters);
					break;

				case "?":
					// help information
					Console.Write(OutputStrings.Add.Help);
					break;
				default:
					Console.WriteLine(Errors.Combine);
					break;
			}
			return;
		}

		// tool methods for case method
		private static void AddVehicle(string[] parameters)
		{
			switch (parameters.Length)
			{
				case 0:
					Console.Write(OutputStrings.Add.Vehicle.AskForVehicleParameters);
					parameters = GetInput();
					goto case 5;
				case 5:
					//Add vehicle
					#warning Hier muss ich noch das Auto erzeugen.
					break;

				case 1:
				case 2:
				case 3:
				case 4:
					if (parameters.Last() == "?")
					{
						Console.Write(OutputStrings.Add.Vehicle.Help);
						break;
					}
					else { goto default; }
				default:
					Console.Write(OutputStrings.Add.Vehicle.Error);
					break;
			}
		}

		private static void AddCustomer(string[] parameters)
		{

			switch (parameters.Length) {
				case 0:
					Console.Write(OutputStrings.Add.Customer.AskForCustomerParameters);
					parameters = GetInput(2, 2);
					goto case 2;
				case 2:
					//Add customer
					break;
				case 1:
					if(parameters.Last() == "?")
					{
						Console.Write(OutputStrings.Add.Customer.Help);
						break;
					}
					else { goto default; }
				default:
					Console.Write(OutputStrings.Add.Customer.Error);
					break;
			}
			/*
			Console.Write(OutputStrings.AddCustomer);
			string[] customerArray = GetInput();

			if (CheckLength(customerArray, 2))
			{
				string lastName = customerArray[0];
				string firstName = customerArray[1];
				// InternalLogic.addCustomer(lastName, firstName);
				// InternalLogic.addCustomer(customerArray[0], customerArray[1]);
			}*/
		}

	}
}
