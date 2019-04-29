﻿using System;
using System.Collections.Generic;

#pragma warning disable IDE1006

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Output
	{
		// general
		internal static class General
		{

			public const string EvilCarLogo =
			#region Logo
@"
	    ______      _ ________          
	   / ____/   __(_) / ____/___ ______
	  / __/ | | / / / / /   / __ `/ ___/
	 / /___ | |/ / / / /___/ /_/ / /    
	/_____/ |___/_/_/\____/\__,_/_/     

"
			#endregion Logo
			;

			internal const string RemindHelp = "If you need help with some command or the prompt as a whole please don't hasitate to use the '?' at any point.";
			public const string ProgrammEnd = "See you soon!\nYour EPT-EvilProgrammingTeam";
			public static string HelpSymbol => "?";
			public static string SyntaxHead => "Syntax\nMainCase\tSubCase\t\tParameter 1\tParameter 2\t\tParameter 3\tParameter 4\t\tParameter 5";
		}
		internal static class Login
		{
			public const string AskForUsername = "please enter username: ";
			public const string Successful = "You are now logged in.";
			public const string Failed = "Login failed!";
		}
		internal static class Error
		{
			public static string Combine => "your first command cannot be combined with your second.";
			public const string CommandTooShort = "not enough parameters have been inserted.";
			public const string CommandTooLong = "too much parameters have been inserted.";
			public const string CommandNotExisting = "command doesn't exist, please use '?' for help.";
			public const string CommandAbort = "The command was not executed.";
			public const string InputIncorrect = "Your input was incorrect";
			public const string SubFunctionUndefined = "This Command doesn't do anything yet!";


		}

		// cases
		internal static readonly CaseTyps.Default Main = new CaseTyps.Main()
		{
			CaseName = "main",
			SubCases = new CaseTyps.Default[] {
				new CaseTyps.Default(){
					CaseName = "add",
					Help = "help for 'add'\n" +
						"you can use 'add vehicle' or 'add customer'",
					AskForParameters = "do you want to add a 'vehicle' or a 'costumer':",
					SubCases = new CaseTyps.Default[] {
						new CaseTyps.Default(){
							CaseName = "vehicle",
							AskForParameters =
								"Please enter now the parameters of the new vehicle in the following format:\n" +
								"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 electric 3",
							 Help =
								"help for 'add vehicle'\n" +
								"full command: add vehicle [ numberplate brand model class fleetNr]\n" +
								"In brand, model and class none space allowed\n" +
								"If you only use 'add vehicle' you will be ask for more information about the vehicle.\n" +
								"If you want to do this at ones use the full command",
							Syntax = "add\t\tvehicle\t\t<numberplate>\t<brand>\t\t\t<model>\t\t<category>\t\t<fleet_ID>",
							ParameterLenght = new int[]{5},
							SubFunction = InternalLogic.AddVehicle,
						},
						new CaseTyps.Default(){
							 CaseName = "customer",
							 AskForParameters = "Please enter your Name Residence:",
							 Help =
								"help for 'add customer'\n" +
								"full command: add customer [ Name Residence]\n" +
								"In Lastname and Firstname none space allowed.\n" +
								"If you only use 'add customer' you will be ask for more information about the coustomer." +
								"If you want to do this at ones use the full command",
							Error = "Your input wasn't correct\n" + General.RemindHelp,
							Syntax = "add\t\tcustomer\t<name>\t\t<residence>",
							ParameterLenght = new int[]{2},
							SubFunction = InternalLogic.AddCustomer
						}
					}
				},
				new CaseTyps.Default(){
					CaseName = "edit",
					SubCases = new CaseTyps.Default[]{
						new CaseTyps.Default()
						{
							CaseName = "vehicle",
						},
						new CaseTyps.Default()
						{
							CaseName = "customer",
							Syntax = "edit\tcustomer\t<customer_ID>\t{name|residence}\t<new_value>",
							ParameterLenght = new int[]{3},
							SubFunction = InternalLogic.EditCustomer,
						},
					}
				},
				new CaseTyps.Default(){
					CaseName = "delete",
					SubCases = new CaseTyps.Default[]{
						new CaseTyps.Default()
						{
							CaseName = "vehicle",
							Syntax = "delete\tvehicle\t\t<vehicle_ID>",
							ParameterLenght = new int[]{1},
							SubFunction = InternalLogic.DeleteVehicle,
						},
						new CaseTyps.Default()
						{
							CaseName = "customer",
							Syntax = "delete\tcustomer\t<customer_ID>",
							ParameterLenght = new int[]{1},
							SubFunction = InternalLogic.DeleteCustomer,
						},
					}
				},
				new CaseTyps.Default(){
					CaseName = "view",
					SubCases = new CaseTyps.Default[]{
						new CaseTyps.Default()
						{
							CaseName = "branch",
							Syntax = "view\tbranch",
							ParameterLenght = new int[]{0},
						},
						new CaseTyps.Default()
						{
							CaseName = "fleet",
							Syntax = "view\tfleet\n\t\t\t\t<branch_ID>",
							ParameterLenght = new int[]{0,1},
						},
						new CaseTyps.Default()
						{
							CaseName = "vehicle",
							Syntax = "view\tvehicle\n\t\t\t\t<branch_ID>\t[<fleet_ID>]\n\t\tsingle\t\t<vehicle_ID>",
							ParameterLenght = new int[]{0,2},
						},
						new CaseTyps.Default()
						{
							CaseName = "customer",
							Syntax = "view\tcustomer\n\t\t\t\t<customer_ID>",
							ParameterLenght = new int[]{0,1},
						},
						new CaseTyps.Default()
						{
							CaseName = "bookings",
							Syntax = "view\tbookings\n\t\t\t\t<branch_ID>\t[< fleet_ID >]\n\t\tvehicle\t\t<vehicle_ID>\n\t\tcustomer\t<customer_ID>\n\t\tbooking\t\t<booking_ID>",
							ParameterLenght = new int[]{0,2},
						},
					}
				},
				new CaseTyps.Default(){
					CaseName = "booking",
					SubCases = new CaseTyps.Default[]{
						new CaseTyps.Default()
						{
							CaseName = "rent",
							Syntax = "booking\trent\t\t<vehicle_ID>\t<customer_ID>",
							ParameterLenght = new int[]{2},
							SubFunction = InternalLogic.BookingRent,
						},
						new CaseTyps.Default()
						{
							CaseName = "return",
							Syntax = "booking\treturn\t\t<vehicle_ID>",
							ParameterLenght = new int[]{1},
							SubFunction = InternalLogic.BookingReturn,
						},
					}
				},
				new CaseTyps.Logout(){
					CaseName = "logout",
					Syntax = "logout",
					SubFunction = (parameters) => { return ReturnValue.RequestedLogout; },
				}
			},
		};
	}
}



/*
 * MainCase	SubCase		Parameter 1		Parameter 2				Parameter 3		Parameter 4		Parameter 5
 * 
 * add		vehicle		<numberplate>	<brand>					<model>			<category>		<fleet_ID>
 * add		customer	<name>			<residence>
 *			
 * edit		vehicle		<vehicle_ID>	{numberplate|fleet_ID}	<new_value>
 * edit		customer	<customer_ID>	{name|residence}		<new_value>
 *			
 * delete	vehicle		<vehicle_ID>
 * delete	customer	<customer_ID>
 *			
 * view		branch
 * view		fleet
 *						<branch_ID>
 * view		vehicle
 *						<branch_ID>		[<fleet_ID>]
 *						single			<vehicle_ID>
 * view		customer
 *						<customer_ID>
 * view		bookings
 *						<branch_ID>		[<fleet_ID>]
 *						vehicle			<vehicle_ID>
 *						customer		<customer_ID>
 *						booking			<booking_ID>
 *
 * booking	rent		<vehicle_ID>	<customer_ID>
 * booking	return		<vehicle_ID>
 * 
 * logout
 */
