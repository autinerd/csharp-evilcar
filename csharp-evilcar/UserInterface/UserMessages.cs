

#pragma warning disable IDE1006

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Prompt
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

			public const string RemindHelp = "If you need help with some command or the prompt as a whole please don't hasitate to use the '?' at any point.";
			public const string ProgrammEnd = "See you soon!\nYour EPT-EvilProgrammingTeam";
			public const string HelpSymbol = "?";
			public const string SyntaxHead = "Syntax\nMainCase\tSubCase\t\tParameter 1\tParameter 2\t\tParameter 3\tParameter 4\t\tParameter 5";
		}
		internal static class Login
		{
			public const string AskForUsername = "please enter username: ";
			public const string Successful = "You are now logged in.";
			public const string Failed = "Login failed!";
		}
		internal static class Error
		{
			public const string DefaultError = "Default-Error";
			public const string HelpNeeded = "You requested help in Case: ";
			public const string WrongArgument = "Some of the entered arguments were wrong.";
			public const string DatabaseError = "An database error happend.";
			public const string NoUserLoggedIn = "You are not logged in.";
			public const string CommandAbort = "The command was not executed.";
			public const string WrongParameterLength = "You entered a not allowed parameter number.";
			public const string CommandFunctionUndefined = "Command-Function-is undefined yet.";
			public const string RequestedLogout = "You requestet logout.";
		}

		// cases
		internal static readonly CaseTyps.Main Main = new CaseTyps.Main()
		{
			CaseName = "main",
			SubCases = new CaseTyps.Base[] {
				new CaseTyps.Selection(){
					CaseName = "add",
					Help = "you can use 'add vehicle' or 'add customer'",
					AskForParameters = "Do you want to add a 'vehicle' or a 'costumer':",
					SubCases = new CaseTyps.Base[] {
						new CaseTyps.Command(){
							CaseName = "vehicle",
							AskForParameters =
								"Please enter now the parameters of the new vehicle in the following format:\n" +
								"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 electric 3",
							Help = "add a new vehicle",
							Syntax = "add\t\tvehicle\t\t<numberplate>\t<brand>\t\t\t<model>\t\t<category>\t\t<fleet_ID>",
							ParameterLength = new int[]{5},
							SubFunction = InternalLogic.AddVehicle,
						},
						new CaseTyps.Command(){
							CaseName = "customer",
							AskForParameters = "Please enter your <name> Residence:",
							Help = "add a new customer",							
							Syntax = "add\t\tcustomer\t<name>\t\t<residence>",
							ParameterLength = new int[]{2},
							SubFunction = InternalLogic.AddCustomer
						}
					}
				},
				new CaseTyps.Selection(){
					CaseName = "edit",
					SubCases = new CaseTyps.Base[]{
						new CaseTyps.Command()
						{
							CaseName = "vehicle",
							Syntax = "edit\tvehicle\t<vehicle_ID>\t{numberplate|fleet_ID}\t<new_value>",
							ParameterLength = new int[]{3},
							SubFunction = InternalLogic.EditCustomer,
						},
						new CaseTyps.Command()
						{
							CaseName = "customer",
							Syntax = "edit\tcustomer\t<customer_ID>\t{name|residence}\t<new_value>",
							ParameterLength = new int[]{3},
							SubFunction = InternalLogic.EditCustomer,
						},
					}
				},
				new CaseTyps.Selection(){
					CaseName = "delete",
					AskForParameters = "Do you want to delete a 'vehicle' or a 'customer'?",
					SubCases = new CaseTyps.Base[]{
						new CaseTyps.Command()
						{
							CaseName = "vehicle",
							Syntax = "delete\tvehicle\t\t<vehicle_ID>",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.DeleteVehicle,
						},
						new CaseTyps.Command()
						{
							CaseName = "customer",
							Syntax = "delete\tcustomer\t<customer_ID>",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.DeleteCustomer,
						},
					}
				},
				new CaseTyps.Selection(){
					CaseName = "view",
					SubCases = new CaseTyps.Base[]{
						new CaseTyps.Command()
						{
							CaseName = "branch",
							Syntax = "view\tbranch",
							ParameterLength = new int[]{0},

						},
						new CaseTyps.Command()
						{
							CaseName = "fleet",
							Syntax = "view\tfleet\n\t\t\t\t<branch_ID>",
							ParameterLength = new int[]{0,1},
						},
						new CaseTyps.Command()
						{
							CaseName = "vehicle",
							Syntax = "view\tvehicle\n\t\t\t\t<branch_ID>\t[<fleet_ID>]\n\t\tsingle\t\t<vehicle_ID>",
							ParameterLength = new int[]{0,2},
						},
						new CaseTyps.Command()
						{
							CaseName = "customer",
							Syntax = "view\tcustomer\n\t\t\t\t<customer_ID>",
							ParameterLength = new int[]{0,1},
						},
						new CaseTyps.Command()
						{
							CaseName = "bookings",
							Syntax = "view\tbookings\n\t\t\t\t<branch_ID>\t[< fleet_ID >]\n\t\tvehicle\t\t<vehicle_ID>\n\t\tcustomer\t<customer_ID>\n\t\tbooking\t\t<booking_ID>",
							ParameterLength = new int[]{0,2},
						},
					}
				},
				new CaseTyps.Selection(){
					CaseName = "booking",
					SubCases = new CaseTyps.Base[]{
						new CaseTyps.Command()
						{
							CaseName = "rent",
							Syntax = "booking\trent\t\t<vehicle_ID>\t<customer_ID>",
							ParameterLength = new int[]{2},
							SubFunction = InternalLogic.BookingRent,
						},
						new CaseTyps.Command()
						{
							CaseName = "return",
							Syntax = "booking\treturn\t\t<vehicle_ID>",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.BookingReturn,
						},
					}
				},
				new CaseTyps.Logout(){
					CaseName = "logout",
					Syntax = "logout",
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
