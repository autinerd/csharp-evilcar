

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
		internal static class Pass
		{
			public const string Success = "done.";
			public const string Empyt = "done.";
		}

		// cases
		internal static readonly CaseTyps.Main Main = new CaseTyps.Main()
		{
			CaseName = "main",
			SubCases = new CaseTyps.Base[] {
				new CaseTyps.Selection(){
					CaseName = "add",
					Help = "Use this if you want to add a vehicle or a customer to the databese.",
					AskForParameters = "Do you want to add a 'vehicle' or a 'costumer':",
					SubCases = new CaseTyps.Base[] {
						new CaseTyps.Command(){
							CaseName = "vehicle",
							AskForParameters =
								"Please enter now the parameters of the new vehicle in the following format:\n" +
								"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 electric 3",
							Help = "Use this if you want to add a new vehicle.",
							Syntax = "add\t\tvehicle\t\t<numberplate>\t<brand>\t\t\t<model>\t\t<category>\t\t<fleet_ID>",
							ParameterLength = new int[]{5},
							SubFunction = InternalLogic.AddVehicle,
						},
						new CaseTyps.Command(){
							CaseName = "customer",
							AskForParameters = "Please enter your <name> and <residence>",
							Help = "Use this if you want to add a new customer.",
							Syntax = "add\t\tcustomer\t<name>\t\t<residence>",
							ParameterLength = new int[]{2},
							SubFunction = InternalLogic.AddCustomer
						}
					}
				},
				new CaseTyps.Selection(){
					CaseName = "edit",
					AskForParameters="Do you want to edit a 'vehicle' or a 'customer'?",
					Help="Use this if you want to edit a vehicle or a customer.",
					SubCases = new CaseTyps.Base[]{
						new CaseTyps.Command()
						{
							CaseName = "vehicle",
							AskForParameters="Please enter the <vehicle_ID>  'numberplate' or 'fleet_ID' and <new_value>.",
							Help="Use this if you want to edit a vehicle.",
							Syntax = "edit\tvehicle\t<vehicle_ID>\t{numberplate|fleet_ID}\t<new_value>",
							ParameterLength = new int[]{3},
							SubFunction = InternalLogic.EditCustomer,
						},
						new CaseTyps.Command()
						{
							CaseName = "customer",
							AskForParameters="Please enter the <customer_Id> 'name' or 'residence' and <new_value>.",
							Help="Use this if you want to edit a customer.",
							Syntax = "edit\tcustomer\t<customer_ID>\t{name|residence}\t<new_value>",
							ParameterLength = new int[]{3},
							SubFunction = InternalLogic.EditCustomer,
						},
					}
				},
				new CaseTyps.Selection(){
					CaseName = "delete",
					AskForParameters = "Do you want to delete a 'vehicle' or a 'customer'?",
					Help="Use this if you want something to be deleted from the database",
					SubCases = new CaseTyps.Base[]{
						new CaseTyps.Command()
						{
							CaseName = "vehicle",
							AskForParameters="Please enter the <vehicle_ID> of the vehicle you want do delete.",
							Help="Use this command if you want to delete a vehicle from the database."
,							Syntax = "delete\tvehicle\t\t<vehicle_ID>",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.DeleteVehicle,
						},
						new CaseTyps.Command()
						{
							CaseName = "customer",
							AskForParameters="Please enter the <customer_id> of the customer you want to delete.",
							Help="Use this command if you want to delete a customer from the database.",
							Syntax = "delete\tcustomer\t<customer_ID>",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.DeleteCustomer,
						},
					}
				},
				new CaseTyps.Selection(){
					CaseName = "view",
					AskForParameters="Please enter if you want to view 'branch', 'fleet', 'vehicle', 'customer' or 'bookings'.",
					Help="Use this if you want to view data from the database.",
					SubCases = new CaseTyps.Base[]{
						new CaseTyps.Command()
						{
							CaseName = "branch",
							AskForParameters=null, // because no more parameter requierd
							Help="Use this command if you want to view all ",
							Syntax = "view\tbranch",
							ParameterLength = new int[]{0},
							SubFunction=UserInterface.DummyFunc,
							//SubFunction=?
#warning view-branch SubFunction fehlt noch
						},
						new CaseTyps.Command()
						{
							CaseName = "fleet",
							AskForParameters="Please enter nothing if you want to view all fleets or enter the <branch_ID> for which to see all fleets.",
							Help="Use this command if you want do view all all fleets or the fleets of one branch.",
							Syntax = "view\tfleet\n\t\t\t\t<branch_ID>",
							ParameterLength = new int[]{0,1},
							SubFunction=UserInterface.DummyFunc,
							//SubFunction=?
#warning view-fleet SubFunction fehlt noch
						},
						new CaseTyps.Command()
						{
							CaseName = "vehicle",
							AskForParameters="Plase enter <branch_ID> and optional <fleet_ID> if you want to view all vehicle of a branch or a fleet in a branch.\n"+
							"If you want to see a single car, please enter 'single' and than the <vehicle_ID>.",
							Help="Use this command if you want to view one or many vehicles.",
							Syntax = "view\tvehicle\n\t\t\t\t<branch_ID>\t[<fleet_ID>]\n\t\tsingle\t\t<vehicle_ID>",
							ParameterLength = new int[]{0,2},
							//SubFunction=?
#warning view-vehicle SubFunction fehlt noch
						},
						new CaseTyps.Command()
						{
							CaseName = "customer",
							AskForParameters="Please enter the <customer_ID> of the customer you want to see.",
							Help="Use this command if you want to view a customers data.",
							Syntax = "view\tcustomer\n\t\t\t\t<customer_ID>",
							ParameterLength = new int[]{0,1},
							//SubFunction=?
#warning view-customer SubFunction fehlt noch
						},
						new CaseTyps.Command()
						{
							CaseName = "bookings",
							AskForParameters="Fehlt noch.",
#warning view-booking AskForParameters fehlt noch
							Help="",
#warning view-booking Help fehlt noch
							//Syntax = "view\tbookings\n\t\t\t\t<branch_ID>\t[< fleet_ID >]\n\t\tvehicle\t\t<vehicle_ID>\n\t\tcustomer\t<customer_ID>\n\t\tbooking\t\t<booking_ID>",
							Syntax = "Fehlt noch.",
#warning @Sidney from @Johannes : Was bei view booking geplant was man alles sehen kann? Weil hier stimmt die Syntax nicht.
							ParameterLength = new int[]{0,2},
						},
					}
				},
				new CaseTyps.Selection(){
					CaseName = "booking",
					AskForParameters="Please enter if you want to 'rent' or 'return' a vehicle.",
					Help="Use this if you want to rent or return a vehicle.",
					SubCases = new CaseTyps.Base[]{
						new CaseTyps.Command()
						{
							CaseName = "rent",
							AskForParameters="Please enter the <vehicle_ID> and the <customer_ID> for a new booking.",
							Help="",
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
