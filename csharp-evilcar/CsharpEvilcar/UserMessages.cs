using System.Collections.Generic;
using static CsharpEvilcar.Prompt.ErrorCodeFlags;

namespace CsharpEvilcar
{
	internal static partial class UserMessages
	{
		internal static Dictionary<Prompt.ErrorCodeFlags, string> Messages => new Dictionary<Prompt.ErrorCodeFlags, string>
		{
			{ IsSuccess, "done."},
			{ IsEmpty, "done." },
			{ IsPass, "Pass-return" },
			{ IsError, "Default-Error" },
			{ IsHelpNeeded, "You requested help in Case: " },
			{ IsWrongArgument, "Some of the entered arguments were wrong." },
			{ IsDatabaseError, "An database error happend." },
			{ IsNoUserLoggedIn, "You are not logged in." },
			{ IsCommandAbort, "The command was not executed." },
			{ IsWrongParameterLength, "The amount of entered parameters doesn't match the amount needed." },
			{ IsCommandFunctionUndefined, "Command-Function-is undefined yet." },
			{ IsRequestedLogout, "You requested logout." },
			{ None, "Type-return-undefined" }
		};

		internal static Dictionary<Prompt.ErrorCodeFlags, string> Passes => new Dictionary<Prompt.ErrorCodeFlags, string>
		{
			{ IsSuccess, "done."},
			{ IsEmpty, "done." },
			{ IsPass, "Pass-return" }
		};

		public static Dictionary<Prompt.ErrorCodeFlags, string> Errors => new Dictionary<Prompt.ErrorCodeFlags, string>
		{
			{ IsError, "Default-Error" },
			{ IsHelpNeeded, "You requested help in Case: " },
			{ IsWrongArgument, "Some of the entered arguments were wrong." },
			{ IsDatabaseError, "An database error happend." },
			{ IsNoUserLoggedIn, "You are not logged in." },
			{ IsCommandAbort, "The command was not executed." },
			{ IsWrongParameterLength, "The amount of entered parameters doesn't match the amount needed." },
			{ IsCommandFunctionUndefined, "Command-Function-is undefined yet." },
			{ IsRequestedLogout, "You requested logout." }
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
