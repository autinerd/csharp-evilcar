using System;

namespace CsharpEvilcar.UserInterface
{
	internal static class Info
	{
		internal static void WelcomeUsername()
		{
			Console.WriteLine("Welcome to EvilCar\nPlease enter your Username:\n> ");
		}

		internal static void PasswordQuestion()
		{
			Console.WriteLine("Please enter your Password:\n> ");
		}

		internal static void addCustomer()
		{
			Console.WriteLine("Please enter the new Customer in the following format:\nLastName PreName \n>>> ");
		}
	}
}